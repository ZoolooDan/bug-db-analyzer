using System;
using System.Collections.Generic;

namespace BugDB.Modules.BugReport
{
  public class Item
  {
    public int Number { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
  }

  public interface IBugReportPresentationModel
  {
    IBugReportView View { get; }

    IList<Item> Items { get; }
  }
}
