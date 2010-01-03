using System;
using System.Collections.Generic;
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
    private static ObjectCopier<DTO.Application, EDM.Application> s_appCopier = new ObjectCopier<DTO.Application, EDM.Application>();
    private static ObjectCopier<DTO.Release, EDM.Release> s_relCopier = new ObjectCopier<DTO.Release, EDM.Release>();
    #endregion Private Fields

    #region Constructors
    #endregion Constructors

    #region Implementation of IDataProvider

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
      throw new NotImplementedException();
    }

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
    /// Returns all bugs.
    /// </summary>
    public DTO.Bug[] GetBugs()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns all revisions of the specifi bug.
    /// </summary>
    public DTO.Revision[] GetBugRevisions(int bugNumber)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns all releases of the specified application.
    /// </summary>
    public Release[] GetApplicationReleases(int appId)
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

    /// <summary>
    /// Creates new release.
    /// </summary>
    public Release CreateRelease(Release relDTO)
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

    #endregion
  }
}