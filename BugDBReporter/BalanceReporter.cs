using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.DataTransferObjects;


namespace BugDB.Reporter
{
  // C:\Program Files\Microsoft Visual Studio 9.0\vc\bin>
  // xsd d:\Work\MyProjects\BugDBAnalyzer4\BugDBReporter\config\ReporterConfig.xsd /classes /l:CS /n:BugDB.Reporter.Configuration /out:d:\Work\MyProjects\BugDBAnalyzer4\BugDBReporter\Configuration


  /// <summary>
  /// Creates different reports.
  /// </summary>
  /// <remarks>
  /// +) Bug balance report
  /// 
  /// Balance report shows balance between bugs added and 
  /// removed from consideration. 
  /// 
  /// Let's group states by their meaning in process:
  /// 
  /// DontExist   : 
  /// ForAnalysis : open, reopen                   
  /// Postponed   : analysed, suspend
  /// ForWork     : to_be_assigned, assigned, active
  /// ForTesting  : invalid, duplicate, works_for_me, fixed, 
  ///               modelling, regression, test_cases
  /// Finished    : verified, closed, deleted
  /// 
  /// States may transit one to another. Although not
  /// all possible transition allowed.
  /// 
  /// Following transition could be extracted:
  /// 
  /// +) Added       : (DontExist, ForTesting, Finished)->(ForAnalysis)
  /// +) Removed     : (ForAnalysis, ForWork, Postponed)->(ForTesting)
  /// +) Postponed   : (ForAnalysis, ForWork)->(Postponed)
  /// +) Reactivated : (Postponed)->(ForAnalysis, ForWork)
  /// 
  /// Two approaches are possible:
  /// +) Treat Postponed<->Reactivated as Removed<->Added
  /// +) Ignore Postponed and Reactivated transitions
  /// 
  /// First approach considers Postopned bugs as Removed
  /// from development process (though temporary). Naturally 
  /// when it's changed from Postponed to ForAnaysis or ForWork
  /// it's treated as Added. 
  /// 
  /// As opposed to that second approach treats 
  /// Postponed as something still for work. 
  /// Thus this Postponed<->Reactivated transition
  /// could be considered as some transition inside
  /// ForWork group.
  ///
  ///  
  /// Different strategies could be used for treating both 
  /// added and removed states:
  ///
  /// 1) Added   : (NotExists, ForTesting, Finished)->(ForAnalysis, ForWork, Postponed)
  ///    Removed : (ForAnalysis, ForWork, Postponed)->(ForTesting, Finished) 
  /// 
  /// 2) Added   : (NotExists, Postponed, ForTesting, Finished)->(ForAnalysis, ForWork)
  ///    Removed : (ForAnalysis, ForWork)->(Postponed, ForTesting, Finished)
  /// 
  /// 
  /// open
  /// invalid
  /// duplicate
  /// analysed
  /// suspend
  /// to_be_assigned
  /// assigned
  /// works_for_me
  /// active
  /// fixed
  /// reopen
  /// modelling
  /// regression
  /// test_cases
  /// verified
  /// closed
  /// deleted
  /// 
  /// </remarks>
  public class BalanceReporter
  {
    #region Private Fields
    private IDataProvider m_provider;
    #endregion Private Fields

    #region Constructors
    public BalanceReporter(IDataProvider provider)
    {
      m_provider = provider;

      LoadConfig();
    }
    #endregion Constructors

    #region Public Methods
    /// <summary>
    /// Creates balance report.
    /// </summary>
    public List<PeriodGroup> CreateReport(GroupingPeriod groupBy, bool ignorePostponed)
    {
      // Input data:
      // +) revisions (filter for bugs)
      // +) interval duration (by day, by week, by month, by quater, by year)
      // +) additional options (e.g. how to treat Postponed)


      // Resulting groups
      List<PeriodGroup> groups = new List<PeriodGroup>();

      // Get bugs for specified filter
      Bug[] bugs = m_provider.GetAllBugs();

      // Process all bugs
      foreach( Bug bug in bugs )
      {
        // Get all revisions for selected bugs (they should be sorted)
        Revision[] revisions = m_provider.GetBugRevisions(bug.Number);

        Revision prevRevision = null;
        // Go through revisions of one bug
        foreach( Revision revision in revisions )
        {
          // Compare current and previous revision
          StatusTransition trans = GetStatusTransition(prevRevision, revision);

          // When combination of states corresponds to state transition 
          if( trans != StatusTransition.None )
          {
            // Get record for period correspondent to the date of current revision
            PeriodGroup group = GetGroup(groups, groupBy, revision);

            // Update correspondent values
            group.Added += trans == StatusTransition.Added ? 1 : 0;
            group.Removed += trans == StatusTransition.Removed ? 1 : 0;
            group.Postponed += trans == StatusTransition.Postponed ? 1 : 0;
            group.Reactivated += trans == StatusTransition.Reactivated ? 1 : 0;
          }

          // Remember current revision
          prevRevision = revision;
        }
      }

      // Sort groups by index because they go in arbitrary order
      groups.Sort((x, y) => x.Interval - y.Interval);

      // Make groups go without gaps
      UnsparseGroups(groups, groupBy);
     
      // Return report
      return groups;
    }

