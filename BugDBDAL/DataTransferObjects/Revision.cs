using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugDB.DataAccessLayer.DataTransferObjects
{
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
  public class Revision
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
    public BugStatus? Status { get; set; }

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

    /// <summary>
    /// Prints debugging information.
    /// </summary>
    public override string ToString()
    {
      string str = String.Format("{{{0},{1}}}-{2},{3},{4}", 
        this.BugNumber, this.Rev,
        this.Type, 
        this.Status.HasValue ? this.Status.ToString() : "None",
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
