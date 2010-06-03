using System.Windows.Controls;

namespace BugDB.Modules.BugReport
{
  /// <summary>
  /// Bug report grid view.
  /// </summary>
  public partial class BugReportView : UserControl, IBugReportView
  {
    public BugReportView()
    {
      InitializeComponent();
    }

    public IBugReportPresentationModel Model
    {
      get { return this.DataContext as IBugReportPresentationModel; }
      set { this.DataContext = value; }
    }
  }
}
