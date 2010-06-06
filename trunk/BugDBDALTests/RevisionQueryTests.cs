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
  [DeploymentItem(@"ReferenceData\getRevisionsData.txt", "ReferenceData")]
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

    ////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Query by application, module and submodule.
    /// </summary>
    [TestMethod]
    public void GetRevisionsByApp()
    {
      int[] apps = new[]
                   {
                     (from a in Provider.GetApplications()
                      where a.Title == "App1"
                      select a.Id).First()
                   };
      int[] modules = new[]
                      {
                        (from m in Provider.GetModules(apps[0])
                         where m.Title == "Mod1"
                         select m.Id).First()
                      };
      int[] subModules = new[]
                         {
                           (from m in Provider.GetSubModules(modules[0])
                            where m.Title == "SubMod2"
                            select m.Id).First()
                         };

      var prms = new QueryParams
                 {
                   Apps = apps
                 };

      // Get all revisions for bugs where application of 
      // the most recent revision is "App1"
      Revision[] actual = Provider.GetRevisions(prms);

      Assert.AreEqual(6, actual.Length);
      Assert.IsNotNull(actual[0].Summary);


      // Filter by modules only
      prms = new QueryParams
             {
               Modules = modules
             };

      actual = Provider.GetRevisions(prms);
      // 3 is right because Mod1 is specific to App1
      Assert.AreEqual(3, actual.Length);


      // Filter by submodules only
      prms = new QueryParams
             {
               SubModules = subModules
             };

      actual = Provider.GetRevisions(prms);
      Assert.AreEqual(3, actual.Length);
    }

    /// <summary>
    /// Query by found and target release.
    /// </summary>
    [TestMethod]
    public void GetRevisionsByRelease()
    {
      int[] apps = new[]
                   {
                     (from a in Provider.GetApplications()
                      where a.Title == "App1"
                      select a.Id).First()
                   };
      int[] rels = new[]
                   {
                     (from r in Provider.GetReleases(apps[0])
                      where r.Title == "1.0"
                      select r.Id).First()
                   };
      var prms = new QueryParams
                   {
                     FoundReleases = rels
                   };

      Revision[] actual = Provider.GetRevisions(prms);

      Assert.AreEqual(6, actual.Length);
    }

    /// <summary>
    /// Query by status.
    /// </summary>
    [TestMethod]
    public void GetRevisionsByStatus()
    {
      BugStatus[] statuses = new[]
                   {
                     BugStatus.ToBeAssigned
                   };
      var prms = new QueryParams
                   {
                     BugStatuses = statuses
                   };

      Revision[] actual = Provider.GetRevisions(prms);

      Assert.AreEqual(3, actual.Length);
    }

  }
}