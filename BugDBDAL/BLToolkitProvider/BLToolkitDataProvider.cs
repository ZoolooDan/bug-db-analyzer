using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using BLToolkit.Data;

using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.DataAccessLayer.Utils;

using EDM = BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel;
using DTO = BugDB.DataAccessLayer.DataTransferObjects;

namespace BugDB.DataAccessLayer.BLToolkitProvider
{
  public class BLToolkitDataProvider : IDataProvider
  {
    #region Private Fields
    private static ObjectMapper<DTO.Bug, EDM.Bug> s_bugCopier = new ObjectMapper<DTO.Bug, EDM.Bug>();
    private static ObjectMapper<DTO.Application, EDM.Application> s_appCopier = new ObjectMapper<DTO.Application, EDM.Application>();
    private static ObjectMapper<DTO.Module, EDM.Module> s_moduleCopier = new ObjectMapper<DTO.Module, EDM.Module>();
    private static ObjectMapper<DTO.SubModule, EDM.SubModule> s_subModuleCopier = new ObjectMapper<DTO.SubModule, EDM.SubModule>();
    private static ObjectMapper<DTO.Release, EDM.Release> s_relCopier = new ObjectMapper<DTO.Release, EDM.Release>();
    private static ObjectMapper<DTO.Person, EDM.Person> s_personCopier = new ObjectMapper<DTO.Person, EDM.Person>();
    private static ObjectMapper<DTO.Revision, EDM.Revision> s_revisionCopier = new ObjectMapper<DTO.Revision, EDM.Revision>();

    private string m_createDbScriptPath;
    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Creates provider without possibility to create DB.
    /// </summary>
    public BLToolkitDataProvider()
      : this(null)
    {
    }

    /// <summary>
    /// Constructs provider with specific DB create string.
    /// </summary>
    public BLToolkitDataProvider(string createDbScriptPath)
    {
      m_createDbScriptPath = createDbScriptPath;
    }
    #endregion Constructors

    #region Implementation of IDataProvider

    ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Initializes underlying storage.
    /// </summary>
    /// <remarks>
    /// Storage will be created and initialized or 
    /// recreated if existed before.
    ///
    /// This implementation uses <see cref="SqlScriptRunner"/> to 
    /// execute DB creation script.
    /// 
    /// DB creation script shall be specified during 
    /// provider creation.
    /// </remarks>
    public void InitializeStorage()
    {
      if(m_createDbScriptPath == null)
        throw new Exception("Database creation script wasn't specified");

      string connString;
      // Use DbManager.Connection to access connection string
      using(DbManager db = new DbManager())
      {
        connString = db.Connection.ConnectionString;
      }
      // Use script runner to execute script
      SqlScriptRunner runner = new SqlScriptRunner(connString);
      runner.Execute(m_createDbScriptPath);
    }

    /// <summary>
    /// Clean already initialized storage.
    /// </summary>
    /// <remarks>
    /// Storage shall be already created.
    /// It is just cleaned up, e.g. everything is
    /// deleted from all tables.
    /// </remarks>
    public void CleanStorage()
    {
      // Insert application
      using(DbManager db = new DbManager())
      {
        db.SetSpCommand("Storage_Clean", null);
        db.ExecuteNonQuery();
      }
    }

    ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Creates new application.
    /// </summary>
    public DTO.Application CreateApplicaton(DTO.Application appDTO)
    {
      // Map DTO to EDM
      EDM.Application appEDM = s_appCopier.Copy(appDTO);

      // Insert application
      using( DbManager db = new DbManager() )
      {
        db.SetCommand(
          @"
          INSERT INTO Applications (app_title) VALUES (@app_title) 
          SELECT Cast(SCOPE_IDENTITY() as int) app_id",
          db.CreateParameters(appEDM, new[] {"app_id"}, null, null)).
          ExecuteObject(appEDM);
      }

      // Map EDM to DTO
      appDTO = s_appCopier.Copy(appEDM);

      return appDTO;
    }

    /// <summary>
    /// Returns all applications.
    /// </summary>
    public DTO.Application[] GetApplications()
    {
      List<EDM.Application> appEDMs;
      using (DbManager db = new DbManager())
      {
        appEDMs = db.SetCommand(@"SELECT * FROM Applications").
          ExecuteList<EDM.Application>();
      }

      List<DTO.Application> appDTOs = new List<DTO.Application>(appEDMs.Count);
      // Copy EDMs to DTOs
      appEDMs.ForEach(appEDM => appDTOs.Add(s_appCopier.Copy(appEDM)));

      return appDTOs.ToArray();
    }

