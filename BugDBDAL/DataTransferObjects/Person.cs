namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Represents team member participating in BugDB process.
  /// </summary>
  /// <remarks>
  /// Person has display title and login which uniquely 
  /// identify him in BugDB.
  /// </remarks>
  public class Person
  {
    /// <summary>
    /// Unique ID of person in storage.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Unique login of person in BugDB.
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Title of person to represent externally.
    /// </summary>
    public string Title { get; set; }
  }
}