    #endregion Public Methods

    #region Helper Methods
    /// <summary>
    /// Load configuration from file.
    /// </summary>
    private void LoadConfig()
    {
      XmlSerializer serializer = new XmlSerializer(typeof(Configuration.ReportConfig));

      string configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
        @"config\ReporterConfig.xml");
      using( Stream stream = new FileStream(configPath, FileMode.Open, FileAccess.Read) )
      {
        Configuration.ReportConfig reporter = (Configuration.ReportConfig)serializer.Deserialize(stream);
      }
    }

    /// <summary>
    /// Returns group for specified revision.
    /// </summary>
    /// <remarks>
    /// Each group has index of perid it covers.
    /// Duration of period is specified as enumerated value.
    /// This is because if specified in days it may vary
    /// because number of days isn't ecual for some periods, like months
    /// years and quaters.
    /// 
    /// Reference point is Jan-1, 1990.
    /// </remarks>
    private static PeriodGroup GetGroup(List<PeriodGroup> groups, GroupingPeriod groupBy, Revision revision)
    {
      DateTime date = revision.Date;
      DateTime refDate = new DateTime(1990, 1, 1);

      TimeSpan offset = revision.Date - refDate;

      int index = -1;
      // Day
      if( groupBy == GroupingPeriod.Day )
      {
        // For days it's just number of days
        // from reference point till revision date
        index = offset.Days;
      }
      else if( groupBy == GroupingPeriod.Week ) // Week
      {
        // For weeks it's quotient of division days by 7
        index = offset.Days/7;
      }
      else if( groupBy == GroupingPeriod.Month ) // Month
      {
        // 01.1990 - 0  = (1990 - 1990)*12 + (01 - 01) = 0*12 + 0
        // 12.1990 - 11 = (1990 - 1990)*12 + (12 - 01) = 0*12 + 11
        // 01.1991 - 12 = (1991 - 1990)*12 + (01 - 01) = 1*12 + 0
        // 06.1991 - 17 = (1991 - 1990)*12 + (06 - 01) = 1*12 + 5
        // 06.1992 - 29 = (1992 - 1990)*12 + (06 - 01) = 2*12 + 5

        // Number of full years plus
        index = (date.Year - refDate.Year)*12 + (date.Month - refDate.Month);
      }
      else if( groupBy == GroupingPeriod.Quater ) // Quater
      {
        // Same as months but divided by 3
        index = ((date.Year - refDate.Year)*12 + (date.Month - refDate.Month))/3;
      }
      else if( groupBy == GroupingPeriod.Year ) // Year
      {
        // Offset of year
        index = date.Year - refDate.Year;
      }

      // Check that it's been caluclated
      Debug.Assert( index > 0 );

      PeriodGroup group = groups.Find(item => item.Interval == index);
      if( group == null )
      {
        DateTime intervalStart, intervalEnd;
        // Get dates
        GetIntervalDates(groupBy, index, out intervalStart, out intervalEnd);
        // Create new group
        group = new PeriodGroup(index, intervalStart, intervalEnd);
        groups.Add(group);
      }

      return group;
    }

