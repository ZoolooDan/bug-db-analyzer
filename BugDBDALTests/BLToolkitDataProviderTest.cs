using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.DataAccessLayer.BLToolkitProvider;
using BugDB.Aggregator;
using BugDB.Tests.Utils;

namespace BugDB.DAL.Tests
{
  /// <summary>
  /// BLToolkitDataProvider Tests.
  /// </summary>
  [DeploymentItem(@"DatabaseScripts\BugDB3.sql", "DatabaseScripts")]
  [TestClass]
  public class BLToolkitDataProviderTest : DatabaseStorageTestBase
  {
    ///////////////////////////////////////////////
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
    /// Tests GetApplication(id).
    /// </summary>
    [TestMethod()]
    public void GetApplicationTest()
    {
      const string appTitle = "TestApp";

      // Create application
      Application app = new Application() { Title = appTitle };
      app = m_provider.CreateApplicaton(app);

      Application actual = m_provider.GetApplication(app.Id);
      Assert.IsNotNull(actual);
      Assert.AreEqual(app.Id, actual.Id);
      Assert.AreEqual(appTitle, actual.Title);
    }

    /// <summary>
    /// A test for GetApplications()
    /// </summary>
    [TestMethod()]
    public void GetApplicationsTest()
    {
      const string appTitle1 = "TestApp1";
      const string appTitle2 = "TestApp2";

      // Create application
      Application app1 = new Application() { Title = appTitle1 };
      app1 = m_provider.CreateApplicaton(app1);
      Application app2 = new Application() { Title = appTitle2 };
      app2 = m_provider.CreateApplicaton(app2);

      Application[] actual = m_provider.GetApplications();
      Assert.IsNotNull(actual);
      Assert.AreEqual(2, actual.Length);
      Assert.IsNotNull(actual[0]);
      Assert.IsNotNull(actual[1]);
      Assert.AreEqual(appTitle1, actual[0].Title);
      Assert.AreEqual(appTitle2, actual[1].Title);
    }

    ///////////////////////////////////////////////
    /// <summary>
    /// Test CreateRelease().
    /// </summary>
    [TestMethod]
    public void CreateReleaseTest()
    {
      Application app = new Application() { Title = "App1" };
      app = m_provider.CreateApplicaton(app);

      Release release = new Release() { AppId = app.Id, Title = "Release1" };
      Release actual = m_provider.CreateRelease(release);

      Assert.IsNotNull(actual);
      Assert.AreNotEqual(0, actual.Id);
      Assert.AreEqual(release.Id, actual.Id);
      Assert.AreEqual(release.Title, actual.Title);
    }

    /// <summary>
    /// Test GetReleases().
    /// </summary>
    [TestMethod]
    public void GetReleasesTest()
    {
      Application app = new Application() { Title = "App1" };
      app = m_provider.CreateApplicaton(app);

      Release release1 = m_provider.CreateRelease(new Release() { AppId = app.Id, Title = "Release1" });
      Release release2 = m_provider.CreateRelease(new Release() { AppId = app.Id, Title = "Release2" });
      
      Release[] actual = m_provider.GetReleases(app.Id);

      Assert.IsNotNull(actual);
      Assert.AreEqual(2, actual.Length);
      Assert.AreNotEqual(0, actual[0].Id);
      Assert.AreEqual(release1.Title, actual[0].Title);
      Assert.AreNotEqual(0, actual[1].Id);
      Assert.AreEqual(release2.Title, actual[1].Title);
    }

    ///////////////////////////////////////////////
    /// <summary>
    /// Test CreateModule().
    /// </summary>
    [TestMethod]
    public void CreateModuleTest()
    {
      Application app = new Application() { Title = "App1" };
      app = m_provider.CreateApplicaton(app);

      Module module = new Module() { AppId = app.Id, Title = "Module1" };
      Module actual = m_provider.CreateModule(module);

      Assert.IsNotNull(actual);
      Assert.AreNotEqual(0, actual.Id);
      Assert.AreEqual(module.AppId, actual.AppId);
      Assert.AreEqual(module.Title, actual.Title);
    }

    /// <summary>
    /// Test GetModules().
    /// </summary>
    [TestMethod]
    public void GetModulesTest()
    {
      Application app1 = new Application() { Title = "App1" };
      app1 = m_provider.CreateApplicaton(app1);

      Module module1 = m_provider.CreateModule(new Module() { AppId = app1.Id, Title = "Module1" });
      Module module2 = m_provider.CreateModule(new Module() { AppId = app1.Id, Title = "Module2" });

      Application app2 = new Application() { Title = "App2" };
      m_provider.CreateModule(new Module() { AppId = app2.Id, Title = "Module1" });
      m_provider.CreateModule(new Module() { AppId = app2.Id, Title = "Module2" });

      Module[] actual = m_provider.GetModules(app1.Id);

      Assert.IsNotNull(actual);
      Assert.AreEqual(2, actual.Length);
      Assert.AreNotEqual(0, actual[0].Id);
      Assert.AreEqual(module1.Title, actual[0].Title);
      Assert.AreNotEqual(0, actual[1].Id);
      Assert.AreEqual(module2.Title, actual[1].Title);
    }

