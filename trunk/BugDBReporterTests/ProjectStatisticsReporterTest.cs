﻿using System;
using System.Data;
using System.IO;

using BugDB.Aggregator;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.BLToolkitProvider;
using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.Reporter;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BugDBReporterTests
{
  /// <summary>
  /// ProjectStatisticsReporter tests.
  /// </summary>
  [TestClass]
  [DeploymentItem(@"DatabaseScripts\BugDB3.sql", "DatabaseScripts")]
  [DeploymentItem(@"Config\ReporterConfig.xml", "Config")]
  [DeploymentItem(@"ReferenceData\projectReporterData.txt", "ReferenceData")]
  public class ProjectStatisticsReporterTest
  {
    #region Private Fields
    private IDataProvider m_provider;
    #endregion Private Fields

    /// <summary>
    /// Gets or sets the test context which provides
    /// information about and functionality for the current test run.
    /// </summary>
    public TestContext TestContext { get; set; }

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
                                            @"ReferenceData\projectReporterData.txt"));
      }
    }
    #endregion Test Setup

    /// <summary>
    ///A test for CreateReport
    ///</summary>
    [TestMethod]
    public void CreateReportTest()
    {
      Revision[] revisions = null; // TODO: Initialize to an appropriate value
      var period = new GroupPeriod(); // TODO: Initialize to an appropriate value
      var fromDate = new DateTime(); // TODO: Initialize to an appropriate value
      var toDate = new DateTime(); // TODO: Initialize to an appropriate value
      var target = new ProjectStatisticsReporter(revisions, period, fromDate, toDate);
        // TODO: Initialize to an appropriate value
      DataSet expected = null; // TODO: Initialize to an appropriate value
      DataSet actual;
      actual = target.CreateReport();
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }
  }
}