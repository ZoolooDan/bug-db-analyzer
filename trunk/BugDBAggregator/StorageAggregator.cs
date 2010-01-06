using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.QueryParser;


namespace BugDB.Aggregator
{
  // Alias for record type
  using Record = IDictionary<string, string>;

  /// <summary>
  /// Processes BugDB query results and 
  /// fills database.
  /// </summary>
  public class StorageAggregator
  {
    #region Private Fields
    private const string NumCol = "number";
    private const string RevisionCol = "subnumber";
    private const string TypeCol = "deadline";
    private const string StatusCol = "status";
    private const string DateCol = "date";
    private const string AppCol = "application";
    private const string ModuleCol = "modul";
    private const string SubModuleCol = "submodul";
    private const string FoundRelCol = "apprelease";
    private const string TargetRelCol = "frelease";
    private const string SeverityCol = "severity";
    private const string PriorCol = "priority";
    private const string ContributorCol = "contributor";
    private const string LeaderCol = "leader";
    private const string DevCol = "developer";
    private const string QaCol = "qa";
    private const string SummaryCol = "summary";

    private IDataProvider m_provider;
    private IDictionary<string, Application> m_appCache;
    private IDictionary<ComplexKey, Module> m_moduleCache;
    private IDictionary<ComplexKey, SubModule> m_subModuleCache;
    private IDictionary<ComplexKey, Release> m_relCache;
    private IDictionary<string, Person> m_personCache;
    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Constracts aggregator for specified provider.
    /// </summary>
    public StorageAggregator(IDataProvider provider)
    {
      m_provider = provider;
    }
    #endregion Constructors

    #region Public Methods
    /// <summary>
    /// Reads query results from file and fills database.
    /// </summary>
    /// <remarks>
    /// See <see cref="FillStorage(TextReader)"/> for more details.
    /// </remarks>
    public void FillStorage(string dataPath)
    {
      using(TextReader reader = new StreamReader(dataPath))
      {
        FillStorage(reader);
      }
    }

    /// <summary>
    /// Reads query results from stream and fills database.
    /// </summary>
    /// <remarks>
    /// Prerequisite for normal functioning of this method
    /// is initialized empty storage. But this particular 
    /// method doesn't initialize storage itself. So prior
    /// calling this method storage shall be initialized.
    /// </remarks>
    public void FillStorage(TextReader reader)
    {
      // Create records enumerator from stream
      IEnumerator<Record> records = QueryResultParser.CreateRecordsEnumerator(reader);

      // Initialize caches
      m_appCache = new Dictionary<string, Application>();
      m_moduleCache = new Dictionary<ComplexKey, Module>();
      m_subModuleCache = new Dictionary<ComplexKey, SubModule>();
      m_relCache = new Dictionary<ComplexKey, Release>();
      m_personCache = new Dictionary<string, Person>();

      // Enumerate records (actually revisions)
      while( records.MoveNext() )
      {
        Record record = records.Current;

        // Process number
        int number;
        if( !int.TryParse(record[NumCol], out number) )
        {
          Debug.Fail("Number shall be specified.");
          continue;
        }

        int revision;
        // Process revision number
        if (!int.TryParse(record[RevisionCol], out revision))
        {
          Debug.Fail("Revision shall be specified.");
          continue;
        }

        // Process date
        DateTime date;
        if (!DateTime.TryParse(record[DateCol], out date))
        {
          Debug.Fail("Date shall be specified.");
          continue;
        }

        // Process record type (could be null)
        string recordTypeStr = record[TypeCol];
        // Process status
        string statusStr = record[StatusCol];
        // Process severity
        int severityNum = -1;
        if( record[SeverityCol] != null )
        {
          if (!int.TryParse(record[SeverityCol], out severityNum))
          {
            Debug.Fail("Severity shall be the number.");
            continue;
          }
        }
        // Process priority
        int priority = -1;
        if( record[PriorCol] != null )
        {
          if(!int.TryParse(record[PriorCol], out priority))
          {
            Debug.Fail("Number shall be specified.");
            continue;
          }
        }

        // Process application
        Application app = ProcessApp(record);

        Module module = null;
        SubModule subModule = null;
        Release foundRelease = null;
        Release targetRelease = null;
        // Process some fields only if application is specified
        if( app != null )
        {
          // Process module
          module = ProcessModule(record, app);
          // Don't process sub module if module is null
          if (module != null)
          {
            // Process sub
            subModule = ProcessSubModule(record, module);
          }

          // Process found release
          foundRelease = ProcessRelease(record, app, FoundRelCol);
          // Process target release
          targetRelease = ProcessRelease(record, app, TargetRelCol);
        }

        // Process contributor
        Person contributor = ProcessPerson(record, ContributorCol);
        // Process team leader
        Person leader = ProcessPerson(record, LeaderCol);
        // Process developer
        Person developer = ProcessPerson(record, DevCol);
        // Process tester
        Person tester = ProcessPerson(record, QaCol);

        // Process revision
      }
    }

    #endregion Public Methods

    #region Helper Methods
    /// <summary>
    /// Processes application of the revision.
    /// </summary>
    private Application ProcessApp(Record record)
    {
      string appTitle;
      Application app = null;
      // If specified for record
      if( record.TryGetValue(AppCol, out appTitle) && appTitle != null )
      {
        // Check that app already added to cache
        // and, correspondingly, to database
        // Add to DB new one overwise
        if( !m_appCache.TryGetValue(appTitle, out app) )
        {
          // Create new DTO
          app = new Application {Title = appTitle};
          // Add to DB (we get at least it's ID)
          app = m_provider.CreateApplicaton(app);
          // Add to cache
          m_appCache.Add(app.Title, app);
        }
      }
      return app;
    }

