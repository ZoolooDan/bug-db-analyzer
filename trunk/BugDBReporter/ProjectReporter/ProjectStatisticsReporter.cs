﻿using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.Reporter.RevisionTransition;


namespace BugDB.Reporter
{
  /// <summary>
  /// Creates bugfixing statistics for some project.
  /// </summary>
  class ProjectStatisticsReporter : IReporter
  {
    #region Private Fields
    private Revision[] m_revisions;
    private GroupPeriod m_period;
    private DateTime m_fromDate;
    private DateTime m_toDate;
    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Constructs reporter.
    /// </summary>
    public ProjectStatisticsReporter(Revision[] revisions, GroupPeriod period, DateTime fromDate, DateTime toDate)
    {
      m_revisions = revisions;
      m_period = period;
      m_fromDate = fromDate;
      m_toDate = toDate;
    }
    #endregion Constructors

    #region Implementation of IReporter
    /// <summary>
    /// Returns data set containing all data for the report.
    /// </summary>
    /// <remarks>
    /// Returned data set contains following tables:
    /// +) Bugfixing periods (Periods)
    /// +) Bugfixing statistics (Statistics)
    /// +) Report information (Info)
    /// 
    /// --------------------------------------------------
    /// Bugfixing periods contains information on Added,
    /// Postponed, Reactivated, Removed bugs for each 
    /// time period and additionally two balances - 
    /// with Postponed/Reactivated and with only Added/Removed
    /// accounted.
    /// 
    /// There are following columns in the table:
    /// +) FromDate - period start date
    /// +) ToDate - period end date
    /// +) Added - number of Add transition during period
    /// +) Postponed - -//- 
    /// +) Reactivated - -//- 
    /// +) Removed - -//- 
    /// +) Balance1 - balance with Postponed/Reactivated
    /// +) Balance2 - balance with only Added/Removed
    /// 
    /// Each row contains information for one time period.
    /// 
    /// --------------------------------------------------
    /// Bugfixing statistics table contains information on
    /// Total, Average per period and Average per day statistics.
    /// 
    /// There are following columns in the table:
    /// +) Aggregate - name of aggregate
    /// +) Added - value of aggregate for Added through all periods
    /// +) Postponed - -//- for Postponed
    /// +) Reactivated - -//- 
    /// +) Removed - -//- 
    /// +) Balance1 - -//- 
    /// +) Balance2 - -//- 
    /// 
    /// --------------------------------------------------
    /// Report information table contains parameters 
    /// for which report was created:
    /// +) FromDate - 
    /// +) ToDate
    /// +) GroupPeriod - ByDay, ByWeek, etc
    /// </remarks>
    public DataSet CreateReport()
    {
      // Create data set
      ProjectStatisticDataSet dataSet = new ProjectStatisticDataSet();

      // Use filter to iterate through revisions
      RevisionTransitionFilter filter = new RevisionTransitionFilter();

      // Select transition which fall into desired time interval
      // Group them by interval index
      // Order groups by interval index
      var query = from t in filter.GetTransitions(m_revisions)
                  where t.CurrentRevision.Date >= m_fromDate &&
                        t.CurrentRevision.Date <= m_toDate
                  group t by GetGroup(m_period, t.CurrentRevision) into g
                  orderby g.Key.Interval
                  select g;

      int prevInterval = -1;
      // Count transitions
      foreach(var group in query)
      {
        // Initialize values
        int added = 0;
        int postponed = 0;
        int reactivated = 0;
        int removed = 0;

        // Update values for all revisions in group
        foreach(RevisionStatusTransition transition in group)
        {
          // Update correspondent values
          added += transition.Name == "Added" ? 1 : 0;
          postponed += transition.Name == "Postponed" ? 1 : 0;
          reactivated += transition.Name == "Reactivated" ? 1 : 0;
          removed += transition.Name == "Removed" ? 1 : 0;
        }

        // Fill gaps between nonempty periods with zero rows
        if (prevInterval != -1 && group.Key.Interval > (prevInterval + 1))
        {
          for (int i = prevInterval + 1; i < group.Key.Interval; i++)
          {
            dataSet.Periods.AddPeriodsRow(0, 0, 0, 0);
          }
        }
        // Add row for current group
        dataSet.Periods.AddPeriodsRow(added, postponed, reactivated, removed);

        // Remember current interval
        prevInterval = group.Key.Interval;
      }

      return null;
    }
    #endregion

