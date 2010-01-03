namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Represents all possible bug statuses.
  /// </summary>
  public enum BugStatus
  {
    /// <summary>
    /// Bug is just opened.
    /// </summary>
    Open,
    /// <summary>
    /// Bug should be assigned by Team Leader.
    /// </summary>
    ToBeAssigned,
    /// <summary>
    /// Bug was analyzed and postponed for now.
    /// </summary>
    Analyzed,
    /// <summary>
    /// Bug was considered as wrong.
    /// </summary>
    Invalid,
    /// <summary>
    /// Bug was assigned to Developer.
    /// </summary>
    Assigned,
    /// <summary>
    /// Bug was taken for work by Developer.
    /// </summary>
    Active,
    /// <summary>
    /// Developer considered bug as not reprodusing any more.
    /// </summary>
    WorksForMe,
    /// <summary>
    /// Bug was fixed by Developer.
    /// </summary>
    Fixed,
    /// <summary>
    /// Bug was verified as fixed by Tester.
    /// </summary>
    Verified,
    /// <summary>
    /// Bug was considered by Tester as still reprodusing after fix.
    /// </summary>
    Reopen,
    /// <summary>
    /// Bug was finally closed after release is finished.
    /// </summary>
    Closed
/*
    assigned
    to_be_assigned
  works_for_me
  verified
  closed
  analysed
  duplicate
  invalid
  active
  fixed
  reopen
 */
  }
}