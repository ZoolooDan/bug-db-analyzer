using System;
using System.Data;

using BugDB.DataAccessLayer.DataTransferObjects;


namespace BugDB.Reporter
{
  /// <summary>
  /// Creates report with bugfixing statistics for each
  /// developer.
  /// </summary>
  /// <remarks>
  /// Developer bugfixing statistics includes 
  /// information on number of fixed and reopen 
  /// bugs through the time.
  /// </remarks>
  class DeveloperStatisticsReporter : IReporter
  {
    #region Private Fields
    private Revision[] m_revisions;
    private DateTime m_fromDate;
    private DateTime m_toDate;
    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Constructs reporter.
    /// </summary>
    public DeveloperStatisticsReporter(Revision[] revisions, DateTime fromDate, DateTime toDate)
    {
      m_revisions = revisions;
      m_fromDate = fromDate;
      m_toDate = toDate;
    }
    #endregion Constructors

    #region Implementation of IReporter
    /// <summary>
    /// Returns data set containing all data of the report.
    /// </summary>
    /// <remarks>
    /// Returned data set contains following tables:
    /// +) Developers info
    /// +) Assigned statistics
    /// +) Releived statistics
    /// +) Fixed statistics
    /// +) Reopen statistics
    /// 
    /// Developers info table contains names of developers
    /// alongside with Total and Average statistics. Each
    /// row represents one column in other tables.
    /// 
    /// If all other tables colum represents one
    /// developer and row represents on period.
    /// 
    /// Assigned statistics contains information on bugs
    /// assigned to developer in each time period.
    /// 
    /// Releived statistics contains information on bugs
    /// removed from assignement without being fixed.
    /// 
    /// Fixed statistics contains information on bugs
    /// fixed in each time period. 
    /// 
    /// Reopen statistics contains information on bugs
    /// reopen in each time period.
    /// 
    /// Balance statistics contains information on difference
    /// between
    /// </remarks>
    public DataSet CreateReport()
    {
      throw new NotImplementedException();
    }
    #endregion
  }
}
