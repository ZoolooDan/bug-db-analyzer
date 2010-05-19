using BLToolkit.Mapping;


namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// Bug status values mapping.
  /// </summary>
  public enum BugStatus
  {
    /// <summary>
    /// Bug is just opened.
    /// </summary>
    [MapValue(0)]
    None,

    /// <summary>
    /// Bug is just opened.
    /// </summary>
    [MapValue(1)]
    Open,
    /// <summary>
    /// Bug was considered as wrong.
    /// </summary>
    [MapValue(2)]
    Invalid,
    /// <summary>
    /// Same bug already exists.
    /// </summary>
    [MapValue(3)]
    Duplicate,
    /// <summary>
    /// Bug was analyzed and postponed for now.
    /// </summary>
    [MapValue(4)]
    Analyzed,
    /// <summary>
    /// Bug can't be fixed without significant
    /// changes or depends on fix in 3rd party software.
    /// </summary>
    [MapValue(5)]
    Suspend,
    /// <summary>
    /// Bug should be assigned by Team Leader.
    /// </summary>
    [MapValue(6)]
    ToBeAssigned,
    /// <summary>
    /// Bug was assigned to Developer.
    /// </summary>
    [MapValue(7)]
    Assigned,
    /// <summary>
    /// Developer considered bug as not reprodusing any more.
    /// </summary>
    [MapValue(8)]
    WorksForMe,
    /// <summary>
    /// Bug was taken for work by Developer.
    /// </summary>
    [MapValue(9)]
    Active,
    /// <summary>
    /// Bug was fixed by Developer.
    /// </summary>
    [MapValue(10)]
    Fixed,
    /// <summary>
    /// Bug was considered by Tester as still reprodusing after fix.
    /// </summary>
    [MapValue(11)]
    Reopen,
    /// <summary>
    /// Modeling staff should verify bug fix.
    /// </summary>
    [MapValue(12)]
    Modeling,
    /// <summary>
    /// Modeling staff should create regression tests.
    /// </summary>
    [MapValue(13)]
    Regression,
    /// <summary>
    /// Test cases should be prepared by QM.
    /// </summary>
    [MapValue(14)]
    TestCases,
    /// <summary>
    /// Bug was verified as fixed by Tester.
    /// </summary>
    [MapValue(15)]
    Verified,
    /// <summary>
    /// Bug was finally closed after release is finished.
    /// </summary>
    [MapValue(16)]
    Closed,
    /// <summary>
    /// ??
    /// </summary>
    [MapValue(17)]
    Deleted
  }
}