    ///////////////////////////////////////////////
    /// <summary>
    /// Test CreateSubModule().
    /// </summary>
    [TestMethod]
    public void CreateSubModuleTest()
    {
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    /// Test GetSubModules().
    /// </summary>
    [TestMethod]
    public void GetSubModulesTest()
    {
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    ///////////////////////////////////////////////
    /// <summary>
    /// Test GetSubModules().
    /// </summary>
    [TestMethod]
    public void CreatePersonTest()
    {
      Person person = new Person() { Login = "user1", Title = "User Clever" };
      person = m_provider.CreatePerson(person);
      Assert.IsNotNull(person);
      Assert.AreNotEqual(0, person.Id);
    }

    /// <summary>
    /// Test GetStaff().
    /// </summary>
    [TestMethod]
    public void GetStaffTest()
    {
      Person person = new Person() { Login = "user1", Title = "User Clever" };
      person = m_provider.CreatePerson(person);

      Person[] actual = m_provider.GetStaff();
      Assert.IsNotNull(actual);
      Assert.AreEqual(1, actual.Length);
      Assert.IsNotNull(actual[0]);
      Assert.AreEqual(person.Id, actual[0].Id);
      Assert.AreEqual(person.Login, actual[0].Login);
      Assert.AreEqual(person.Title, actual[0].Title);
    }

    ///////////////////////////////////////////////
    /// <summary>
    /// Tests GetBugs().
    /// </summary>
    [TestMethod]
    public void GetBugsTest()
    {
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    ///////////////////////////////////////////////
    /// <summary>
    /// Tests CreateRevision().
    /// </summary>
    [TestMethod]
    public void CreateRevisionTest()
    {
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    /// Tests GetRevisions().
    /// </summary>
    [TestMethod]
    public void GetRevisionsTest()
    {
      Assert.Inconclusive("Verify the correctness of this test method.");
    }


    /// <summary>
    /// Tests GetRevisions(bugNum).
    /// </summary>
    [TestMethod()]
    public void GetRevisionsOfBugTest()
    {
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    /// Test for GetRevisions(query).
    /// </summary>
    [TestMethod()]
    [DeploymentItem(@"ReferenceData\getRevisionsData.txt", "ReferenceData")]
    public void GetRevisionsByQueryTest()
    {
      // Fill storage with data
      string dbDataPath = Path.Combine(
        this.TestContext.TestDeploymentDir,
        @"ReferenceData\getRevisionsData.txt");
      StorageAggregator aggregator = new StorageAggregator(m_provider);
      aggregator.FillStorage(dbDataPath);

      QueryParams prms = new QueryParams()
                         {
                           Apps = new []
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

    ///////////////////////////////////////////////
    /// <summary>
    /// Test for CleanStorage.
    /// </summary>
    [TestMethod()]
    public void CleanStorageTest()
    {
      // Add some records to all collections
      Application app = new Application() { Title = "App1" };
      app = m_provider.CreateApplicaton(app);

      Person person = new Person() { Login = "user1", Title = "User Clever" };
      person = m_provider.CreatePerson(person);

      Module module = new Module() { AppId = app.Id, Title = "Module1" };
      module = m_provider.CreateModule(module);

      SubModule subModule = new SubModule() { ModuleId = module.Id, Title = "SubModule1" };
      subModule = m_provider.CreateSubModule(subModule);

      Release release = new Release() { AppId = app.Id, Title = "Release1" };
      release = m_provider.CreateRelease(release);

      Revision revision = new Revision()
      {
        AppId = app.Id,
        BugNumber = 10,
        Rev = 1,
        Date = DateTime.Now,
        Type = BugType.Bug,
        FoundReleaseId = release.Id,
        TargetReleaseId = release.Id,
        Severity = BugSeverity.Critical,
        Priority = 1,
        Status = BugStatus.Open,
        ContributorId = person.Id,
        ModuleId = module.Id,
        SubModuleId = subModule.Id,
        Summary = "Summary text"
      };

      revision = m_provider.CreateRevision(revision);

      // Check that everything is added to storage
      Assert.AreNotEqual(0, m_provider.GetApplications().Length);
      Assert.AreNotEqual(0, m_provider.GetModules(app.Id).Length);
      Assert.AreNotEqual(0, m_provider.GetSubModules(module.Id).Length);
      Assert.AreNotEqual(0, m_provider.GetStaff().Length);
      //      Assert.AreNotEqual(0, m_provider.GetBugs().Length);
      Assert.AreNotEqual(0, m_provider.GetRevisions(10).Length);
      //      Assert.AreNotEqual(0, m_provider.GetReleases().Length);

      // Clean storage
      m_provider.CleanStorage();
      // And check that it is cleared
      Assert.AreEqual(0, m_provider.GetApplications().Length);
      Assert.AreEqual(0, m_provider.GetModules(app.Id).Length);
      Assert.AreEqual(0, m_provider.GetSubModules(module.Id).Length);
      Assert.AreEqual(0, m_provider.GetStaff().Length);
      //      Assert.AreEqual(0, m_provider.GetBugs().Length);
      Assert.AreEqual(0, m_provider.GetRevisions(10).Length);
      //      Assert.AreEqual(0, m_provider.GetReleases().Length);
    }

    #region Helper Methods
    #endregion Helper Methods
  }
}


// Look into 
// http://netcave.org/GettingStartedWithTDDInVisualStudio.aspx 
// http://stephenwalther.com/blog/archive/2008/03/20/tdd-test-driven-development-with-visual-studio-2008-unit-tests.aspx
// http://codeclimber.net.nz/archive/2008/01/18/How-to-simulate-RowTest-with-MS-Test.aspx
// for some TDD techniques