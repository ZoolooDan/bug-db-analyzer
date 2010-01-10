using System.IO;

using BugDB.Aggregator;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.BLToolkitProvider;
using BugDB.Reporter;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BugDBReporterTests
{
  /// <summary>
  /// Test class for BalanceReporter.
  /// </summary>
  [TestClass]
  [DeploymentItem(@"DatabaseScripts\BugDB3.sql", "DatabaseScripts")]
  [DeploymentItem(@"ReferenceData\approachOne.txt", "ReferenceData")]
  public class BalanceReporterTest
  {
    #region Private Fields
    private IDataProvider m_provider;
    #endregion Private Fields

    #region Public Properties
    /// <summary>
    /// Gets or sets the test context which provides
    /// information about and functionality for the current test run.
    /// </summary>
    public TestContext TestContext { get; set; }
    #endregion Public Properties

    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion

    /// <summary>
    /// Initialize database only once for all tests
    /// until they only query data.
    /// </summary>
    [TestInitialize]
    public void TestInitialize()
    {
      if( m_provider == null )
      {
        // Path to create script
        string dbCreateScriptPath = Path.Combine(
          this.TestContext.TestDeploymentDir, @"DatabaseScripts\BugDB3.sql");
        // Create provider
        m_provider = new BLToolkitDataProvider(dbCreateScriptPath);
        // Initialize storage (recreate database)
        m_provider.InitializeStorage();
        // Fill storage
        StorageAggregator aggregator = new StorageAggregator(m_provider);
        aggregator.FillStorage(Path.Combine(this.TestContext.TestDeploymentDir,
                                            @"ReferenceData\approachOne.txt"));
      }
    }

    /// <summary>
    ///A test for CreateReport
    ///</summary>
    [TestMethod]
    public void CreateReportTest()
    {
      var reporter = new BalanceReporter(); // TODO: Initialize to an appropriate value
      reporter.CreateReport();
      Assert.Inconclusive("A method that does not return a value cannot be verified.");
    }
  }
}