    #region Helper Methods
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
    private static Group GetGroup(GroupPeriod period, Revision revision)
    {
      DateTime date = revision.Date;
      DateTime refDate = new DateTime(1990, 1, 1);

      TimeSpan offset = revision.Date - refDate;

      int index = -1;
      // ByDay
      if( period == GroupPeriod.ByDay )
      {
        // For days it's just number of days
        // from reference point till revision date
        index = offset.Days;
      }
      else if( period == GroupPeriod.ByWeek ) // ByWeek
      {
        // For weeks it's quotient of division days by 7
        index = offset.Days/7;
      }
      else if( period == GroupPeriod.ByMonth ) // ByMonth
      {
        // 01.1990 - 0  = (1990 - 1990)*12 + (01 - 01) = 0*12 + 0
        // 12.1990 - 11 = (1990 - 1990)*12 + (12 - 01) = 0*12 + 11
        // 01.1991 - 12 = (1991 - 1990)*12 + (01 - 01) = 1*12 + 0
        // 06.1991 - 17 = (1991 - 1990)*12 + (06 - 01) = 1*12 + 5
        // 06.1992 - 29 = (1992 - 1990)*12 + (06 - 01) = 2*12 + 5

        // Number of full years plus
        index = (date.Year - refDate.Year)*12 + (date.Month - refDate.Month);
      }
      else if( period == GroupPeriod.ByQuater ) // ByQuater
      {
        // Same as months but divided by 3
        index = ((date.Year - refDate.Year)*12 + (date.Month - refDate.Month))/3;
      }
      else if( period == GroupPeriod.ByYear ) // ByYear
      {
        // Offset of year
        index = date.Year - refDate.Year;
      }

      // Check that it's been calculated
      Debug.Assert( index > 0 );

      DateTime intervalStart, intervalEnd;
      // Get dates
      GetIntervalDates(period, index, out intervalStart, out intervalEnd);
      // Create new group
      Group group = new Group(index, intervalStart, intervalEnd);

      return group;
    }

    /// <summary>
    /// Returns start and end dates of interval.
    /// </summary>
    private static void GetIntervalDates(GroupPeriod period, int interval, 
      out DateTime intervalStart, out DateTime intervalEnd)
    {
      DateTime refDate = new DateTime(1990, 1, 1);

      intervalStart = DateTime.MinValue;
      intervalEnd = DateTime.MinValue;

      // ByDay
      if( period == GroupPeriod.ByDay )
      {
        // Shift reference date by interval days
        intervalStart = refDate + new TimeSpan(interval, 0, 0, 0);
        // Interval end is the same as start
        intervalEnd = intervalStart;
      }
      else if( period == GroupPeriod.ByWeek ) // ByWeek
      {
        // Shift reference date by interval weeks
        intervalStart = refDate.AddDays(interval*7);
        intervalEnd = intervalStart.AddDays(6);
      }
      else if( period == GroupPeriod.ByMonth ) // ByMonth
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
      else if( period == GroupPeriod.ByQuater ) // ByQuater
      {
        // Multiply by 4 to get months
        int months1 = interval*3;
        int months2 = (interval + 1)*3;
        // Similar as for months
        intervalStart = new DateTime(refDate.Year + months1/12, refDate.Month + months1%12, 1);
        intervalEnd = new DateTime(refDate.Year + months2/12, refDate.Month + months2%12, 1) 
          - new TimeSpan(1, 0, 0, 0);
      }
      else if( period == GroupPeriod.ByYear ) // ByYear
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
}