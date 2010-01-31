using System.Data;


namespace BugDB.Reporter
{
  /// <summary>
  /// Reporter interface.
  /// </summary>
  /// <remarks>
  /// All reporters shall implement this interface.
  /// To be general for all possible cases it only returns
  /// <see cref="DataSet"/> on call to the <see cref="CreateReport()"/> method.
  /// </remarks>
  interface IReporter
  {
    /// <summary>
    /// Returns data set containing all data for the report.
    /// </summary>
    /// <remarks>
    /// Returned data set in general contains specific 
    /// data for each concrete type of the report.
    /// </remarks>
    DataSet CreateReport();
  }
}