    /// <summary>
    /// Returns transition type if combination of 
    /// status of current and previous revision represents one.
    /// </summary>
    /// <remarks>
    /// State groups:
    /// ----------------------------------------------------
    /// DontExist   : 
    /// ForAnalysis : open, reopen                   
    /// Postponed   : analysed, suspend
    /// ForWork     : to_be_assigned, assigned, active
    /// ForTesting  : invalid, duplicate, works_for_me, fixed, 
    ///               modelling, regression, test_cases
    /// Finished    : verified, closed, deleted
    /// 
    /// Allowed transitions:
    /// ------------------------------------------------------------------
    /// +) Added       : (DontExist, ForTesting, Finished)->(ForAnalysis)
    /// +) Removed     : (ForAnalysis, ForWork, Postponed)->(ForTesting)
    /// +) Postponed   : (ForAnalysis, ForWork)->(Postponed)
    /// +) Reactivated : (Postponed)->(ForAnalysis, ForWork)
    /// 
    /// It also catches disallowed transitions:
    /// +) (ForAnalysis, Postponed, ForWork)->(Finished)
    /// +) (Finished)->(ForAnalysis, Postponed, ForWork)
    /// </remarks>
    private static StatusTransition GetStatusTransition(Revision prevRevision, 
      Revision curRevision)
    {
      Debug.Assert(curRevision != null);

      StatusGroup statusGroupPrev = GetStateGroup(prevRevision);
      StatusGroup statusGroupCur = GetStateGroup(curRevision);

      IDictionary<StatusGroup[], StatusTransition> allowed =
        new Dictionary<StatusGroup[], StatusTransition>(new StateGroupMappingComparer());
      // +) Added       : (DontExist, ForTesting, Finished)->(ForAnalysis)
      allowed.Add(new[] {StatusGroup.DontExist, StatusGroup.ForAnalysis}, StatusTransition.Added);
      allowed.Add(new[] {StatusGroup.ForTesting, StatusGroup.ForAnalysis}, StatusTransition.Added);
      allowed.Add(new[] {StatusGroup.Finished, StatusGroup.ForAnalysis}, StatusTransition.Added);
      // +) Removed     : (ForAnalysis, ForWork, Postponed)->(ForTesting)
      allowed.Add(new[] {StatusGroup.ForAnalysis, StatusGroup.ForTesting}, StatusTransition.Removed);
      allowed.Add(new[] {StatusGroup.ForWork, StatusGroup.ForTesting}, StatusTransition.Removed);
      allowed.Add(new[] {StatusGroup.Postponed, StatusGroup.ForTesting}, StatusTransition.Removed);
      // +) Postponed   : (ForAnalysis, ForWork)->(Postponed)
      allowed.Add(new[] {StatusGroup.ForAnalysis, StatusGroup.Postponed}, StatusTransition.Postponed);
      allowed.Add(new[] {StatusGroup.ForWork, StatusGroup.Postponed}, StatusTransition.Postponed);
      // +) Reactivated : (Postponed)->(ForAnalysis, ForWork)
      allowed.Add(new[] {StatusGroup.Postponed, StatusGroup.ForAnalysis}, StatusTransition.Reactivated);
      allowed.Add(new[] {StatusGroup.Postponed, StatusGroup.ForWork}, StatusTransition.Reactivated);
    
      IDictionary<StatusGroup[], StatusTransition> forbidden =
        new Dictionary<StatusGroup[], StatusTransition>(new StateGroupMappingComparer());
      // +) (ForAnalysis, Postponed, ForWork)->(Finished)
      forbidden.Add(new[] {StatusGroup.ForAnalysis, StatusGroup.Finished}, StatusTransition.None);
      forbidden.Add(new[] {StatusGroup.Postponed, StatusGroup.Finished}, StatusTransition.None);
      forbidden.Add(new[] {StatusGroup.ForWork, StatusGroup.Finished}, StatusTransition.None);
      // +) (Finished)->(ForAnalysis, Postponed, ForWork)
      forbidden.Add(new[] {StatusGroup.Finished, StatusGroup.ForAnalysis}, StatusTransition.None);
      forbidden.Add(new[] {StatusGroup.Finished, StatusGroup.Postponed}, StatusTransition.None);
      forbidden.Add(new[] {StatusGroup.Finished, StatusGroup.ForWork}, StatusTransition.None);

      StatusTransition transition;// = StatusTransition.None;
      // First try to find in allowed transitions
      var key = new[] {statusGroupPrev, statusGroupCur};
      if( !allowed.TryGetValue(key, out transition) )
      {
        // Next in forbidded - report
        if( forbidden.TryGetValue(key, out transition) )
        {
          Debug.Fail("Forbidden transition detected.");
        }
        else
        {
          // If neither - then None
          transition = StatusTransition.None;
        }
      }
      return transition;
    }