    /// <summary>
    /// Returns application by ID.
    /// </summary>
    public DTO.Application GetApplication(int appId)
    {
      EDM.Application appEDM;
      using( DbManager db = new DbManager() )
      {
        appEDM = db.SetCommand(
          @"SELECT * FROM Applications WHERE app_id = @AppId",
          db.InputParameter("@AppId", appId)).
          ExecuteObject<EDM.Application>();
      }

      // Copy EDMs to DTOs
      DTO.Application appDTO = s_appCopier.Copy(appEDM);

      return appDTO;
    }

    ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Gets all possible bug types.
    /// </summary>
    public BugType[] GetTypes()
    {
      return new[]
               {
                 BugType.Bug,
                 BugType.Feature
               };
    }

    /// <summary>
    /// Gets all possible statuses.
    /// </summary>
    public BugStatus[] GetStatuses()
    {
      return new[]
               {
                 BugStatus.None, 
                 BugStatus.Open,
                 BugStatus.Invalid,
                 BugStatus.Duplicate,
                 BugStatus.Analyzed,
                 BugStatus.Suspend,
                 BugStatus.ToBeAssigned,
                 BugStatus.Assigned,
                 BugStatus.WorksForMe,
                 BugStatus.Active,
                 BugStatus.Fixed,
                 BugStatus.Reopen,
                 BugStatus.Modeling,
                 BugStatus.Regression,
                 BugStatus.TestCases,
                 BugStatus.Verified,
                 BugStatus.Closed,
                 BugStatus.Deleted
               };
    }

    /// <summary>
    /// Gets all possible severities.
    /// </summary>
    public BugSeverity[] GetSeverities()
    {
      return new[]
               {
                 BugSeverity.Fatal,
                 BugSeverity.Critical,
                 BugSeverity.Serious,
                 BugSeverity.Minor,
                 BugSeverity.Sev5,
                 BugSeverity.Sev6,
                 BugSeverity.Sev7,
                 BugSeverity.Sev8,
                 BugSeverity.Sev9
               };
    }

    /// <summary>
    /// Gets all possible priorities.
    /// </summary>
    public int[] GetPriorities()
    {
      return new [] {1, 2, 3, 4, 5, 6, 7, 8, 9};
    }

    ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Creates new release.
    /// </summary>
    public DTO.Release CreateRelease(DTO.Release relDTO)
    {
      // Map DTO to EDM
      EDM.Release relEDM = s_relCopier.Copy(relDTO);

      // Insert application
      using (DbManager db = new DbManager())
      {
        db.SetCommand(
          @"
          INSERT INTO Releases (release_title, app_id) VALUES (@release_title, @app_id) 
          SELECT Cast(SCOPE_IDENTITY() as int) release_id",
          db.CreateParameters(relEDM, new[] { "release_id" }, null, null)).
          ExecuteObject(relEDM);
      }

      // Map EDM to DTO
      relDTO = s_relCopier.Copy(relEDM);

      return relDTO;
    }

    /// <summary>
    /// Returns all releases of the specified application.
    /// </summary>
    public DTO.Release[] GetReleases(int appId)
    {
      List<EDM.Release> relEDMs;
      using (DbManager db = new DbManager())
      {
        relEDMs = db.SetCommand(@"SELECT * FROM Releases WHERE app_id = @AppId",
          db.InputParameter("@AppId", appId)).
          ExecuteList<EDM.Release>();
      }

      // TODO: Add method to copy lists to copier
      List<DTO.Release> relDTOs = new List<DTO.Release>(relEDMs.Count);
      // Copy EDMs to DTOs
      relEDMs.ForEach(relEDM => relDTOs.Add(s_relCopier.Copy(relEDM)));

      return relDTOs.ToArray();
    }

    ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Creates new module.
    /// </summary>
    public DTO.Module CreateModule(DTO.Module moduleDTO)
    {
      EDM.Module moduleEDM = s_moduleCopier.Copy(moduleDTO);

      using (DbManager db = new DbManager())
      {
        db.SetCommand(
          @"INSERT INTO Modules (app_id, module_title) 
            VALUES (@app_id, @module_title)
            SELECT Cast(SCOPE_IDENTITY() as int) module_id",
          db.CreateParameters(moduleEDM, new[] { "module_id" }, null, null)).
          ExecuteObject(moduleEDM);
      }

      return s_moduleCopier.Copy(moduleEDM);
    }

