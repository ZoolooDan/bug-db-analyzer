using BLToolkit.Mapping;

namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// EDM object for working with Releases table.
  /// </summary>
  public class Release
  {
    /// <summary>
    /// Unique release ID.
    /// </summary>
    [MapField("release_id")]
    public int Id { get; set; }

    /// <summary>
    /// ID of the parent application.
    /// </summary>
    [MapField("app_id")]
    public int AppId { get; set; }

    /// <summary>
    /// Title of the release.
    /// </summary>
    [MapField("release_title")]
    public string Title { get; set; }
  }
}
