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
    public RevisionQueryTests() : base(false)
    {
      
    }

    public override void FillStorage()
    {
      // Fill storage with data
      string dbDataPath = Path.Combine(
        TestContext.TestDeploymentDir,
        @"ReferenceData\getRevisionsData.txt");
      var aggregator = new StorageAggregator(Provider);
      aggregator.FillStorage(dbDataPath);
    }

    /// <summary>
    /// Test for GetRevisions(query).
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"ReferenceData\getRevisionsData.txt", "ReferenceData")]
    public void GetRevisionsByQueryTest()
    {
      var prms = new QueryParams
      {
        Apps = new[]
                              {
                                (from a in Provider.GetApplications()
                                 where a.Title == "App1"
                                 select a.Id).First()
                              }
      };

      // Get all revisions for bugs where application of 
      // the most recent revision is "App1"
      Revision[] actual = Provider.GetRevisions(prms);

      Assert.AreEqual(6, actual.Length);
    }
  }
}