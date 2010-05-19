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
  ///   suspend
  ///   to_be_assigned
  ///   assigned
  ///   works_for_me
  ///   active
  ///   fixed
  ///   reopen
  ///   modelling
  ///   regression
  ///   test_cases
  ///   verified
  ///   closed
  ///   deleted
  /// </remarks>
  public enum BugStatus
  {
    /// <summary>
    /// Status isn't specified.
    /// </summary>
    None,
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
    /// Bug can't be fixed without significant
    /// changes or depends on fix in 3rd party software.
    /// </summary>
    Suspend,
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
    /// Modeling staff should verify bug fix.
    /// </summary>
    Modeling,
    /// <summary>
    /// Modeling staff should create regression tests.
    /// </summary>
    Regression,
    /// <summary>
    /// Test cases should be prepared by QM.
    /// </summary>
    TestCases,
    /// <summary>
    /// Bug was verified as fixed by Tester.
    /// </summary>
    Verified,
    /// <summary>
    /// Bug was finally closed after release is finished.
    /// </summary>
    Closed,
    /// <summary>
    /// ??
    /// </summary>
    Deleted
  }
}