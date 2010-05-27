using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugDB.Modules.BugReport
{
  class BugReportPresentationModel : IBugReportPresentationModel
  {
    private IList<Item> m_items;

    public BugReportPresentationModel(IBugReportView view)
    {
      this.View = view;
      this.View.Model = this;
    }

    public IBugReportView View { get; set; }

    public IList<Item> Items
    {
      get
      {
        if( m_items == null )
        {
          m_items = new List<Item> {
            new Item {Number = 1, Title = "Item1", Date = DateTime.Now},
            new Item {Number = 2, Title = "Item2", Date = DateTime.Now},
            new Item {Number = 3, Title = "Item3", Date = DateTime.Now}
          };
        }
        return m_items;
      }
    }
  }
}
