﻿using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.BLToolkitProvider;
using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.Aggregator;


namespace BugDBAggregatorTests
{
  /// <summary>
  /// Tests for StorageAggregator.
  /// </summary>
  [TestClass]
  [DeploymentItem(@"DatabaseScripts\BugDB3.sql", "DatabaseScripts")]
  public class AggregatorTest
  {
    #region Private Fields
    private const string RecordSourceFileName = @"ReferenceData\aggregatorSource.txt";
    private const string DbCreateScript = @"DatabaseScripts\BugDB3.sql";

    private IDataProvider m_provider;
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
      // Path to create script
      string dbCreateScriptPath = Path.Combine(
        this.TestContext.TestDeploymentDir, DbCreateScript);
      // Create provider
      m_provider = new BLToolkitDataProvider(dbCreateScriptPath);
      // Initialize storage (recreate database)
      m_provider.InitializeStorage();
    }

    /// <summary>
    /// A test for FillStorage().
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"ReferenceData\aggregatorSource.txt", "ReferenceData")]
    public void FillStorageTest()
    {
      string path = Path.Combine(this.TestContext.TestDeploymentDir, RecordSourceFileName);

      // Check that storage is empty
      Application[] apps = m_provider.GetApplications();
      Assert.AreEqual(0, apps.Length);

      StorageAggregator aggregator = new StorageAggregator(m_provider);
      aggregator.FillStorage(path);

      apps = m_provider.GetApplications();
      Assert.AreNotEqual(0, apps.Length);

      Application tmmApp = Array.Find(apps, app => app.Title == "VPI TMM/CM");
      Assert.IsNotNull(tmmApp, "No 'VPI TMM/CM' application.");

      Release[] rels = m_provider.GetApplicationReleases(tmmApp.Id);
      Assert.AreNotEqual(0, rels.Length);

      // Check that there are no revisions which starts or ends with quote ('"')
      Array.ForEach(rels, item => Assert.IsFalse(item.Title.StartsWith("\"") || 
                                                 item.Title.EndsWith("\""),
                                                 "Release shouldn't start with quote."));

      Assert.IsTrue(Array.Exists(rels, rel => rel.Title == "4.0"), "No '4.0' release.");
    }
  }
}