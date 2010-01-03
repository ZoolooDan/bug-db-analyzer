using System;
using System.Diagnostics;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.DataAccessLayer.BLToolkitProvider;

namespace BugDB.DAL.Tests
{
  /// <summary>
  ///This is a test class for BLToolkitDataProviderTest and is intended
  ///to contain all BLToolkitDataProviderTest Unit Tests
  ///</summary>
  [DeploymentItem(@"ReferenceData\BugDB_Clean.bak", "ReferenceData")]
  [TestClass]
  public class BLToolkitDataProviderTest
  {
    #region Private Fields
    private const string SqlCmdArgsTemplate = @"-S .\SQLEXPRESS -b -r -Q ""{0}""";
    private const string SqlCmdExe = @"c:\Program Files\Microsoft SQL Server\90\Tools\Binn\SQLCMD.EXE";
    private const string RestoreQueryTemplate = @"RESTORE DATABASE BugDB FROM DISK = N'{0}' WITH FILE = 1, NOUNLOAD, STATS = 10";
    private const string DbBackUpFileName = @"ReferenceData\BugDB_Clean.bak";

    /// <summary>
    /// BugDB storage data provider.
    /// </summary>
    private IDataProvider m_provider;
    #endregion Private Fields

    #region Public Properties
    /// <summary>
    /// Gets or sets the test context which provides
    /// information about and functionality for the current test run.
    ///</summary>
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
    /// Restore database and create provider.
    /// </summary>
    [TestInitialize]
    public void DataInitialize()
    {
      // Path to backup file
      string backupFileName = Path.Combine(
        this.TestContext.TestDeploymentDir, DbBackUpFileName);

      // Restore
      RestoreDatabase(backupFileName);

      // Create provider
      m_provider = new BLToolkitDataProvider();
    }

    /// <summary>
    /// Restore database from specified file.
    /// </summary>
    public static void RestoreDatabase(string backupFileName)
    {
      // Query to be executed
      string restoreQuery = String.Format(RestoreQueryTemplate, backupFileName);

      // Arguments
      string sqlCmdArgs = String.Format(SqlCmdArgsTemplate, restoreQuery);

      // Execute program
      ExecuteProcess(SqlCmdExe, sqlCmdArgs);
    }

    /// <summary>
    /// A test for GetBugs().
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"ReferenceData\BugDB_Clean.bak")]
    public void GetBugsTest()
    {
      BLToolkitDataProvider target = new BLToolkitDataProvider(); // TODO: Initialize to an appropriate value
      Bug[] expected = null; // TODO: Initialize to an appropriate value
      Bug[] actual;
      actual = target.GetBugs();
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    /// A test for GetBugRevisions
    /// </summary>
    [TestMethod()]
    public void GetBugRevisionsTest()
    {
      BLToolkitDataProvider target = new BLToolkitDataProvider(); // TODO: Initialize to an appropriate value
      int bugNumber = 0; // TODO: Initialize to an appropriate value
      Revision[] expected = null; // TODO: Initialize to an appropriate value
      Revision[] actual;
      actual = target.GetBugRevisions(bugNumber);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    /// A test for GetApplications()
    /// </summary>
    [TestMethod()]
    public void GetApplicationsTest()
    {
      const string appTitle = "TestApp";

      // Create application
      CreateApplicatonTest();

      Application[] actual = m_provider.GetApplications();
      Assert.IsNotNull(actual);
      Assert.AreEqual(1, actual.Length);
      Assert.IsNotNull(actual[0]);
      Assert.AreNotEqual(0, actual[0]);
      Assert.AreEqual(appTitle, actual[0].Title);
    }

    /// <summary>
    /// A test for GetApplication(id)
    /// </summary>
    [TestMethod()]
    public void GetApplicationTest()
    {
      BLToolkitDataProvider target = new BLToolkitDataProvider(); // TODO: Initialize to an appropriate value
      int appId = 0; // TODO: Initialize to an appropriate value
      Application expected = null; // TODO: Initialize to an appropriate value
      Application actual;
      actual = target.GetApplication(appId);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    /// A test for CreateApplicaton()
    /// </summary>
    [TestMethod()]
    public void CreateApplicatonTest()
    {
      const string appTitle = "TestApp";

      Application app = new Application() { Title = appTitle };
      Application actual = m_provider.CreateApplicaton(app);
      Assert.IsNotNull(actual);
      Assert.AreNotEqual(0, actual.Id);
      Assert.AreEqual(appTitle, actual.Title);
    }

    /// <summary>
    ///A test for BLToolkitDataProvider Constructor
    ///</summary>
    [TestMethod()]
    public void BlToolkitDataProviderConstructorTest()
    {
      BLToolkitDataProvider target = new BLToolkitDataProvider();
      Assert.Inconclusive("TODO: Implement code to verify target");
    }

    #region Helper Methods
    /// <summary>
    /// Executes specified program with specified arguments.
    /// </summary>
    public static void ExecuteProcess(string cmd, string args)
    {
      Process process = new Process();
      process.StartInfo.FileName = cmd;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = true;
      process.StartInfo.Arguments = args;
      process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      process.StartInfo.CreateNoWindow = true;
      if( process.Start() )
      {
        // Wait until finished
        process.WaitForExit();

        // Request results of execution
        string result = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        int exitCode = process.ExitCode;

        // Report error if any
        Assert.AreEqual(0, exitCode, error);
        // Report process output if succeeded
        //this.TestContext.WriteLine(result);
      }
      else
      {
        Assert.Fail(String.Format("Failed to start '{0}'", cmd));
      }
    }
    #endregion Helper Methods
  }
}
