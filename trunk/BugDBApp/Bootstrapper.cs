using System;
using System.Windows;
using BugDB.Modules.BugReport;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;

namespace BugDB.Application
{
  public class Bootstrapper : UnityBootstrapper
  {
    protected override IModuleCatalog GetModuleCatalog()
    {
      var catalog = new ModuleCatalog();
      catalog.AddModule(typeof(BugReportModule));

      return catalog;
    }

    protected override void ConfigureContainer()
    {
      Container.RegisterType<IShellView, Shell>();

      base.ConfigureContainer();
    }

    protected override DependencyObject CreateShell()
    {
      ShellPresenter presenter = Container.Resolve<ShellPresenter>();
      IShellView view = presenter.View;

      view.ShowView();

      return view as DependencyObject;
    }
  }
}