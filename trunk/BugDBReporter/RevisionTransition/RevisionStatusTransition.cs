using BugDB.DataAccessLayer.DataTransferObjects;


namespace BugDB.Reporter.RevisionTransition
{
  /// <summary>
  /// Represents some determinable revision group transition.
  /// </summary>
  /// <remarks>
  /// Contains references to previous revision,
  /// revision which initiated transition and
  /// transition name. Actually transitions are 
  /// defined for status groups. So groups for 
  /// previous and current revisions are also
  /// returned.
  /// </remarks>
  class RevisionStatusTransition
  {
    /// <summary>
    /// Name of the transition.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Revision before transition.
    /// </summary>
    public Revision PreviousRevision { get; set; }

    /// <summary>
    /// Revision which initiatied transition.
    /// </summary>
    public Revision CurrentRevision { get; set; }

    /// <summary>
    /// Group to which belongs status of previous revision.
    /// </summary>
    public RevisionStatusGroup PreviousGroup { get; set; }

    /// <summary>
    /// Group to which belongs status of current revision.
    /// </summary>
    public RevisionStatusGroup CurrentGroup { get; set; }
  }
}
