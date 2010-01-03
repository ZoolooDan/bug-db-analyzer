namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Represents specific application for 
  /// which bug records exist.
  /// </summary>
  /// <remarks>
  /// Application have title and subordinate
  /// collections of releases and modules
  /// (which actually aren't represented in this DTO).
  /// </remarks>
  public class Application
  {
    /// <summary>
    /// Unique ID of the application.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Title of the application.
    /// </summary>
    public string Title { get; set; }
  }
}
