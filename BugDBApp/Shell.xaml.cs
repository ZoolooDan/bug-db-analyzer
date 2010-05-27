using System;
using System.Windows;
using Microsoft.Practices.Composite.Presentation;

namespace BugDB.Application
{
  /// <summary>
  /// Main application window.
  /// </summary>
  public partial class Shell : Window, IShellView
  {
    public Shell()
    {
      InitializeComponent();
    }

    public void ShowView()
    {
      this.Show();
    }
  }
}
