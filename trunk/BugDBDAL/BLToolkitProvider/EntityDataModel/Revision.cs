using System;

using BLToolkit.Mapping;


namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// EDM object for working with Revisions table.
  /// </summary>
  public class Revision
  {
    /// <summary>
    /// Unique number of the bug record.
    /// </summary>
    [MapField("bug_number")]
    public int BugNumber { get; set; }

    /// <summary>
    /// Bug revision number.
    /// </summary>
    [MapField("revision")]
    public int Rev { get; set; }

    /// <summary>
    /// Type of the bug record.
    /// </summary>
    [MapField("bug_type")]
    public BugType Type { get; set; }
    
    /// <summary>
    /// Status of the revision.
    /// </summary>
    [MapField("status")]
    public BugStatus? Status { get; set; }

    /// <summary>
    /// Date of the revision.
    /// </summary>
    [MapField("date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// ID of application.
    /// </summary>
    [MapField("app_id")]
    public int AppId { get; set; }

    /// <summary>
    /// ID of module.
    /// </summary>
    [MapField("module_id")]
    public int? ModuleId { get; set; }

    /// <summary>
    /// ID of submodule.
    /// </summary>
    [MapField("submodule_id")]
    public int? SubModuleId { get; set; }

    /// <summary>
    /// ID of the application release during which this bug was found.
    /// </summary>
    [MapField("found_release_id")]
    public int? FoundReleaseId { get; set; }

    /// <summary>
    /// ID of the application release during which bug supposed to be fixed.
    /// </summary>
    [MapField("target_release_id")]
    public int? TargetReleaseId { get; set; }

    /// <summary>
    /// Importance of the bug for daily work and release.
    /// </summary>
    [MapField("severity")]
    public BugSeverity? Severity { get; set; }

    /// <summary>
    /// Bug processing priority.
    /// </summary>
    /// <remarks>
    /// Lower values mean higher priority.
    /// </remarks>
    [MapField("priority")]
    public int? Priority { get; set; }

    /// <summary>
    /// ID of person who created the bug.
    /// </summary>
    [MapField("contributor_id")]
    public int ContributorId { get; set; }
    
    /// <summary>
    /// ID of person who is responsible for the bug assignment.
    /// </summary>
    [MapField("leader_id")]
    public int? TeamLeaderId { get; set; }
    
    /// <summary>
    /// ID of person who is responsible for the bug fixing.
    /// </summary>
    [MapField("developer_id")]
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
    [MapField("qa_id")]
    public int? TesterId { get; set; }
    
    /// <summary>
    /// Short description of the bug.
    /// </summary>
    [MapField("summary")]
    public string Summary { get; set; }  
  }
}


/*
 number      subnumber deadline     status                         date         closedate    application                                        modul                                              submodul                                           apprelease                     frelease                       severity                       priority                       contributor                    leader                         developer                      qa                             summary                                                                                                                                                          
 ----------- --------- ------------ ------------------------------ ------------ ------------ -------------------------------------------------- -------------------------------------------------- -------------------------------------------------- ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
       15210         0 bug          open                           2004/02/23                Installer                                                                                                                                                unknown                                                                                      n/a                            viktorov                                                                                                                    VPIplayer: the error message comes up during deinstallation: "-1612..."                                                                                           
       13624         1 bug          to_be_assigned                 2003/10/13                VPIplayer                                                                                                                                                1.0                            1.0                            2                              1                              a.lowery                       ronnie                                                                                       Player sliders: top of inter slider gives invalid integer! (again) 55                                                                                            

 (476 rows affected) 
 */
