using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.DataAccessLayer.Utils;
using BugDB.QueryParser;

namespace BugDB.Aggregator
{
  using Record = IDictionary<string, string>;

  /// <summary>
  /// Processes BugDB query results and 
  /// fills database.
  /// </summary>
  public class DbAggregator
  {
    #region Private Fields

    private const string ConnStringSetting = "ConnectionString";
    private const string CreateDbScript = @"DatabaseScripts\BugDB3.sql";

    private const string NumCol = "number";
    private const string RevCol = "subnumber";
    private const string TypeCol = "deadline";
    private const string StatusCol = "status";
    private const string DateCol = "date";
    private const string AppCol = "application";
    private const string ModuleCol = "modul";
    private const string SubModuleCol = "submodul";
    private const string FoundRelCol = "apprelease";
    private const string TargetRelCol = "frelease";
    private const string SevCol = "severity";
    private const string PriorCol = "priority";
    private const string ContrCol = "contributor";
    private const string LeaderCol = "leader";
    private const string DevCol = "developer";
    private const string QaCol = "qa";
    private const string SummaryCol = "summary";

    private IDataProvider m_provider;
    private IDictionary<string, Application> m_appCache;
    private IDictionary<string, Release> m_relCache;

    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Constracts aggregator for specified provider.
    /// </summary>
    public DbAggregator(IDataProvider provider)
    {
      m_provider = provider;
    }
    #endregion Constructors

    #region Public Methods
    /// <summary>
    /// Reads query results from stream and fills database.
    /// </summary>
    public void FillDatabase(Stream stream)
    {
      // First create database
      CreateDatabase();

      // Create records enumerator from stream
      IEnumerator<Record> records = QueryResultParser.CreateRecordsEnumerator(stream);

      // Initialize caches
      m_appCache = new Dictionary<string, Application>();
      m_relCache = new Dictionary<string, Release>();

      // Enumerate records (actually revisions)
      while( records.MoveNext() )
      {
        Record record = records.Current;

        // Process application
        Application app = ProcessApp(record);

        // Process found release
        ProcessRelease(record, app, FoundRelCol);
        // Process target release
        ProcessRelease(record, app, TargetRelCol);
      }
    }

    #endregion Public Methods

    #region Helper Methods
    /// <summary>
    /// Creates database.
    /// </summary>
    /// <remarks>
    /// Executes database creations script.
    /// If database already exists then it will be removed. 
    /// </remarks>
    private static void CreateDatabase()
    {
      string connString = ConfigurationManager.AppSettings[ConnStringSetting];
      SqlScriptRunner runner = new SqlScriptRunner(connString);
      string scriptPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      scriptPath = Path.Combine(scriptPath, CreateDbScript);
      runner.Execute(scriptPath);
    }

    /// <summary>
    /// Processes application of the revision.
    /// </summary>
    private Application ProcessApp(Record record)
    {
      string appTitle;
      Application app = null;
      // If specified for record
      if (record.TryGetValue(AppCol, out appTitle) && appTitle != null)
      {
        // Check that app already added to cache
        // and, correspondingly, to database
        // Add to DB new one overwise
        if (!m_appCache.TryGetValue(appTitle, out app))
        {
          // Create new DTO
          app = new Application { Title = appTitle };
          // Add to DB (we get at least it's ID)
          app = m_provider.CreateApplicaton(app);
          // Add to cache
          m_appCache.Add(app.Title, app);
        }
      }
      return app;
    }

    /// <summary>
    /// Processes specified release of the revision.
    /// </summary>
    private Release ProcessRelease(Record record, Application app, string releaseColumnName)
    {
      string relTitle;
      Release rel = null;
      // Process release is specified for the revision
      // (app should be specified also)
      if (app != null && record.TryGetValue(releaseColumnName, out relTitle) && relTitle != null)
      {
        if (!m_relCache.TryGetValue(relTitle, out rel))
        {
          // Create new DTO
          rel = new Release { Title = relTitle, AppId = app.Id };
          // Add to DB
          rel = m_provider.CreateRelease(rel);
          // Add to cache
          m_relCache.Add(rel.Title, rel);
        }
      }
      return rel;
    }
    #endregion Helper Methods
  }
}
