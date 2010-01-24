using System.Collections.Generic;
using System.IO;

using BugDB.Aggregator;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.BLToolkitProvider;
using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.Reporter.RevisionTransition;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BugDBReporterTests
{
  /// <summary>
  /// Test for RevisionTransitionFilter class.
  /// </summary>
  [TestClass]
  [DeploymentItem(@"DatabaseScripts\BugDB3.sql", "DatabaseScripts")]
  [DeploymentItem(@"Config\ReporterConfig.xml", "Config")]
  [DeploymentItem(@"ReferenceData\approachOne.txt", "ReferenceData")]
  public class RevisionTransitionFilterTest
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

    #region Test Setup
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
    #endregion Test Setup
    
    /// <summary>
    /// A test for GetTransitions
    ///</summary>
    [TestMethod]
    [DeploymentItem("BugDB.Reporter.dll")]
    public void GetTransitionsTest()
    {
      RevisionTransitionFilter filter = new RevisionTransitionFilter();
      Bug[] bugs = m_provider.GetBugs();
      Revision[] revisions = m_provider.GetBugRevisions(bugs[0].Number);

      var transitions = new List<RevisionStatusTransition>(filter.GetTransitions(revisions));
      var transitionsExp = new List<RevisionStatusTransition>
                           {
                             new RevisionStatusTransition() {}
                           };
    }
  }
}