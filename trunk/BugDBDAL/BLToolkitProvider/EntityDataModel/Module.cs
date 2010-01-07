using BLToolkit.Mapping;


namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// EDM object for working with Modules table.
  /// </summary>
  public class Module
  {
    /// <summary>
    /// Unique ID of the module.
    /// </summary>
    [MapField("module_id")]
    public int Id { get; set; }

    /// <summary>
    /// ID of parent application.
    /// </summary>
    [MapField("app_id")]
    public int AppId { get; set; }

    /// <summary>
    /// Title of the module.
    /// </summary>
    [MapField("module_title")]
    public string Title { get; set; }
  }
}