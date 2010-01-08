using BLToolkit.Mapping;


namespace BugDB.DataAccessLayer.BLToolkitProvider.EntityDataModel
{
  /// <summary>
  /// EDM object for working with Bugs table.
  /// </summary>
  public class Bug
  {
    /// <summary>
    /// Unique bug number.
    /// </summary>
    [MapField("bug_number")]
    public int Number { get; set; }

    /// <summary>
    /// Most recent revision ID.
    /// </summary>
    [MapField("recent_revision")]
    public int RecentRevisionId { get; set; }
  }
}


/*
 number      subnumber deadline     status                         date         closedate    application                                        modul                                              submodul                                           apprelease                     frelease                       severity                       priority                       contributor                    leader                         developer                      qa                             summary                                                                                                                                                          
 ----------- --------- ------------ ------------------------------ ------------ ------------ -------------------------------------------------- -------------------------------------------------- -------------------------------------------------- ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
       15210         0 bug          open                           2004/02/23                Installer                                                                                                                                                unknown                                                                                      n/a                            viktorov                                                                                                                    VPIplayer: the error message comes up during deinstallation: "-1612..."                                                                                           
       13624         1 bug          to_be_assigned                 2003/10/13                VPIplayer                                                                                                                                                1.0                            1.0                            2                              1                              a.lowery                       ronnie                                                                                       Player sliders: top of inter slider gives invalid integer! (again) 55                                                                                            

 (476 rows affected) 
 */
