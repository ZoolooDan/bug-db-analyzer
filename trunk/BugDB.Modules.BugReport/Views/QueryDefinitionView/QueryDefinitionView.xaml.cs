using System.ComponentModel;
using System.Windows.Controls;
using BugDB.Common;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.DataTransferObjects;
using Microsoft.Practices.Composite.Events;
using System.Linq;

namespace BugDB.Modules.BugReport.Views
{
  /// <summary>
  /// View for defining query to BugDB.
  /// </summary>
  public partial class QueryDefinitionView : UserControl
  {
    private IEventAggregator m_eventAggregator;

    public QueryDefinitionView(IEventAggregator eventAggregator)
    {
      InitializeComponent();

      // Set sizes of control to Auto in runtime mode
      // http://stackoverflow.com/questions/75495/wpf-usercontrol-design-time-size
      if( LicenseManager.UsageMode != LicenseUsageMode.Designtime )
      {
        this.Width = double.NaN; 
        this.Height = double.NaN;
      }

      m_eventAggregator = eventAggregator;
    }

    public QueryDefinitionPresentationModel Model
    {
      get { return this.DataContext as QueryDefinitionPresentationModel; }
      set { this.DataContext = value; }
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
    }

    private void m_apps_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //Model.NotifyAppsChanged();
    }

  }
}
