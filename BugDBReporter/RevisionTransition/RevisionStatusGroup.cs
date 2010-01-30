using BugDB.DataAccessLayer.DataTransferObjects;


namespace BugDB.Reporter.RevisionTransition
{
  /// <summary>
  /// Group for some particular revision status.
  /// </summary>
  /// <remarks>
  /// Contains name and list of concrete revision
  /// statuses which belongs to group.
  /// </remarks>
  class RevisionStatusGroup
  {
    /// <summary>
    /// Name of the group.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Statuses which form the group.
    /// </summary>
    public BugStatus[] Statuses { get; set; }

    /// <summary>
    /// Prints debugging string.
    /// </summary>
    public override string ToString()
    {
      return this.Name;
    }
  }
}
