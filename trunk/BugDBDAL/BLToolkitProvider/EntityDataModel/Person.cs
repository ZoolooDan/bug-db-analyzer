using BLToolkit.Mapping;


namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// EDM object for working with Staff table.
  /// </summary>
  public class Person
  {
    /// <summary>
    /// Unique ID of person in storage.
    /// </summary>
    [MapField("person_id")]
    public int Id { get; set; }

    /// <summary>
    /// Unique login of person in BugDB.
    /// </summary>
    [MapField("person_login")]
    public string Login { get; set; }

    /// <summary>
    /// Title of person to represent externally.
    /// </summary>
    [MapField("person_title")]
    public string Title { get; set; }
  }
}