    /// <summary>
    /// Returns all modules of specific application.
    /// </summary>
    public DTO.Module[] GetModules(int appId)
    {
      List<EDM.Module> moduleEDMs;
      using (DbManager db = new DbManager())
      {
        moduleEDMs = db.SetCommand(@"SELECT * FROM Modules WHERE app_id=@AppId",
          db.InputParameter("@AppId", appId)).
          ExecuteList<EDM.Module>();
      }

      // TODO: Add method to copy lists to copier
      List<DTO.Module> moduleDTOs = new List<DTO.Module>(moduleEDMs.Count);
      // Copy EDMs to DTOs
      moduleEDMs.ForEach(moduleEDM => moduleDTOs.Add(s_moduleCopier.Copy(moduleEDM)));

      return moduleDTOs.ToArray();
    }

    ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Creates new sub module.
    /// </summary>
    public DTO.SubModule CreateSubModule(DTO.SubModule subModuleDTO)
    {
      EDM.SubModule subModuleEDM = s_subModuleCopier.Copy(subModuleDTO);

      using (DbManager db = new DbManager())
      {
        db.SetCommand(
          @"INSERT INTO Submodules (module_id, submodule_title) 
            VALUES (@module_id, @submodule_title)
            SELECT Cast(SCOPE_IDENTITY() as int) submodule_id",
          db.CreateParameters(subModuleEDM, new[] { "submodule_id" }, null, null)).
          ExecuteObject(subModuleEDM);
      }

      return s_subModuleCopier.Copy(subModuleEDM);
    }

    /// <summary>
    /// Returns all sub modules of specific module.
    /// </summary>
    public DTO.SubModule[] GetSubModules(int moduleId)
    {
      List<EDM.SubModule> subModuleEDMs;
      using (DbManager db = new DbManager())
      {
        subModuleEDMs = db.SetCommand(
          @"SELECT * FROM Submodules WHERE module_id=@ModuleId",
          db.InputParameter("@ModuleId", moduleId)).
          ExecuteList<EDM.SubModule>();
      }

      // TODO: Add method to copy lists to copier
      List<DTO.SubModule> subModuleDTOs = new List<DTO.SubModule>(subModuleEDMs.Count);
      // Copy EDMs to DTOs
      subModuleEDMs.ForEach(subModuleEDM => 
        subModuleDTOs.Add(s_subModuleCopier.Copy(subModuleEDM)));

      return subModuleDTOs.ToArray();
    }

    ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Creates new person.
    /// </summary>
    public DTO.Person CreatePerson(DTO.Person personDTO)
    {
      EDM.Person personEDM = s_personCopier.Copy(personDTO);

      using(DbManager db = new DbManager())
      {
        db.SetCommand(
          @"INSERT INTO Staff (person_login, person_title) 
            VALUES (@person_login, @person_title)
            SELECT Cast(SCOPE_IDENTITY() as int) person_id",
          db.CreateParameters(personEDM, new[] {"person_id"}, null, null)).
          ExecuteObject(personEDM);
      }

      return s_personCopier.Copy(personEDM);
    }

    /// <summary>
    /// Return all persons.
    /// </summary>
    public DTO.Person[] GetStaff()
    {
      List<EDM.Person> personEDMs;
      using (DbManager db = new DbManager())
      {
        personEDMs = db.SetCommand(@"SELECT * FROM Staff").
          ExecuteList<EDM.Person>();
      }

      // TODO: Add method to copy lists to copier
      List<DTO.Person> personDTOs = new List<DTO.Person>(personEDMs.Count);
      // Copy EDMs to DTOs
      personEDMs.ForEach(personEDM => personDTOs.Add(s_personCopier.Copy(personEDM)));

      return personDTOs.ToArray();
    }

    ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Returns all bugs.
    /// </summary>
    public DTO.Bug[] GetBugs()
    {
      List<EDM.Bug> bugEDMs;
      using (DbManager db = new DbManager())
      {
        bugEDMs = db.SetCommand(@"SELECT * FROM Bugs ORDER BY bug_number").
          ExecuteList<EDM.Bug>();
      }

      List<DTO.Bug> bugDTOs = new List<DTO.Bug>(bugEDMs.Count);
      // Copy EDMs to DTOs
      bugEDMs.ForEach(bugEDM => bugDTOs.Add(s_bugCopier.Copy(bugEDM)));

      return bugDTOs.ToArray();
    }

    ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Creates new revision.
    /// </summary>
    public Revision CreateRevision(Revision revisionDTO)
    {
      // Map DTO to EDM
      EDM.Revision relEDM = s_revisionCopier.Copy(revisionDTO);

      // Insert application
      using (DbManager db = new DbManager())
      {
        db.SetSpCommand("Revision_Create",
          db.CreateParameters(relEDM)).
          ExecuteObject(relEDM);
      }

      // Map EDM to DTO
      revisionDTO = s_revisionCopier.Copy(relEDM);

      return revisionDTO;
    }

