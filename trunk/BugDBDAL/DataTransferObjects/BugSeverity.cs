namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Severity of the bug (how critical bug is for the release).
  /// </summary>
  /// <remarks>
  /// First 4 severities has standardized meaning and
  /// others are reserved for the future usage.
  /// </remarks>
  public enum BugSeverity
  {
    /// <summary>
    /// Fatal defect which prevents application
    /// even basic functioning and should be fixed
    /// as soon as possible.
    /// </summary>
    Fatal,
    /// <summary>
    /// Bug is considered as critical for release.
    /// </summary>
    Critical,
    /// <summary>
    /// Bug is not critical for release but 
    /// represents quite serious defect.
    /// </summary>
    Serious,
    /// <summary>
    /// Not really serious defect.
    /// </summary>
    Minor,
    /// <summary>
    /// Reserved.
    /// </summary>
    Sev5,
    /// <summary>
    /// Reserved.
    /// </summary>
    Sev6,
    /// <summary>
    /// Reserved.
    /// </summary>
    Sev7,
    /// <summary>
    /// Reserved.
    /// </summary>
    Sev8,
    /// <summary>
    /// Reserved.
    /// </summary>
    Sev9
  }
}