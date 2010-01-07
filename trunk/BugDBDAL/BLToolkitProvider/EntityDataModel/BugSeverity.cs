using BLToolkit.Mapping;


namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// Severity values mapping.
  /// </summary>
  public enum BugSeverity
  {
    /// <summary>
    /// Fatal defect which prevents application
    /// even basic functioning and should be fixed
    /// as soon as possible.
    /// </summary>
    [MapValue(1)]
    Fatal,
    /// <summary>
    /// Bug is considered as critical for release.
    /// </summary>
    [MapValue(2)]
    Critical,
    /// <summary>
    /// Bug is not critical for release but 
    /// represents quite serious defect.
    /// </summary>
    [MapValue(3)]
    Serious,
    /// <summary>
    /// Not really serious defect.
    /// </summary>
    [MapValue(4)]
    Minor,
    /// <summary>
    /// Reserved.
    /// </summary>
    [MapValue(5)]
    Sev5,
    /// <summary>
    /// Reserved.
    /// </summary>
    [MapValue(6)]
    Sev6,
    /// <summary>
    /// Reserved.
    /// </summary>
    [MapValue(7)]
    Sev7,
    /// <summary>
    /// Reserved.
    /// </summary>
    [MapValue(8)]
    Sev8,
    /// <summary>
    /// Reserved.
    /// </summary>
    [MapValue(9)]
    Sev9
  }
}