    /// <summary>
    /// Returns status group for the revision.
    /// </summary>
    /// <remarks>
    /// DontExist   : 
    /// ForAnalysis : open, reopen                   
    /// Postponed   : analysed, suspend
    /// ForWork     : to_be_assigned, assigned, active
    /// ForTesting  : invalid, duplicate, works_for_me, fixed, 
    ///               modelling, regression, test_cases
    /// Finished    : verified, closed, deleted
    /// </remarks>
    private static StatusGroup GetStateGroup(Revision revision)
    {
      StatusGroup statusGroup = StatusGroup.DontExist;

      IDictionary<BugStatus, StatusGroup> mapping = new Dictionary<BugStatus, StatusGroup>();
      /// ForAnalysis : open, reopen                   
      mapping.Add(BugStatus.Open, StatusGroup.ForAnalysis);
      mapping.Add(BugStatus.Reopen, StatusGroup.ForAnalysis);
      /// Postponed   : analysed, suspend
      mapping.Add(BugStatus.Analyzed, StatusGroup.Postponed);
      mapping.Add(BugStatus.Suspend, StatusGroup.Postponed);
      /// ForWork     : to_be_assigned, assigned, active
      mapping.Add(BugStatus.ToBeAssigned, StatusGroup.ForWork);
      mapping.Add(BugStatus.Assigned, StatusGroup.ForWork);
      mapping.Add(BugStatus.Active, StatusGroup.ForWork);
      /// ForTesting  : invalid, duplicate, works_for_me, fixed, 
      ///               modelling, regression, test_cases
      mapping.Add(BugStatus.Invalid, StatusGroup.ForTesting);
      mapping.Add(BugStatus.Duplicate, StatusGroup.ForTesting);
      mapping.Add(BugStatus.WorksForMe, StatusGroup.ForTesting);
      mapping.Add(BugStatus.Fixed, StatusGroup.ForTesting);
      mapping.Add(BugStatus.Modeling, StatusGroup.ForTesting);
      mapping.Add(BugStatus.Regression, StatusGroup.ForTesting);
      mapping.Add(BugStatus.TestCases, StatusGroup.ForTesting);
      /// Finished    : verified, closed, deleted
      mapping.Add(BugStatus.Verified, StatusGroup.Finished);
      mapping.Add(BugStatus.Closed, StatusGroup.Finished);
      mapping.Add(BugStatus.Deleted, StatusGroup.Finished);

      // If revision didn't exist - then group is DontExist
      if( revision != null )
      {
        // Get status group (treat empty status as if but is just open)
        Debug.Assert(mapping.TryGetValue(revision.Status ?? BugStatus.Open, 
          out statusGroup));
      }
      return statusGroup;
    }

    /// <summary>
    /// Make groups go without gaps.
    /// </summary>
    private static void UnsparseGroups(IList<PeriodGroup> groups, GroupingPeriod groupBy)
    {
      // Just exit if empty collection
      if( groups.Count == 0 )
      {
        return;
      }

      int prevInterval = groups.Count > 0 ? groups[0].Interval : -1;
      for( int i = 1; i < groups.Count; i++ )
      {
        int interval = prevInterval + 1;
        // If gap is found
        if( groups[i].Interval > interval )
        {
          // Insert new group
          DateTime intervalStart, intervalEnd; 
          GetIntervalDates(groupBy, interval, out intervalStart, out intervalEnd);
          PeriodGroup group = new PeriodGroup(interval, intervalStart, intervalEnd);
          groups.Insert(i, group);
        }
        prevInterval = interval;
      }
    }

