namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Bug represents one particular defect or feature.
  /// </summary>
  /// <remarks>
  /// Although bug itself has many properties bug itself
  /// has only one unchanging field - it's number.
  /// All the rest properties may change through bug
  /// lifetime and are stored in so called revisions.
  /// Last (most recent) revision represents current
  /// state of the bug record.
  /// </remarks>
  public class Bug
  {
    /// <summary>
    /// Unique bug number.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Most recent revision ID.
    /// </summary>
    public int MostRecentRevId { get; set; }
  }
}

/*
 number      subnumber deadline     status                         date         closedate    application                                        modul                                              submodul                                           apprelease                     frelease                       severity                       priority                       contributor                    leader                         developer                      qa                             summary                                                                                                                                                          
 ----------- --------- ------------ ------------------------------ ------------ ------------ -------------------------------------------------- -------------------------------------------------- -------------------------------------------------- ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
       15210         0 bug          open                           2004/02/23                Installer                                                                                                                                                unknown                                                                                      n/a                            viktorov                                                                                                                    VPIplayer: the error message comes up during deinstallation: "-1612..."                                                                                           
       13624         1 bug          to_be_assigned                 2003/10/13                VPIplayer                                                                                                                                                1.0                            1.0                            2                              1                              a.lowery                       ronnie                                                                                       Player sliders: top of inter slider gives invalid integer! (again) 55                                                                                            

 (476 rows affected) 
 */
