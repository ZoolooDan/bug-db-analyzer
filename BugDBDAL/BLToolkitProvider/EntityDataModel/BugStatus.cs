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
    /// Bug should be assigned by Team Leader.
    /// </summary>
    [MapValue(5)]
    ToBeAssigned,
    /// <summary>
    /// Bug was assigned to Developer.
    /// </summary>
    [MapValue(6)]
    Assigned,
    /// <summary>
    /// Developer considered bug as not reprodusing any more.
    /// </summary>
    [MapValue(7)]
    WorksForMe,
    /// <summary>
    /// Bug was taken for work by Developer.
    /// </summary>
    [MapValue(8)]
    Active,
    /// <summary>
    /// Bug was fixed by Developer.
    /// </summary>
    [MapValue(9)]
    Fixed,
    /// <summary>
    /// Bug was considered by Tester as still reprodusing after fix.
    /// </summary>
    [MapValue(10)]
    Reopen,
    /// <summary>
    /// Bug was verified as fixed by Tester.
    /// </summary>
    [MapValue(11)]
    Verified,
    /// <summary>
    /// Bug was finally closed after release is finished.
    /// </summary>
    [MapValue(12)]
    Closed
  }
}