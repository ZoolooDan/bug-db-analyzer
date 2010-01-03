namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Release represents named version of application.
  /// </summary>
  /// <remarks>
  /// Release is associated with particular application.
  /// </remarks>
  public class Release
  {
    /// <summary>
    /// Unique release ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID of the parent application.
    /// </summary>
    public int AppId { get; set; }

    /// <summary>
    /// Title of the release.
    /// </summary>
    public string Title { get; set; }
  }
}
