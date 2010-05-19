using System.IO;
using System.Linq;
using BugDB.Aggregator;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BugDB.DAL.Tests
{
  /// <summary>
  /// Summary description for RevisionQueryTests
  /// </summary>
  [TestClass]
  public class RevisionQueryTests : DatabaseStorageTestBase
  {
    #region Additional test attributes

    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //

    #endregion

    /// <summary>
    /// Test for GetRevisions(query).
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"ReferenceData\getRevisionsData.txt", "ReferenceData")]
    public void GetRevisionsByQueryTest()
    {
      // Fill storage with data
      string dbDataPath = Path.Combine(
        TestContext.TestDeploymentDir,
        @"ReferenceData\getRevisionsData.txt");
      var aggregator = new StorageAggregator(m_provider);
      aggregator.FillStorage(dbDataPath);

      var prms = new QueryParams
      {
        Apps = new[]
                              {
                                (from a in m_provider.GetApplications()
                                 where a.Title == "App1"
                                 select a.Id).First()
                              }
      };

      // Get all revisions for bugs where application of 
      // the most recent revision is "App1"
      Revision[] actual = m_provider.GetRevisions(prms);

      Assert.AreEqual(6, actual.Length);
    }
  }
}