    /// <summary>
    /// Returns all revisions of the specifi bug.
    /// </summary>
    public DTO.Revision[] GetRevisions(int bugNumber)
    {
      List<EDM.Revision> revEDMs;
      using (DbManager db = new DbManager())
      {
        revEDMs = db.SetCommand(@"
            SELECT bug_number, revision, date, bug_type, app_id, module_id, 
            submodule_id, status, found_release_id, target_release_id, 
            severity, priority, contributor_id, leader_id, developer_id, 
            qa_id, S.summary_text AS summary 
            FROM Revisions AS R INNER JOIN Summaries AS S ON R.summary_id = S.summary_id
            WHERE bug_number=@BugNumber
            ORDER BY revision",
            db.InputParameter("@BugNumber", bugNumber)).
          ExecuteList<EDM.Revision>();


      }

      List<DTO.Revision> revDTOs = new List<DTO.Revision>(revEDMs.Count);
      // Copy EDMs to DTOs
      revEDMs.ForEach(revEDM => revDTOs.Add(s_revisionCopier.Copy(revEDM)));

      return revDTOs.ToArray();
    }

    /// <summary>
    /// Returns revisions satisfying query parameters.
    /// </summary>
    /// <remarks>
    /// Most recent or all revisions of bug are searched
    /// depending on <see cref="QueryParams.IncludeHistory"/>.
    /// If at least one searched revision satisfy query
    /// then all revisions of bug are returned.
    /// 
    /// It is allowed to specify only part of parameters. 
    /// In that case only they will be accouneted in query.
    /// 
    /// All revisions are returned for bug because it is
    /// analysis of status transition may be erronous overwise.
    /// </remarks>
    public Revision[] GetRevisions(QueryParams prms)
    {
      // -- In recent
      // SELECT * FROM Revisions
      // WHERE bug_number IN
      //  (SELECT B.bug_number FROM Bugs AS B INNER JOIN Revisions AS R 
      //     ON B.bug_number = R.bug_number AND 
      //        B.recent_revision = R.revision
      //   WHERE <condition>)
      // ORDER BY bug_number, revision
      //
      // -- With history
      // SELECT * FROM Revisions 
      // WHERE bug_number IN 
      //   (SELECT DISTINCT bug_number FROM Revisions AS R 
      //    WHERE <condition>)
      // ORDER BY bug_number, revision


      List<EDM.Revision> revEDMs;
      using (DbManager db = new DbManager())
      {
        var dbPrms = new List<IDbDataParameter>();

        // Create subordinate query command depending on 
        // whether to include history of revisions or 
        // process only recent one
        string subQueryBase = prms.IncludeHistory ?
          @"SELECT DISTINCT bug_number FROM Revisions AS R" :
          @"SELECT B.bug_number FROM Bugs AS B INNER JOIN Revisions AS R 
              ON B.bug_number = R.bug_number AND 
                 B.recent_revision = R.revision";

        StringBuilder condition = new StringBuilder();
        // Bug numbers
        if( prms.BugNumberMin.HasValue )
        {
          AppendSimpleCondition(condition, "R.bug_number >= @BugNumberMin");
          dbPrms.Add(db.InputParameter("@BugNumberMin", 
            prms.BugNumberMin.Value));
        }
        if( prms.BugNumberMax.HasValue )
        {
          AppendSimpleCondition(condition, "R.bug_number <= @BugNumberMax");
          dbPrms.Add(db.InputParameter("@BugNumberMax", 
            prms.BugNumberMax.Value));
        }

        // Types
        if( prms.BugTypes != null && prms.BugTypes.Length != 0 )
        {
          AppendInCondition(condition, "R.bug_type", 
            (from t in prms.BugTypes select (int)t).ToArray());
        }

        // Applications
        if( prms.Apps != null && prms.Apps.Length != 0 )
        {
          AppendInCondition(condition, "R.app_id", prms.Apps);
        }

        // Modules
        if( prms.Modules != null && prms.Modules.Length != 0 )
        {
          AppendInCondition(condition, "R.module_id", prms.Modules);
        }

        // Sub modules
        if( prms.SubModules != null && prms.SubModules.Length != 0 )
        {
          AppendInCondition(condition, "R.submodule_id", prms.SubModules);
        }

        // Found Releases
        if( prms.FoundReleases != null && prms.FoundReleases.Length != 0 )
        {
          AppendInCondition(condition, "R.found_release_id", prms.FoundReleases);
        }

        // Target Releases
        if( prms.TargetReleases != null && prms.TargetReleases.Length != 0 )
        {
          AppendInCondition(condition, "R.target_release_id", prms.TargetReleases);
        }

        // Statuses
        if( prms.BugStatuses != null && prms.BugStatuses.Length != 0 )
        {
          AppendInCondition(condition, "R.status", 
            (from s in prms.BugStatuses select (int)s).ToArray());
        }

        // Severities
        if( prms.Severities != null && prms.Severities.Length != 0 )
        {
          AppendInCondition(condition, "R.severity", 
            (from s in prms.Severities select (int)s).ToArray());
        }

        // Priorities
        if( prms.Priorities != null && prms.Priorities.Length != 0 )
        {
          AppendInCondition(condition, "R.priority", prms.Priorities);
        }

        // Contributors
        if( prms.Contributors != null && prms.Contributors.Length != 0 )
        {
          AppendInCondition(condition, "R.contributor_id", prms.Contributors);
        }

        // Team Leaders
        if( prms.TeamLeaders != null && prms.TeamLeaders.Length != 0 )
        {
          AppendInCondition(condition, "R.leader_id", prms.TeamLeaders);
        }

        // Developers
        if( prms.Developers != null && prms.Developers.Length != 0 )
        {
          AppendInCondition(condition, "R.developer_id", prms.Developers);
        }

        // Testers
        if( prms.Testers != null && prms.Testers.Length != 0 )
        {
          AppendInCondition(condition, "R.qa_id", prms.Testers);
        }

        // From Date
        if( prms.DateFrom.HasValue && prms.DateFrom != DateTime.MinValue )
        {
          AppendSimpleCondition(condition, "R.date >= @FromDate");
          dbPrms.Add(db.InputParameter("@FromDate", prms.DateFrom.Value));
        }
        // To Date
        if( prms.DateTo.HasValue && prms.DateTo != DateTime.MinValue )
        {
          AppendSimpleCondition(condition, "R.date <= @ToDate");
          dbPrms.Add(db.InputParameter("@ToDate", prms.DateTo.Value));
        }

        // Summaries
        if( prms.Summary != null )
        {
          string trimmed = prms.Summary.Trim();
          // Process summary only if it's not empty string
          if (trimmed.Length != 0)
          {
            AppendSimpleCondition(condition, "R.summary_text LIKE @Summary");
            dbPrms.Add(db.InputParameter("@Summary", trimmed));
          }
        }

        // Final query
        string query = String.Format(
          @"SELECT * FROM Revisions AS R INNER JOIN Summaries AS S
            ON R.summary_id = S.summary_id 
            WHERE bug_number IN ({0}{1}{2}) 
            ORDER BY bug_number, revision", 
          subQueryBase,
          condition.Length != 0 ? " WHERE " : "",
          condition);

        // Execute
        revEDMs = db.SetCommand(query, dbPrms.ToArray()).
          ExecuteList<EDM.Revision>();
      }

      List<DTO.Revision> revDTOs = new List<DTO.Revision>(revEDMs.Count);
      // Copy EDMs to DTOs
      revEDMs.ForEach(revEDM => revDTOs.Add(s_revisionCopier.Copy(revEDM)));

      return revDTOs.ToArray();
    }


#endregion

    #region Helper Methods
    /// <summary>
    /// Appends simple logical AND condition.
    /// </summary>
    private static void AppendSimpleCondition(StringBuilder builder, string condition)
    {
      // Add consequent condition with AND
      if( builder.Length != 0 )
      {
        builder.Append(" AND ");
      }
      builder.Append(condition);
    }

    /// <summary>
    /// Appends IN condition for ints.
    /// </summary>
    private static void AppendInCondition(StringBuilder builder, string field, int[] values)
    {
      // Add consequent condition with AND
      if( builder.Length != 0 )
      {
        builder.Append(" AND ");
      }
      builder.Append(field);
      builder.Append(" IN (");
      for( int i = 0; i < values.Length; i++ )
      {
        if( i > 0 )
        {
          builder.Append(",");
        }
        builder.Append(values[i]);
      }
      builder.Append(")");
    }
    #endregion Helper Methods
  }
}