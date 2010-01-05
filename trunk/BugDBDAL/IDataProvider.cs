using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BugDB.DataAccessLayer.DataTransferObjects;

namespace BugDB.DataAccessLayer
{
  // Alias for the namespaces where 
  // Data Transfer Objects (DTO) are defined
  using DTO = DataTransferObjects;

  /// <summary>
  /// Interface for accessing objects from data layer.
  /// </summary>
  public interface IDataProvider
  {
    /// <summary>
    /// Initializes underlying storage.
    /// </summary>
    /// <remarks>
    /// Storage will be created and initialized or 
    /// recreated if existed before.
    /// </remarks>
    void InitializeStorage();

    /// <summary>
    /// Returns all applications.
    /// </summary>
    DTO.Application[] GetApplications();
    /// <summary>
    /// Returns application by ID.
    /// </summary>
    DTO.Application GetApplication(int appId);
    /// <summary>
    /// Creates new application.
    /// </summary>
    DTO.Application CreateApplicaton(DTO.Application app);

    /// <summary>
    /// Returns all bugs.
    /// </summary>
    DTO.Bug[] GetBugs();

    /// <summary>
    /// Returns all revisions of the specified bug.
    /// </summary>
    DTO.Revision[] GetBugRevisions(int bugNumber);

    /// <summary>
    /// Returns all releases of the specified application.
    /// </summary>
    Release[] GetApplicationReleases(int appId);

    /// <summary>
    /// Creates new release.
    /// </summary>
    Release CreateRelease(Release release);
  }
}
