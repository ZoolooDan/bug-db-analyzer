using BLToolkit.Mapping;


namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// Bug type values mapping.
  /// </summary>
  public enum BugType
  {
    /// <summary>
    /// Defect in software.
    /// </summary>
    [MapValue(1)]
    Bug,
    /// <summary>
    /// New feature request.
    /// </summary>
    [MapValue(2)]
    Feature
  }
}