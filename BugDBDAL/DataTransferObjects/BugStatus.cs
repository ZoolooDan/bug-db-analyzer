namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Represents all possible bug statuses.
  /// </summary>
  /// <remarks>
  /// Original statuses:
  ///   open
  ///   invalid
  ///   duplicate
  ///   analysed
  ///   to_be_assigned
  ///   assigned
  ///   works_for_me
  ///   active
  ///   fixed
  ///   reopen
  ///   verified
  ///   closed
  /// </remarks>
  public enum BugStatus
  {
    /// <summary>
    /// Bug is just opened.
    /// </summary>
    Open,
    /// <summary>
    /// Bug was considered as wrong.
    /// </summary>
    Invalid,
    /// <summary>
    /// Same bug already exists.
    /// </summary>
    Duplicate,
    /// <summary>
    /// Bug was analyzed and postponed for now.
    /// </summary>
    Analyzed,
    /// <summary>
    /// Bug should be assigned by Team Leader.
    /// </summary>
    ToBeAssigned,
    /// <summary>
    /// Bug was assigned to Developer.
    /// </summary>
    Assigned,
    /// <summary>
    /// Developer considered bug as not reprodusing any more.
    /// </summary>
    WorksForMe,
    /// <summary>
    /// Bug was taken for work by Developer.
    /// </summary>
    Active,
    /// <summary>
    /// Bug was fixed by Developer.
    /// </summary>
    Fixed,
    /// <summary>
    /// Bug was considered by Tester as still reprodusing after fix.
    /// </summary>
    Reopen,
    /// <summary>
    /// Bug was verified as fixed by Tester.
    /// </summary>
    Verified,
    /// <summary>
    /// Bug was finally closed after release is finished.
    /// </summary>
    Closed
  }
}