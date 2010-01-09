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
    /// Creates new application.
    /// </summary>
    DTO.Application CreateApplicaton(DTO.Application app);

    /// <summary>
    /// Returns all applications.
    /// </summary>
    DTO.Application[] GetApplications();

    /// <summary>
    /// Returns application by ID.
    /// </summary>
    DTO.Application GetApplication(int appId);

    /// <summary>
    /// Creates new revision.
    /// </summary>
    Revision CreateRevision(Revision revision);

    /// <summary>
    /// Returns all bugs.
    /// </summary>
    DTO.Bug[] GetBugs();

    /// <summary>
    /// Returns all revisions of the specified bug.
    /// </summary>
    DTO.Revision[] GetBugRevisions(int bugNumber);

    /// <summary>
    /// Creates new release.
    /// </summary>
    Release CreateRelease(Release release);

    /// <summary>
    /// Returns all releases of the specified application.
    /// </summary>
    Release[] GetApplicationReleases(int appId);

    /// <summary>
    /// Creates new person.
    /// </summary>
    Person CreatePerson(Person person);

    /// <summary>
    /// Return all persons.
    /// </summary>
    Person[] GetStaff();

    /// <summary>
    /// Creates new module.
    /// </summary>
    Module CreateModule(Module module);

    /// <summary>
    /// Returns all modules of specific application.
    /// </summary>
    Module[] GetApplicationModules(int appId);

    /// <summary>
    /// Creates new sub module.
    /// </summary>
    SubModule CreateSubModule(SubModule module);

    /// <summary>
    /// Returns all sub modules of specific module.
    /// </summary>
    SubModule[] GetModuleSubModules(int moduleId);
  }
}