    /// <summary>
    /// Processes module of application.
    /// </summary>
    private Module ProcessModule(Record record, Application app)
    {
      Debug.Assert(app != null); // application shall be specified

      string title;
      Module module = null;
      // If specified for record
      if (app != null && record.TryGetValue(ModuleCol, out title) && title != null)
      {
        // Key for cache is used to distinguish modules
        // with the same titles for different applications
        ComplexKey key = new ComplexKey(app.Id, title);
        // Check that module already added to cache
        // and, correspondingly, to database
        // Add to DB new one overwise
        if (!m_moduleCache.TryGetValue(key, out module))
        {
          // Create new DTO
          module = new Module { Title = title, AppId = app.Id };
          // Add to DB (we get at least it's ID)
          module = m_provider.CreateModule(module);
          // Add to cache
          m_moduleCache.Add(key, module);
        }
      }
      return module;
    }

    /// <summary>
    /// Processes sub module of module.
    /// </summary>
    private SubModule ProcessSubModule(Record record, Module module)
    {
      Debug.Assert(module != null); // application shall be specified

      string title;
      SubModule subModule = null;
      // If specified for the record
      if (module != null && record.TryGetValue(SubModuleCol, out title) && title != null)
      {
        // Key for cache is used to distinguish submodules
        // with the same titles for different modules
        ComplexKey key = new ComplexKey(module.Id, title);
        // Check that sub module already added to cache
        // and, correspondingly, to database
        // Add to DB new one overwise
        if (!m_subModuleCache.TryGetValue(key, out subModule))
        {
          // Create new DTO
          subModule = new SubModule { Title = title, ModuleId = module.Id };
          // Add to DB (we get at least it's ID)
          subModule = m_provider.CreateSubModule(subModule);
          // Add to cache
          m_subModuleCache.Add(key, subModule);
        }
      }
      return subModule;
    }

    /// <summary>
    /// Processes specified release of the revision.
    /// </summary>
    private Release ProcessRelease(Record record, Application app, string releaseColumnName)
    {
      string relTitle;
      Release rel = null;
      // Process release if specified for the revision
      // (app should be specified also)
      if( app != null && record.TryGetValue(releaseColumnName, out relTitle) &&
          relTitle != null )
      {
        // Trim trailing and ending quotes
        relTitle = relTitle.Trim('"');
        // Key for cache is used to distinguish releases
        // with the same titles for different applications
        ComplexKey key = new ComplexKey(app.Id, relTitle);
        // Check if already in cache
        if( !m_relCache.TryGetValue(key, out rel) )
        {
          // Create new DTO
          rel = new Release {Title = relTitle, AppId = app.Id};
          // Add to DB
          rel = m_provider.CreateRelease(rel);
          // Add to cache
          m_relCache.Add(key, rel);
        }
      }
      return rel;
    }

    /// <summary>
    /// Handles person information.
    /// </summary>
    private Person ProcessPerson(Record record, string column)
    {
      string login;
      Person person = null;
      // Process person if specified for the revision
      if (record.TryGetValue(column, out login) && login != null)
      {
        // Check if already in cache
        if (!m_personCache.TryGetValue(login, out person))
        {
          // Create new DTO
          person = new Person { Login = login, Title = "dummy" };
          // Add to DB
          person = m_provider.CreatePerson(person);
          // Add to cache
          m_personCache.Add(login, person);
        }
      }
      return person;
    }
    #endregion Helper Methods
  }

  /// <summary>
  /// Complex key for dictionaries.
  /// </summary>
  internal class ComplexKey : IEquatable<ComplexKey>
  {
    #region Private Fields
    private Object[] m_values;
    #endregion Private Fields

    #region Constructors
    public ComplexKey(params object[] values)
    {
      m_values = values;
    }
    #endregion Constructors

    #region Overrides
    public bool Equals(ComplexKey other)
    {
      if( ReferenceEquals(null, other) )
      {
        return false;
      }
      if( ReferenceEquals(this, other) )
      {
        return true;
      }
      return AreEquals(m_values, other.m_values);
    }

    public override bool Equals(object obj)
    {
      if( ReferenceEquals(null, obj) )
      {
        return false;
      }
      if( ReferenceEquals(this, obj) )
      {
        return true;
      }
      if( obj.GetType() != typeof(ComplexKey) )
      {
        return false;
      }
      return Equals((ComplexKey)obj);
    }

    private static bool AreEquals(object[] rh, object[] lh)
    {
      if (rh.Length != lh.Length)
      {
        return false;
      }
      bool eq = true;
      for (int i = 0; i < rh.Length; i++)
      {
        eq &= Object.Equals(rh[i], lh[i]);
      }
      return eq;
    }

    public override int GetHashCode()
    {
      int hash = 1;
      foreach( object obj in m_values )
      {
        hash ^= obj.GetHashCode();
      }
      return hash;
    }

    public static bool operator ==(ComplexKey left, ComplexKey right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(ComplexKey left, ComplexKey right)
    {
      return !Equals(left, right);
    }

    public override string ToString()
    {
      StringBuilder builder = new StringBuilder();
      builder.Append("{");
      foreach( object obj in m_values )
      {
        if( builder.Length != 1 )
        {
          builder.Append(", ");
        }
        builder.Append(obj);
      }
      builder.Append("}");
      return builder.ToString();
    }
    #endregion Overrides
  }
}
