using BLToolkit.Mapping;


namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// EDM object for working with Submodules table.
  /// </summary>
  public class SubModule
  {
    /// <summary>
    /// Unique ID of the module.
    /// </summary>
    [MapField("submodule_id")]
    public int Id { get; set; }

    /// <summary>
    /// ID of parent module.
    /// </summary>
    [MapField("module_id")]
    public int ModuleId { get; set; }

    /// <summary>
    /// Title of the module.
    /// </summary>
    [MapField("submodule_title")]
    public string Title { get; set; }
  }
}