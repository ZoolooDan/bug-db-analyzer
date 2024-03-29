﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;

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
    private const string PriorityCol = "priority";
    private const string ContributorCol = "contributor";
    private const string LeaderCol = "leader";
    private const string DevCol = "developer";
    private const string QaCol = "qa";
    private const string SummaryCol = "summary";

    private const string BuggType = "bug";
    private const string FeatureType = "feature";

    private const string OpenStatus = "open";
    private const string InvalidStatus = "invalid";
    private const string DuplicateStatus = "duplicate";
    private const string AnalyzedStatus = "analysed";
    private const string SuspendStatus = "suspend";
    private const string ToBeAssStatus = "to_be_assigned";
    private const string AssignedStatus = "assigned";
    private const string WorksForMeStatus = "works_for_me";
    private const string ActiveStatus = "active";
    private const string FixedStatus = "fixed";
    private const string ReopenStatus = "reopen";
    private const string ModelingStatus = "modelling";
    private const string RegressionStatus = "regression";
    private const string VerifiedStatus = "verified";
    private const string TestCasesStatus = "test_cases";
    private const string ClosedStatus = "closed";
    private const string DeletedStatus = "deleted";

    private static readonly IDictionary<string, BugType> s_typeMappings;
    private static readonly IDictionary<string, BugStatus> s_statusMappings;
    private static readonly IDictionary<string, BugSeverity> s_severityMappings;

    private IDataProvider m_provider;
    private IDictionary<string, Application> m_appCache;
    private IDictionary<ComplexKey, Module> m_moduleCache;
    private IDictionary<ComplexKey, SubModule> m_subModuleCache;
    private IDictionary<ComplexKey, Release> m_relCache;
    private IDictionary<string, Person> m_personCache;
    private int m_recordsCount = -1;

    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Constructs static state.
    /// </summary>
    static StorageAggregator()
    {
      s_typeMappings = new Dictionary<string, BugType>();
      s_typeMappings.Add(BuggType, BugType.Bug);
      s_typeMappings.Add(FeatureType, BugType.Feature);

      s_statusMappings = new Dictionary<string, BugStatus>();
      s_statusMappings.Add(OpenStatus, BugStatus.Open);
      s_statusMappings.Add(InvalidStatus, BugStatus.Invalid);
      s_statusMappings.Add(DuplicateStatus, BugStatus.Duplicate);
      s_statusMappings.Add(AnalyzedStatus, BugStatus.Analyzed);
      s_statusMappings.Add(SuspendStatus, BugStatus.Suspend);
      s_statusMappings.Add(ToBeAssStatus, BugStatus.ToBeAssigned);
      s_statusMappings.Add(AssignedStatus, BugStatus.Assigned);
      s_statusMappings.Add(WorksForMeStatus, BugStatus.WorksForMe);
      s_statusMappings.Add(ActiveStatus, BugStatus.Active);
      s_statusMappings.Add(FixedStatus, BugStatus.Fixed);
      s_statusMappings.Add(ReopenStatus, BugStatus.Reopen);
      s_statusMappings.Add(ModelingStatus, BugStatus.Modeling);
      s_statusMappings.Add(TestCasesStatus, BugStatus.TestCases);
      s_statusMappings.Add(RegressionStatus, BugStatus.Regression);
      s_statusMappings.Add(VerifiedStatus, BugStatus.Verified);
      s_statusMappings.Add(ClosedStatus, BugStatus.Closed);
      s_statusMappings.Add(DeletedStatus, BugStatus.Deleted);

      s_severityMappings = new Dictionary<string, BugSeverity>();
      s_severityMappings.Add("1", BugSeverity.Fatal);
      s_severityMappings.Add("2", BugSeverity.Critical);
      s_severityMappings.Add("3", BugSeverity.Serious);
      s_severityMappings.Add("4", BugSeverity.Minor);
      s_severityMappings.Add("5", BugSeverity.Sev5);
      s_severityMappings.Add("6", BugSeverity.Sev6);
      s_severityMappings.Add("7", BugSeverity.Sev7);
      s_severityMappings.Add("8", BugSeverity.Sev8);
      s_severityMappings.Add("9", BugSeverity.Sev9);
    }

    /// <summary>
    /// Constracts aggregator for specified provider.
    /// </summary>
    public StorageAggregator(IDataProvider provider)
    {
      m_provider = provider;
    }
    #endregion Constructors

    #region Public Events
    /// <summary>
    /// Occurs when progress changed.
    /// </summary>
    public event EventHandler<AggregatorProgressArgs> ProgressChanged;
    #endregion Public Events

    #region Public Methods

    /// <summary>
    /// Reads query results from file and fills database.
    /// </summary>
    /// <remarks>
    /// See <see cref="FillStorage(TextReader)"/> for more details.
    /// </remarks>
    public void FillStorage(string dataPath)
    {
      // Count records
      using(TextReader reader = new StreamReader(dataPath))
      {
        m_recordsCount = CountRecords(reader);
      }

      // Fill storage
      using(TextReader reader = new StreamReader(dataPath))
      {
        FillStorage(reader);
      }
      m_recordsCount = -1;
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

      int current = 0;
      // Enumerate records (actually revisions)
      while( records.MoveNext() )
      {
        // Take current record from parser
        Record record = records.Current;

        // Process number
        int number = ProcessNumber(record);

        // Process revision number
        int revision = ProcessRevisionNumber(record);

        // Process short description
        string summary = ProcessSummary(record);

        // Process date
        DateTime date = ProcessDate(record);

        // Process record type (could be null)
        BugType type = ProcessType(record);

        // Process status
        BugStatus status = ProcessStatus(record);

        // Process severity
        BugSeverity? severity = ProcessSeverity(record);

        // Process priority
        int? priority = ProcessPriority(record);

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
        ProcessRevision(number, revision, date, type, status, 
          summary, severity, priority, app, module, 
          subModule, foundRelease, 
          targetRelease, contributor, leader, developer, tester);

        current++;
        // Notify progress
        FireProgressChanged(current);
      }
    }

    #endregion Public Methods

    #region Helper Methods
    /// <summary>
    /// Process bug number.
    /// </summary>
    private static int ProcessNumber(Record record)
    {
      int number;
      Debug.Assert(int.TryParse(record[NumCol], out number));
      return number;
    }

    /// <summary>
    /// Process revision number.
    /// </summary>
    private static int ProcessRevisionNumber(Record record)
    {
      int revision;
      // Process revision number
      Debug.Assert(int.TryParse(record[RevisionCol], out revision));
      return revision;
    }
    
    /// <summary>
    /// Process summary.
    /// </summary>
    private static string ProcessSummary(Record record)
    {
      return record[SummaryCol] ?? "";
    }

    /// <summary>
    /// Process record type.
    /// </summary>
    /// <remarks>
    /// It seems type can be null for old bugs.
    /// So in that case it is handled as Unspecified.
    /// </remarks>
    private static BugType ProcessType(Record record)
    {
      BugType type = BugType.Bug;
      string typeStr = record[TypeCol];
      bool found = typeStr != null ? s_typeMappings.TryGetValue(typeStr, out type) : false;
    
      return found ? type : BugType.Unspecified;
    }

    /// <summary>
    /// Process severity.
    /// </summary>
    /// <remarks>
    /// Can be null.
    /// </remarks>
    private static BugSeverity? ProcessSeverity(Record record)
    {
      BugSeverity value = BugSeverity.Fatal;
      string str = record[SeverityCol];
      bool found = str != null ? s_severityMappings.TryGetValue(str, out value) : false;

      return found ? new BugSeverity?(value) : null;
    }

    /// <summary>
    /// Process severity.
    /// </summary>
    /// <remarks>
    /// Can be null.
    /// </remarks>
    private static int? ProcessPriority(Record record)
    {
      int priority = 0;
      string str = record[PriorityCol];
      bool found = false;
      if (str != null)
      {
        found = int.TryParse(str, out priority);
      }
      return found ? (int?)priority : null;
    }

    /// <summary>
    /// Process status.
    /// </summary>
    private static BugStatus ProcessStatus(Record record)
    {
      BugStatus status = BugStatus.None;
      string str = record[StatusCol];
      bool found = str != null ? s_statusMappings.TryGetValue(str, out status) : false;
      
      return found ? status : BugStatus.None;
    }

    /// <summary>
    /// Process date.
    /// </summary>
    private static DateTime ProcessDate(Record record)
    {
      DateTime date;
      Debug.Assert(DateTime.TryParse(record[DateCol], out date));
      return date;
    }

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
      Debug.Assert(module != null); // module shall be specified

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

    /// <summary>
    /// Create revision based on provided information.
    /// </summary>
    private void ProcessRevision(int number, int revision, DateTime date,
      BugType type, BugStatus status, string summary,
      BugSeverity? severity, int? priority, Application app,
      Module module, SubModule subModule, Release foundRelease, Release targetRelease,
      Person contributor, Person leader, Person developer, Person tester)
    {
      // Check prerequisites
      if( app == null )
      {
        throw new Exception(
          String.Format("Application isn't specified for bug={0}, revision={1}", number, revision));
      }
      if( contributor == null )
      {
        throw new Exception(
          String.Format("Contributor isn't specified for bug={0}, revision={1}", number, revision));
      }

      Revision rev = new Revision
                     {
                       BugNumber = number,
                       Rev = revision,
                       Date = date,
                       Type = type,
                       Status = status,
                       Summary = summary,
                       Severity = severity,
                       Priority = priority,
                       AppId = app.Id,
                       ModuleId = module != null ? (int?)module.Id : null,
                       SubModuleId = subModule != null ? (int?)subModule.Id : null,
                       FoundReleaseId = foundRelease != null ? (int?)foundRelease.Id : null,
                       TargetReleaseId = targetRelease != null ? (int?)targetRelease.Id : null,
                       ContributorId = contributor.Id,
                       TeamLeaderId = leader != null ? (int?)leader.Id : null,
                       DeveloperId = developer != null ? (int?)developer.Id : null,
                       TesterId = tester != null ? (int?)tester.Id : null
                     };
      m_provider.CreateRevision(rev);
    }
    
    /// <summary>
    /// Fires ProgressChanged event.
    /// </summary>
    private void FireProgressChanged(int current)
    {
      if (ProgressChanged != null)
      {
        ProgressChanged(this, new AggregatorProgressArgs(current, m_recordsCount));
      }
    }

    /// <summary>
    /// Calculates number of records of data available.
    /// </summary>
    private static int CountRecords(TextReader reader)
    {
      int count = 0;
      // Create records enumerator from stream
      IEnumerator<Record> records = QueryResultParser.CreateRecordsEnumerator(reader);
      while( records.MoveNext() )
      {
        count++;
      }
      return count;
    }

    #endregion Helper Methods
  }

  /// <summary>
  /// Arguments for StorageAggregator ProgressChanged event.
  /// </summary>
  public class AggregatorProgressArgs : EventArgs
  {
    private int m_current;
    private int m_total;

    public AggregatorProgressArgs(int current, int total)
    {
      m_current = current;
      m_total = total;
    }

    public int Current
    {
      get { return m_current; }
    }

    public int Total
    {
      get { return m_total; }
    }
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
