using System.IO;
using System.Windows;

using BugDB.Aggregator;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.BLToolkitProvider;
using BugDB.DataAccessLayer.DataTransferObjects;

using Application=BugDB.DataAccessLayer.DataTransferObjects.Application;

using System.Reflection;


namespace BugDB.Analyzer.GUI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    #region Private Fields
    private const string CreateDbScript = @"DatabaseScripts\BugDB3.sql";

    private IDataProvider m_provider;
    private bool m_dbFilled = false;
    #endregion Private Fields

    #region Constructors
    public MainWindow()
    {
      string dbScriptPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      dbScriptPath = Path.Combine(dbScriptPath, CreateDbScript);

      m_provider = new BLToolkitDataProvider(dbScriptPath);

      InitializeComponent();
    }
    #endregion Constructors

    #region Event Handlers
    private void OnFetchDataButton_Click(object sender, RoutedEventArgs e)
    {
      // Initialize database with data
      FillDatabase();

      // Request applications
      Application[] apps = m_provider.GetApplications();
      // Assign them to list data source
      appsListBox.ItemsSource = apps;
      appsListBox.DisplayMemberPath = "Title";
    }

    private void OnTargetReleasesListBox_SelectionChanged(object sender,
                                                          System.Windows.Controls.SelectionChangedEventArgs e)
    {
      if( appsListBox.SelectedIndex != -1 )
      {
        Application app = (Application)appsListBox.SelectedValue;
        // Request all releases
        Release[] releases = m_provider.GetApplicationReleases(app.Id);
        // And show
        targetReleasesListBox.ItemsSource = releases;
        targetReleasesListBox.DisplayMemberPath = "Title";
      }
    }
    #endregion Event Handlers

    #region Helper Methods
    /// <summary>
    /// Fills database from scratch.
    /// </summary>
    private void FillDatabase()
    {
      // Fill DB only once
      if( !m_dbFilled )
      {
        // Initialize storage
        m_provider.InitializeStorage();

        string bugsFilePath = @"Data\bugs.txt";
        // Fill database from query results file
        StorageAggregator aggregator = new StorageAggregator(m_provider);
        aggregator.FillStorage(bugsFilePath);
      }
    }
    #endregion Helper Methods
  }
}