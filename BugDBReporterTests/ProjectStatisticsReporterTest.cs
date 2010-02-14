using System;
using System.Data;
using System.IO;
using System.Linq;

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
    /// Test Info table for whole period.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"ReferenceData\projRepRef_InfoTableTest_WholePeriod.xml", "ReferenceData")]
    public void InfoTableTest_WholePeriod()
    {
      TableTest_Impl("Info",
        "projRepRef_InfoTableTest_WholePeriod.xml",
        DateTime.MinValue, 
        DateTime.MaxValue);
    }

    /// <summary>
    /// Test Info table for specific period.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"ReferenceData\projRepRef_InfoTableTest_SpecPeriod.xml", "ReferenceData")]
    public void InfoTableTest_SpecificPeriod()
    {
      TableTest_Impl("Info", 
        "projRepRef_InfoTableTest_SpecPeriod.xml",
        new DateTime(2000, 12, 09), 
        new DateTime(2000, 12, 14));
    }

    /// <summary>
    /// Test Periods table for specific period.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"ReferenceData\projRepRef_PeriodsTableTest_SpecPeriod.xml", "ReferenceData")]
    public void PeriodsTableTest_SpecificPeriod()
    {
      TableTest_Impl("Periods",
        "projRepRef_PeriodsTableTest_SpecPeriod.xml",
        new DateTime(2000, 12, 09), 
        new DateTime(2000, 12, 14));
    }

    #region Helper Methods
    /// <summary>
    /// Common test for specific table.
    /// </summary>
    private void TableTest_Impl(string tableName, string refFileName, 
      DateTime from, DateTime to)
    {
      Application app = (from a in m_provider.GetAllApplications()
                        where a.Title == "App1"
                        select a).First();

      Revision[] revisions = m_provider.GetRevisions(
        new QueryParams
        {
          Apps = new[] {app.Id}
        });

      var period = GroupingPeriod.Week;
      var reporter = new ProjectStatisticsReporter(revisions, period, from, to);

      // Create report
      DataSet actual = reporter.CreateReport();
      // Write it for reference
      DataSet filtered = WriteDataSetRef(actual, refFileName,
        new[] {tableName});

      // Read reference
      var expected = ReadDataSet(refFileName);

      // Compare
      Assert.AreEqual(expected.GetXml(), filtered.GetXml());
    }

    /// <summary>
    /// Writes dataset for reference.
    /// </summary>
    private DataSet WriteDataSetRef(DataSet dataSet, string fileName, string[] tables)
    {
      string fileName2 = Path.GetFileNameWithoutExtension(fileName) +
                         "_" + Path.GetExtension(fileName);
      return WriteDataSet(dataSet, fileName2, tables);
    }


    /// <summary>
    /// Writes only specified tables from dataset.
    /// </summary>
    private DataSet WriteDataSet(DataSet dataSet, string fileName, string[] tables)
    {
      string path = Path.Combine(
        this.TestContext.TestDeploymentDir,
        Path.Combine("ReferenceData", fileName));

      DataSet clone = dataSet.Copy();
      foreach( DataTable table in clone.Tables )
      {
        if( Array.IndexOf(tables, table.TableName) == -1 )
        {
          table.Clear();
        }
      }
      clone.WriteXml(path);

      return clone;
    }

    /// <summary>
    /// Reads dataset from specified file.
    /// </summary>
    private ProjectStatisticDataSet ReadDataSet(string fileName)
    {
      var dataSet = new ProjectStatisticDataSet();
      string refDataPath = Path.Combine(
        this.TestContext.TestDeploymentDir,
        Path.Combine("ReferenceData", fileName));
      dataSet.ReadXml(refDataPath);
      return dataSet;
    }

    /// <summary>
    /// Reads specific tables to dataset from specified file.
    /// </summary>
    private ProjectStatisticDataSet ReadDataSet(string fileName, 
      string[] tables)
    {
      var dataSet = new ProjectStatisticDataSet();
      string refDataPath = Path.Combine(
        this.TestContext.TestDeploymentDir,
        Path.Combine("ReferenceData", fileName));

      // Read all
      dataSet.ReadXml(refDataPath);

      // But clear tables which aren't specified
      foreach(DataTable table in dataSet.Tables)
      {
        if( Array.IndexOf(tables, table.TableName) == -1 )
        {
          table.Clear();
        }
      }
      return dataSet;
    }
    #endregion Helper Methods
  }
}