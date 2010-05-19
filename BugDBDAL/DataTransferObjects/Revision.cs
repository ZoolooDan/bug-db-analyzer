using System;

namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Extensions for DateTime class.
  /// </summary>
  public static class DateTimeCompareExtensions
  {
    /// <summary>
    /// Compares objects with some tolerance.
    /// </summary>
    /// <remarks>
    /// SQL Server "datetime" values are rounded to increments of .000, .003, or .007 seconds
    /// http://msdn.microsoft.com/en-us/library/ms187819.aspx
    /// 
    /// This extension method can be used to compare DateTime objects with some tolerance.
    /// http://stackoverflow.com/questions/381401/how-do-you-compare-datetime-objects-using-a-specified-tolerance-in-c
    /// </remarks>
    public static bool EqualsTol(this DateTime d1, DateTime d2, TimeSpan tolerance)
    {
      return (d1 - d2) < tolerance;
    }
  }

  /// <summary>
  /// Revision represents state of the bug
  /// at some particular time.
  /// </summary>
  /// <remarks>
  /// Through it's lifetime each bug may have several revisons.
  /// Revision is created when some properties of bug are changed.
  /// State of the revision itself can't be changed (e.g. it's immutable).
  /// Revision ID increased from 0 and greates revision ID
  /// corresponds to the most recent revision. This revision
  /// represents current state of the bug.
  /// </remarks>
  public class Revision : IEquatable<Revision>
  {
    /// <summary>
    /// Unique number of the bug record.
    /// </summary>
    public int BugNumber { get; set; }

    /// <summary>
    /// Bug revision.
    /// </summary>
    public int Rev { get; set; }

    /// <summary>
    /// Type of the bug record.
    /// </summary>
    public BugType Type { get; set;  }
    
    /// <summary>
    /// Status of the revision.
    /// </summary>
    public BugStatus Status { get; set; }

    /// <summary>
    /// Date of the revision.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// ID of application.
    /// </summary>
    public int AppId { get; set; }

    /// <summary>
    /// ID of module.
    /// </summary>
    public int? ModuleId { get; set; }

    /// <summary>
    /// ID of submodule.
    /// </summary>
    public int? SubModuleId { get; set; }

    /// <summary>
    /// ID of the application release during which this bug was found.
    /// </summary>
    public int? FoundReleaseId { get; set; }

    /// <summary>
    /// ID of the application release during which bug supposed to be fixed.
    /// </summary>
    public int? TargetReleaseId { get; set; }

    /// <summary>
    /// Importance of the bug for daily work and release.
    /// </summary>
    public BugSeverity? Severity { get; set; }

    /// <summary>
    /// Bug processing priority.
    /// </summary>
    /// <remarks>
    /// Lower values mean higher priority.
    /// </remarks>
    public int? Priority { get; set; }

    /// <summary>
    /// ID of person who created the bug.
    /// </summary>
    public int ContributorId { get; set; }
    
    /// <summary>
    /// ID of person who is responsible for the bug assignment.
    /// </summary>
    public int? TeamLeaderId { get; set; }
    
    /// <summary>
    /// ID of person who is responsible for the bug fixing.
    /// </summary>
    public int? DeveloperId { get; set; }
    
    /// <summary>
    /// ID of person who is responsible for the verifying
    /// bug fix or other changes of the state.
    /// </summary>
    /// <remarks>
    /// Testers are responsible for verifying following states:
    /// +) Fixed
    /// +) WorksForMe
    /// +) Invalid
    /// </remarks>
    public int? TesterId { get; set; }
    
    /// <summary>
    /// Short description of the bug.
    /// </summary>
    public string Summary { get; set; }

    #region Equality Overrides
    public bool Equals(Revision other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      // Notice comparison of DateTime objects
      return other.BugNumber == BugNumber && other.Rev == Rev && Equals(other.Type, Type) && other.Status.Equals(Status) && other.Date.EqualsTol(Date, TimeSpan.FromMilliseconds(2)) && other.AppId == AppId && other.ModuleId.Equals(ModuleId) && other.SubModuleId.Equals(SubModuleId) && other.FoundReleaseId.Equals(FoundReleaseId) && other.TargetReleaseId.Equals(TargetReleaseId) && other.Severity.Equals(Severity) && other.Priority.Equals(Priority) && other.ContributorId == ContributorId && other.TeamLeaderId.Equals(TeamLeaderId) && other.DeveloperId.Equals(DeveloperId) && other.TesterId.Equals(TesterId) && Equals(other.Summary, Summary);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != typeof (Revision)) return false;
      return Equals((Revision) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        int result = BugNumber;
        result = (result*397) ^ Rev;
        result = (result*397) ^ Type.GetHashCode();
        result = (result*397) ^ Status.GetHashCode();
        result = (result*397) ^ Date.GetHashCode();
        result = (result*397) ^ AppId;
        result = (result*397) ^ (ModuleId.HasValue ? ModuleId.Value : 0);
        result = (result*397) ^ (SubModuleId.HasValue ? SubModuleId.Value : 0);
        result = (result*397) ^ (FoundReleaseId.HasValue ? FoundReleaseId.Value : 0);
        result = (result*397) ^ (TargetReleaseId.HasValue ? TargetReleaseId.Value : 0);
        result = (result*397) ^ (Severity.HasValue ? Severity.Value.GetHashCode() : 0);
        result = (result*397) ^ (Priority.HasValue ? Priority.Value : 0);
        result = (result*397) ^ ContributorId;
        result = (result*397) ^ (TeamLeaderId.HasValue ? TeamLeaderId.Value : 0);
        result = (result*397) ^ (DeveloperId.HasValue ? DeveloperId.Value : 0);
        result = (result*397) ^ (TesterId.HasValue ? TesterId.Value : 0);
        result = (result*397) ^ (Summary != null ? Summary.GetHashCode() : 0);
        return result;
      }
    }

    public static bool operator ==(Revision left, Revision right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Revision left, Revision right)
    {
      return !Equals(left, right);
    }
    #endregion Equality Overrides

    /// <summary>
    /// Prints debugging information.
    /// </summary>
    public override string ToString()
    {
      string str = String.Format("{{{0},{1}}}-{2},{3},{4}", 
        this.BugNumber, this.Rev,
        this.Type, 
        this.Status,
        this.Date.ToShortDateString());
      return str;
    }
  }
}

/*
 number      subnumber deadline     status                         date         closedate    application                                        modul                                              submodul                                           apprelease                     frelease                       severity                       priority                       contributor                    leader                         developer                      qa                             summary                                                                                                                                                          
 ----------- --------- ------------ ------------------------------ ------------ ------------ -------------------------------------------------- -------------------------------------------------- -------------------------------------------------- ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
       15210         0 bug          open                           2004/02/23                Installer                                                                                                                                                unknown                                                                                      n/a                            viktorov                                                                                                                    VPIplayer: the error message comes up during deinstallation: "-1612..."                                                                                           
       13624         1 bug          to_be_assigned                 2003/10/13                VPIplayer                                                                                                                                                1.0                            1.0                            2                              1                              a.lowery                       ronnie                                                                                       Player sliders: top of inter slider gives invalid integer! (again) 55                                                                                            

 (476 rows affected) 
 */
