using BLToolkit.Mapping;

namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// EDM object for working with Applications table.
  /// </summary>
  public class Application
  {
    /// <summary>
    /// Unique ID of the application.
    /// </summary>
    [MapField("app_id")]
    public int Id { get; set; }

    /// <summary>
    /// Title of the application.
    /// </summary>
    [MapField("app_title")]
    public string Title { get; set; }
  }
}
