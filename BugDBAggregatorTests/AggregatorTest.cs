using BugDB.Aggregator;
using BugDB.DAL.Tests;
using BugDB.DataAccessLayer.BLToolkitProvider;
using BugDB.DataAccessLayer.DataTransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using BugDB.DataAccessLayer;

namespace BugDBAggregatorTests
{
  /// <summary>
  ///This is a test class for AggregatorTest and is intended
  ///to contain all AggregatorTest Unit Tests
  ///</summary>
  [TestClass]
//  [DeploymentItem(ReferenceDataDir + "\\" + DbBackUpFileName, ReferenceDataDir)]
//  [DeploymentItem(@"ReferenceData\BugDB_Clean2.bak", "ReferenceData")]
  public class AggregatorTest
  {
    #region Private Fields
    private const string ReferenceDataDir = "ReferenceData";
    private const string RecordSourceFileName = "recordsSource.txt";
    private const string DbBackUpFileName = @"BugDB_Clean2.bak";
    #endregion Private Fields

    #region Public Properites
    /// <summary>
    /// Gets or sets the test context which provides
    /// information about and functionality for the current test run.
    /// </summary>
    public TestContext TestContext { get; set; }
    #endregion Public Properites

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

    //Use TestInitialize to run code before running each test
    [TestInitialize]
    public void TestInitialize()
    {
      // Path to backup file
      string backupFileName = Path.Combine(
        this.TestContext.TestDeploymentDir, 
        Path.Combine(ReferenceDataDir, DbBackUpFileName));

      // Restore
      BLToolkitDataProviderTest.RestoreDatabase(backupFileName);
    }

    /// <summary>
    /// A test for FillDatabase
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"ReferenceData\recordsSource.txt", "ReferenceData")]
    [DeploymentItem(@"ReferenceData\BugDB_Clean2.bak", "ReferenceData")]
    public void FillDatabaseTest()
    {
      string path = Path.Combine(this.TestContext.TestDeploymentDir,
                                 Path.Combine(ReferenceDataDir, RecordSourceFileName));

      using(Stream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
      {
        IDataProvider provider = new BLToolkitDataProvider();
        DbAggregator aggregator = new DbAggregator(provider);
        aggregator.FillDatabase(stream);

        Application[] apps = provider.GetApplications();
        Assert.AreNotEqual(0, apps.Length);

        Release[] rels = provider.GetApplicationReleases(apps[0].Id);
        Assert.AreNotEqual(0, rels.Length);
      }
    }
  }
}
