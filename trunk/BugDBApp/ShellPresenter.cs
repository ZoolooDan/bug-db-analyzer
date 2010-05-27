using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugDB.Application
{
  public class ShellPresenter
  {
    public ShellPresenter(IShellView view)
    {
      View = view;
    }

    public IShellView View { get; private set; }
  }

}
