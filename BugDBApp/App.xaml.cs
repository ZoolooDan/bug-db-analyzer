using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using BugDB.Application;
using Microsoft.Practices.Composite.UnityExtensions;

namespace BugDBApp
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      UnityBootstrapper bootstrapper = new Bootstrapper();
      bootstrapper.Run();
    }
  }
}