    /// <summary>
    /// Returns start and end dates of interval.
    /// </summary>
    private static void GetIntervalDates(GroupingPeriod groupBy, int interval, 
      out DateTime intervalStart, out DateTime intervalEnd)
    {
      DateTime refDate = new DateTime(1990, 1, 1);

      intervalStart = DateTime.MinValue;
      intervalEnd = DateTime.MinValue;

      // Day
      if( groupBy == GroupingPeriod.Day )
      {
        // Shift reference date by interval days
        intervalStart = refDate + new TimeSpan(interval, 0, 0, 0);
        // Interval end is the same as start
        intervalEnd = intervalStart;
      }
      else if( groupBy == GroupingPeriod.Week ) // Week
      {
        // Shift reference date by interval weeks
        intervalStart = refDate.AddDays(interval*7);
        intervalEnd = intervalStart.AddDays(6);
      }
      else if( groupBy == GroupingPeriod.Month ) // Month
      {
        // 01.1990 - 0  = (1990 - 1990)*12 + (01 - 01) = 0*12 + 0
        // 12.1990 - 11 = (1990 - 1990)*12 + (12 - 01) = 0*12 + 11
        // 01.1991 - 12 = (1991 - 1990)*12 + (01 - 01) = 1*12 + 0
        // 06.1991 - 17 = (1991 - 1990)*12 + (06 - 01) = 1*12 + 5
        // 06.1992 - 29 = (1992 - 1990)*12 + (06 - 01) = 2*12 + 5

        // Number of full years to years, modulo to months
        intervalStart = new DateTime(refDate.Year + interval/12, refDate.Month + interval%12, 1);
        intervalEnd = intervalStart.AddMonths(1) - new TimeSpan(1, 0, 0, 0);
      }
      else if( groupBy == GroupingPeriod.Quater ) // Quater
      {
        // Multiply by 4 to get months
        int months1 = interval*3;
        int months2 = (interval + 1)*3;
        // Similar as for months
        intervalStart = new DateTime(refDate.Year + months1/12, refDate.Month + months1%12, 1);
        intervalEnd = new DateTime(refDate.Year + months2/12, refDate.Month + months2%12, 1) 
          - new TimeSpan(1, 0, 0, 0);
      }
      else if( groupBy == GroupingPeriod.Year ) // Year
      {
        // Shift years
        intervalStart = new DateTime(refDate.Year + interval, 1, 1);
        intervalEnd = new DateTime(refDate.Year + interval, 12, 31);
      }
      else
      {
        Debug.Fail("Unknow interval.");
      }
    }
    #endregion Helper Methods
  }


  internal class StateGroupMappingComparer : IEqualityComparer<StatusGroup[]>
  {
    #region Implementation of IEqualityComparer<StatusGroup[]>
    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    public bool Equals(StatusGroup[] x, StatusGroup[] y)
    {
      Debug.Assert(x != null && y != null);
      Debug.Assert(x.Length == 2);
      Debug.Assert(x.Length == y.Length);

      return x[0] == y[0] && x[1] == y[1];
    }

    /// <summary>
    /// Returns a hash code for the specified object.
    /// </summary>
    public int GetHashCode(StatusGroup[] obj)
    {
      Debug.Assert(obj != null);
      Debug.Assert(obj.Length == 2);

      return obj[0].GetHashCode() ^ obj[1].GetHashCode();
    }
    #endregion
  }


  internal enum StatusGroup
  {
    DontExist,
    ForAnalysis,
    Postponed,
    ForWork,
    ForTesting,
    Finished
  }



  /// <summary>
  /// Possible transitions.
  /// </summary>
  internal enum StatusTransition
  {
    /// <summary>
    /// No transition.
    /// </summary>
    None,
    /// <summary>
    /// Transition to add state.
    /// </summary>
    Added,
    /// <summary>
    /// Transition to removed state.
    /// </summary>
    Removed,
    /// <summary>
    /// Transition to postponed state.
    /// </summary>
    Postponed,
    /// <summary>
    /// Transition from postponed state.
    /// </summary>
    Reactivated
  }


  /*
 * /// http://blog.jagregory.com/page/5/
  public class CustomerRepository
  {
    /// <summary>
    /// Creates a NHibernate ICriteria based on the filters.
    /// </summary>
    /// <param name="filters">Filters to apply.</param>
    /// <returns>ICriteria</returns>
    private ICriteria CreateFilteredCriteria(FilterCriterionCollection filters)
    {
      ICriteria criteria = SessionManager.CurrentSession
        .CreateCriteria(typeof(Customer));

      // criterion handling - write this yourself depending on how your
      // db filters (and what filter types you're supporting)
      foreach(FilterCriterion filter in filters)
      {
        if( filter.Type == typeof(string) )
        {
          criteria.Add(Expression.Like(filter.FieldName, "%" + filter.Value + "%"));
        }
        else if( filter.Type == typeof(bool) )
        {
          criteria.Add(Expression.Eq(filter.FieldName, filter.Value));
        }
      }

      return criteria;
    }

    /// <summary>
    /// Gets the total record count from the database using the filters.
    /// </summary>
    /// <param name="filters">Filters to apply before getting the count.</param>
    /// <returns>Total number of records in the filtered list</returns>
    public int GetAllCount(FilterCriterionCollection filters)
    {
      return CreateFilteredCriteria(filters)
        .SetProjection(Projections.Count("ID"))
        .UniqueResult<int>();
    }

    /// <summary>
    /// Gets one page of data from the database.
    /// </summary>
    /// <param name="range">Select range (start ID and number of records).</param>
    /// <param name="sort">Sorting to apply.</param>
    /// <param name="filters">Filters to apply.</param>
    /// <returns>List for one page of data.</returns>
    public IList<Customer> FindAllPaged(SelectRange range, SortInfo sort, FilterCriterionCollection filters)
    {
      // create the criteria using the filters, then set the range
      ICriteria criteria = CreateFilteredCriteria(filters)
        .SetFirstResult(range.Start)
        .SetMaxResults(range.Size);

      // only add the sort if one is specified
      if( !string.IsNullOrEmpty(sort.Field) )
      {
        if( sort.Direction == Direction.Asc )
        {
          criteria.AddOrder(Order.Asc(sort.Field));
        }
        else
        {
          criteria.AddOrder(Order.Desc(sort.Field));
        }
      }

      return criteria.List<Customer>();
    }
  }
 */
}