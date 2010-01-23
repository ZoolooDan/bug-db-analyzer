using System;
using System.Collections.Generic;
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
  [DeploymentItem(@"Config\ReporterConfig.xml", "Config")]
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
    /// ByDay report test.
    /// </summary>
    [TestMethod]
    public void ByDayTest()
    {
      var reporter = new BalanceReporter(m_provider);
      
      List<Group> report = reporter.CreateReport(GroupPeriod.ByDay, false);
      Assert.AreEqual(9, report.Count);
      Assert.AreEqual(new DateTime(2000, 12, 08), report[0].IntervalStart);
      Assert.AreEqual(new DateTime(2000, 12, 08), report[0].IntervalEnd);
      Assert.AreEqual(1, report[0].Added);
      Assert.AreEqual(0, report[0].Removed);
      Assert.AreEqual(0, report[0].Postponed);
      Assert.AreEqual(0, report[0].Reactivated);
    }

    /// <summary>
    /// ByWeek report test.
    /// </summary>
    [TestMethod]
    public void ByWeekTest()
    {
      var reporter = new BalanceReporter(m_provider);
      
      List<Group> report = reporter.CreateReport(GroupPeriod.ByWeek, false);
      Assert.AreEqual(2, report.Count);
      Assert.AreEqual(new DateTime(2000, 12, 04), report[0].IntervalStart);
      Assert.AreEqual(new DateTime(2000, 12, 10), report[0].IntervalEnd);
      Assert.AreEqual(1, report[1].Added);
      Assert.AreEqual(3, report[1].Removed);
      Assert.AreEqual(0, report[1].Postponed);
      Assert.AreEqual(1, report[1].Reactivated);
    }

    /// <summary>
    /// ByMonth report test.
    /// </summary>
    [TestMethod]
    public void ByMonthTest()
    {
      var reporter = new BalanceReporter(m_provider);
      
      List<Group> report = reporter.CreateReport(GroupPeriod.ByMonth, false);
      Assert.AreEqual(1, report.Count);
      Assert.AreEqual(new DateTime(2000, 12, 01), report[0].IntervalStart);
      Assert.AreEqual(new DateTime(2000, 12, 31), report[0].IntervalEnd);


    }

    /// <summary>
    /// ByQuater report test.
    /// </summary>
    [TestMethod]
    public void ByQuaterTest()
    {
      var reporter = new BalanceReporter(m_provider);
      
      // 01 Jan -
      // 02 Feb  1
      // 03 Mar -
      // 04 Apr -
      // 05 May  2
      // 06 Jun -
      // 07 Jul -
      // 08 Aug  3
      // 09 Sep -
      // 10 Nov -
      // 11 Oct  4
      // 12 Dec -

      List<Group> report = reporter.CreateReport(GroupPeriod.ByQuater, false);
      Assert.AreEqual(1, report.Count);
      Assert.AreEqual(new DateTime(2000, 10, 1), report[0].IntervalStart);
      Assert.AreEqual(new DateTime(2000, 12, 31), report[0].IntervalEnd);


    }

    /// <summary>
    /// ByYear report test.
    /// </summary>
    [TestMethod]
    public void ByYearTest()
    {
      var reporter = new BalanceReporter(m_provider);
      
      List<Group> report = reporter.CreateReport(GroupPeriod.ByYear, false);
      Assert.AreEqual(1, report.Count);
      Assert.AreEqual(new DateTime(2000, 1, 1), report[0].IntervalStart);
      Assert.AreEqual(new DateTime(2000, 12, 31), report[0].IntervalEnd);


    }

  }
}