using System;


namespace BugDB.Reporter
{
  /// <summary>
  /// Grouping period.
  /// </summary>
  public enum GroupingPeriod
  {
    Day,
    Week,
    Month,
    Quater,
    Year
  }


  /// <summary>
  /// Represents group which is created for each time interval.
  /// </summary>
  /// <remarks>
  /// Grouping in balance report is accomplished by date.
  /// Time axis is divided into descrete intervals.
  /// Duration of interval is integer number and counted in days.
  /// Reference point is Jan-1, 1990, which is Monday that
  /// simplifies by week, by month, etc interval calculations.
  /// Only index of interval of is stored.
  /// </remarks>
  public class PeriodGroup
  {
    #region Constructors
    /// <summary>
    /// Constructs group item for specified interval.
    /// </summary>
    /// <param name="intervalIdx">Index of interval</param>
    /// <param name="intervalStart">Start date of interval</param>
    /// <param name="intervalEnd">End date of interval</param>
    public PeriodGroup(int intervalIdx, DateTime intervalStart, DateTime intervalEnd)
    {
      this.Interval = intervalIdx;
      this.IntervalStart = intervalStart;
      this.IntervalEnd = intervalEnd;
    }
    #endregion Constructor

    #region Public Fields
    /// <summary>
    /// Interval index.
    /// </summary>
    public int Interval { get; private set; }

    /// <summary>
    /// Start of the interval.
    /// </summary>
    public DateTime IntervalStart { get; set; }
    /// <summary>
    /// End of the interval
    /// </summary>
    public DateTime IntervalEnd { get; set; }

    /// <summary>
    /// Number of added bugs.
    /// </summary>
    public int Added { get; set; }

    /// <summary>
    /// Number of removed bugs.
    /// </summary>
    public int Removed { get; set; }

    /// <summary>
    /// Number of postponed bugs.
    /// </summary>
    public int Postponed { get; set; }

    /// <summary>
    /// Number of reactivated bugs.
    /// </summary>
    public int Reactivated { get; set; }
    #endregion Public Fields

    #region Public Methods
    public bool Equals(PeriodGroup other)
    {
      if( ReferenceEquals(null, other) )
      {
        return false;
      }
      if( ReferenceEquals(this, other) )
      {
        return true;
      }
      return other.Interval == Interval;
    }
    #endregion Public Methods

    #region Overrides
    public override bool Equals(object obj)
    {
      if( ReferenceEquals(null, obj) )
      {
        return false;
      }
      if( ReferenceEquals(this, obj) )
      {
        return true;
      }
      if( obj.GetType() != typeof(PeriodGroup) )
      {
        return false;
      }
      return Equals((PeriodGroup)obj);
    }

    public override int GetHashCode()
    {
      return Interval;
    }

    public static bool operator ==(PeriodGroup left, PeriodGroup right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(PeriodGroup left, PeriodGroup right)
    {
      return !Equals(left, right);
    }
    #endregion Overrides
  }
}