using BLToolkit.Mapping;


namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// Bug type values mapping.
  /// </summary>
  public enum BugType
  {
    /// <summary>
    /// Type is unspecified.
    /// </summary>
    [MapValue(1)]
    Unspecified,
    /// <summary>
    /// Defect in software.
    /// </summary>
    [MapValue(2)]
    Bug,
    /// <summary>
    /// New feature request.
    /// </summary>
    [MapValue(3)]
    Feature
  }
}