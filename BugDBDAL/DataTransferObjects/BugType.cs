namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Represents two possible types of records in bug DB (bug and feature).
  /// </summary>
  public enum BugType
  {
    /// <summary>
    /// Defect in software.
    /// </summary>
    Bug,
    /// <summary>
    /// New feature request.
    /// </summary>
    Feature
  }
}