using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BugDB.Modules.BugReport.Misc
{
  public class SimpleModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected void FirePropertyChanged(string propName)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propName));
      }
    }

  }
}
