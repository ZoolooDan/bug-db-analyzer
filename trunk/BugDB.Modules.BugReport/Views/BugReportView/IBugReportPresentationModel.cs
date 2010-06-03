using System;
using System.Collections.Generic;

namespace BugDB.Modules.BugReport
{
  public class BugModel
  {
    public int BugNumber { get; set; }
    public string Summary { get; set; }
    
  }

  public interface IBugReportPresentationModel
  {
    IBugReportView View { get; }

    IList<BugModel> Items { get; }
  }
}
