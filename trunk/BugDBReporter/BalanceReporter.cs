using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugDB.Reporter
{
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
  /// ForAnalysis : open, reopen                   
  /// ForFixing   : to_be_assigned, assigned, active
  /// Postponed   : analysed, suspend
  /// ForTesting  : invalid, duplicate, works_for_me, fixed, 
  ///               modelling, regression, test_cases
  /// Closed      : verified, closed, deleted
  /// 
  /// Different strategies could be used for treating both 
  /// added and removed states:
  /// 
  /// 1) Added   : (ForTesting, Closed)->(ForAnalysis, ForFixing, Postponed)
  ///    Removed : (ForAnalysis, ForFixing, Postponed)->(ForTesting, Closed) 
  /// 
  /// 2) Added   : (Postponed, ForTesting, Closed)->(ForAnalysis, ForFixing)
  ///    Removed : (ForAnalysis, ForFixing)->(Postponed, ForTesting, Closed)
  /// 
  /// First approach considers Postopned bugs as removed
  /// from development process (though temporary). Naturally 
  /// when it's changed from Postponed to ForAnaysis or ForFixing
  /// it's treated as Added. 
  /// As opposed to that second approach treats Postponed as
  /// something still for work thus - Added.
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
    /// <summary>
    /// Creates balance report.
    /// </summary>
    public void CreateReport()
    {
      
    }
  }
}
