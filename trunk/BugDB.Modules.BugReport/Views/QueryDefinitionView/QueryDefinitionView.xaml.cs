using System.ComponentModel;
using System.Windows.Controls;

namespace BugDB.Modules.BugReport.Views
{
  /// <summary>
  /// View for defining query to BugDB.
  /// </summary>
  public partial class QueryDefinitionView : UserControl
  {
    public QueryDefinitionView()
    {
      InitializeComponent();
      // Set sizes of control to Auto in runtime mode
      // http://stackoverflow.com/questions/75495/wpf-usercontrol-design-time-size
      if( LicenseManager.UsageMode != LicenseUsageMode.Designtime )
      {
        this.Width = double.NaN; 
        this.Height = double.NaN;
      }
    }

    public QueryDefinitionPresentationModel Model
    {
      get { return this.DataContext as QueryDefinitionPresentationModel; }
      set { this.DataContext = value; }
    }

  }
}
