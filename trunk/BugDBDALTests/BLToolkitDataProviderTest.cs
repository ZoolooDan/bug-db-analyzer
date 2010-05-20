using System;
using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    [TestMethod]
    public void CreateApplicatonTest()
    {
      const string appTitle = "TestApp";

      var app = new Application {Title = appTitle};
      Application actual = Provider.CreateApplicaton(app);
      Assert.IsNotNull(actual);
      Assert.AreNotEqual(0, actual.Id);
      Assert.AreEqual(appTitle, actual.Title);
    }

    /// <summary>
    /// Tests GetApplication(id).
    /// </summary>
    [TestMethod]
    public void GetApplicationTest()
    {
      const string appTitle = "TestApp";

      // Create application
      var app = new Application {Title = appTitle};
      app = Provider.CreateApplicaton(app);

      Application actual = Provider.GetApplication(app.Id);
      Assert.IsNotNull(actual);
      Assert.AreEqual(app.Id, actual.Id);
      Assert.AreEqual(appTitle, actual.Title);
    }

    /// <summary>
    /// A test for GetApplications()
    /// </summary>
    [TestMethod]
    public void GetApplicationsTest()
    {
      const string appTitle1 = "TestApp1";
      const string appTitle2 = "TestApp2";

      // Create application
      var app1 = new Application {Title = appTitle1};
      app1 = Provider.CreateApplicaton(app1);
      var app2 = new Application {Title = appTitle2};
      app2 = Provider.CreateApplicaton(app2);

      Application[] actual = Provider.GetApplications();
      Assert.IsNotNull(actual);
      Assert.AreEqual(2, actual.Length);
      Assert.IsNotNull(actual[0]);
      Assert.IsNotNull(actual[1]);
      Assert.AreEqual(appTitle1, actual[0].Title);
      Assert.AreEqual(app1.Id, actual[0].Id);
      Assert.AreEqual(appTitle2, actual[1].Title);
      Assert.AreEqual(app2.Id, actual[1].Id);
    }

    ///////////////////////////////////////////////
    /// <summary>
    /// Test CreateRelease().
    /// </summary>
    [TestMethod]
    public void CreateReleaseTest()
    {
      var app = new Application {Title = "App1"};
      app = Provider.CreateApplicaton(app);

      var release = new Release {AppId = app.Id, Title = "Release1"};
      Release actual = Provider.CreateRelease(release);

      Assert.IsNotNull(actual);
      Assert.AreNotEqual(0, actual.Id);
      Assert.AreEqual(release.Title, actual.Title);
    }

    /// <summary>
    /// Test GetReleases().
    /// </summary>
    [TestMethod]
    public void GetReleasesTest()
    {
      var app = new Application {Title = "App1"};
      app = Provider.CreateApplicaton(app);

      Release release1 = Provider.CreateRelease(new Release {AppId = app.Id, Title = "Release1"});
      Release release2 = Provider.CreateRelease(new Release {AppId = app.Id, Title = "Release2"});

      Release[] actual = Provider.GetReleases(app.Id);

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
      var app = new Application {Title = "App1"};
      app = Provider.CreateApplicaton(app);

      var module = new Module {AppId = app.Id, Title = "Module1"};
      Module actual = Provider.CreateModule(module);

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
      var app1 = new Application {Title = "App1"};
      app1 = Provider.CreateApplicaton(app1);

      Provider.CreateModule(new Module {AppId = app1.Id, Title = "Module1"});
      Provider.CreateModule(new Module {AppId = app1.Id, Title = "Module2"});

      var app2 = new Application {Title = "App2"};
      app2 = Provider.CreateApplicaton(app2);
      Module module3 = Provider.CreateModule(new Module {AppId = app2.Id, Title = "Module1"});
      Module module4 = Provider.CreateModule(new Module {AppId = app2.Id, Title = "Module4"});

      Module[] actual = Provider.GetModules(app2.Id);

      Assert.IsNotNull(actual);
      Assert.AreEqual(2, actual.Length);
      Assert.AreNotEqual(0, actual[0].Id);
      Assert.AreEqual(app2.Id, actual[0].AppId);
      Assert.AreEqual(module3.Title, actual[0].Title);
      Assert.AreNotEqual(0, actual[1].Id);
      Assert.AreEqual(app2.Id, actual[1].AppId);
      Assert.AreEqual(module4.Title, actual[1].Title);
    }

    ///////////////////////////////////////////////
    /// <summary>
    /// Test CreateSubModule().
    /// </summary>
    [TestMethod]
    public void CreateSubModuleTest()
    {
      const string title = "SubModule1";

      var app = new Application { Title = "App1" };
      app = Provider.CreateApplicaton(app);

      var module = new Module { AppId = app.Id, Title = "Module1" };
      module = Provider.CreateModule(module);

      var subModule = new SubModule { ModuleId = module.Id, Title = title };
      var actual = Provider.CreateSubModule(subModule);

      Assert.IsNotNull(actual);
      Assert.AreNotEqual(0, actual.Id);
      Assert.AreEqual(module.Id, actual.ModuleId);
      Assert.AreEqual(title, actual.Title);
    }

    /// <summary>
    /// Test GetSubModules().
    /// </summary>
    [TestMethod]
    public void GetSubModulesTest()
    {
      const string title1 = "SubModule1";
      const string title2 = "SubModule2";
      const string title3 = "SubModule3";

      var app = new Application { Title = "App1" };
      app = Provider.CreateApplicaton(app);

      var module1 = new Module { AppId = app.Id, Title = "Module1" };
      module1 = Provider.CreateModule(module1);
      var module2 = new Module { AppId = app.Id, Title = "Module2" };
      module2 = Provider.CreateModule(module2);

      Provider.CreateSubModule(
        new SubModule { ModuleId = module1.Id, Title = title1 });
      Provider.CreateSubModule(
        new SubModule { ModuleId = module1.Id, Title = title2 });
      Provider.CreateSubModule(
        new SubModule { ModuleId = module2.Id, Title = title2 });
      Provider.CreateSubModule(
        new SubModule { ModuleId = module2.Id, Title = title3 });
      
      var actual = Provider.GetSubModules(module2.Id);

      Assert.IsNotNull(actual);
      Assert.AreEqual(2, actual.Length);
      Assert.AreNotEqual(0, actual[0].Id);
      Assert.AreEqual(module2.Id, actual[0].ModuleId);
      Assert.AreEqual(title2, actual[0].Title);
      Assert.AreNotEqual(0, actual[1].Id);
      Assert.AreEqual(module2.Id, actual[1].ModuleId);
      Assert.AreEqual(title3, actual[1].Title);
    }

    ///////////////////////////////////////////////
    /// <summary>
    /// Test GetSubModules().
    /// </summary>
    [TestMethod]
    public void CreatePersonTest()
    {
      var person = new Person {Login = "user1", Title = "User Clever"};
      person = Provider.CreatePerson(person);
      Assert.IsNotNull(person);
      Assert.AreNotEqual(0, person.Id);
    }

    /// <summary>
    /// Test GetStaff().
    /// </summary>
    [TestMethod]
    public void GetStaffTest()
    {
      var person = new Person {Login = "user1", Title = "User Clever"};
      person = Provider.CreatePerson(person);

      Person[] actual = Provider.GetStaff();
      Assert.IsNotNull(actual);
      Assert.AreEqual(1, actual.Length);
      Assert.IsNotNull(actual[0]);
      Assert.AreEqual(person.Id, actual[0].Id);
      Assert.AreEqual(person.Login, actual[0].Login);
      Assert.AreEqual(person.Title, actual[0].Title);
    }

    ///////////////////////////////////////////////

    /// <summary>
    /// Tests CreateRevision().
    /// </summary>
    [TestMethod]
    public void CreateRevisionTest()
    {
      // Add some records to all collections
      var app = new Application { Title = "App1" };
      app = Provider.CreateApplicaton(app);

      var contributor = new Person { Login = "user1", Title = "Contributor" };
      contributor = Provider.CreatePerson(contributor);

      var leader = new Person { Login = "user2", Title = "Leader" };
      leader = Provider.CreatePerson(leader);

      var dev = new Person { Login = "user3", Title = "Developer" };
      dev = Provider.CreatePerson(dev);

      var tester = new Person { Login = "user4", Title = "QM" };
      tester = Provider.CreatePerson(tester);

      var module = new Module { AppId = app.Id, Title = "Module1" };
      module = Provider.CreateModule(module);

      var subModule = new SubModule { ModuleId = module.Id, Title = "SubModule1" };
      subModule = Provider.CreateSubModule(subModule);

      var release = new Release { AppId = app.Id, Title = "Release1" };
      release = Provider.CreateRelease(release);


      var revision1 = new Revision
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
        ContributorId = contributor.Id,
        ModuleId = module.Id,
        SubModuleId = subModule.Id,
        Summary = "Summary text"
      };

      var actual1 = Provider.CreateRevision(revision1);

      Assert.IsNotNull(actual1);
      Assert.AreEqual(app.Id, actual1.AppId);
      Assert.AreEqual(10, actual1.BugNumber);
      Assert.AreEqual(1, actual1.Rev);
      Assert.AreEqual(revision1.Date, actual1.Date);
      Assert.AreEqual(BugType.Bug, actual1.Type);
      Assert.AreEqual(release.Id, actual1.FoundReleaseId);
      Assert.AreEqual(release.Id, actual1.TargetReleaseId);
      Assert.AreEqual(1, actual1.Priority);
      Assert.AreEqual(BugSeverity.Critical, actual1.Severity);
      Assert.AreEqual(BugStatus.Open, actual1.Status);
      Assert.AreEqual(contributor.Id, actual1.ContributorId);
      Assert.IsFalse(actual1.TeamLeaderId.HasValue);
      Assert.IsFalse(actual1.DeveloperId.HasValue);
      Assert.IsFalse(actual1.TesterId.HasValue);
      Assert.AreEqual(module.Id, actual1.ModuleId);
      Assert.AreEqual(subModule.Id, actual1.SubModuleId);
      Assert.AreEqual(revision1.Summary, actual1.Summary);

      var revision2 = new Revision
      {
        AppId = app.Id,
        BugNumber = 10,
        Rev = 2,
        Date = DateTime.Now,
        Type = BugType.Bug,
        FoundReleaseId = release.Id,
        TargetReleaseId = release.Id,
        Severity = BugSeverity.Critical,
        Priority = 1,
        Status = BugStatus.Verified,
        ContributorId = contributor.Id,
        TeamLeaderId = leader.Id,
        DeveloperId = dev.Id,
        TesterId = tester.Id,
        ModuleId = module.Id,
        SubModuleId = subModule.Id,
        Summary = "Summary text 2"
      };

      var actual2 = Provider.CreateRevision(revision2);

      Assert.IsNotNull(actual2);
      Assert.AreEqual(app.Id, actual2.AppId);
      Assert.AreEqual(10, actual2.BugNumber);
      Assert.AreEqual(2, actual2.Rev);
      Assert.AreEqual(revision2.Date, actual2.Date);
      Assert.AreEqual(BugType.Bug, actual2.Type);
      Assert.AreEqual(release.Id, actual2.FoundReleaseId);
      Assert.AreEqual(release.Id, actual2.TargetReleaseId);
      Assert.AreEqual(1, actual2.Priority);
      Assert.AreEqual(BugSeverity.Critical, actual2.Severity);
      Assert.AreEqual(BugStatus.Verified, actual2.Status);
      Assert.AreEqual(contributor.Id, actual2.ContributorId);
      Assert.AreEqual(leader.Id, actual2.TeamLeaderId);
      Assert.AreEqual(dev.Id, actual2.DeveloperId);
      Assert.AreEqual(tester.Id, actual2.TesterId);
      Assert.AreEqual(module.Id, actual2.ModuleId);
      Assert.AreEqual(subModule.Id, actual2.SubModuleId);
      Assert.AreEqual(revision2.Summary, actual2.Summary);
    }

    /// <summary>
    /// Tests GetRevisions().
    /// </summary>
    [TestMethod]
    public void GetRevisionsTest()
    {
      // Add some records to all collections
      var app = new Application { Title = "App1" };
      app = Provider.CreateApplicaton(app);

      var contributor = new Person { Login = "user1", Title = "Contributor" };
      contributor = Provider.CreatePerson(contributor);

      var leader = new Person { Login = "user2", Title = "Leader" };
      leader = Provider.CreatePerson(leader);

      var dev = new Person { Login = "user3", Title = "Developer" };
      dev = Provider.CreatePerson(dev);

      var tester = new Person { Login = "user4", Title = "QM" };
      tester = Provider.CreatePerson(tester);

      var module = new Module { AppId = app.Id, Title = "Module1" };
      module = Provider.CreateModule(module);

      var subModule = new SubModule { ModuleId = module.Id, Title = "SubModule1" };
      subModule = Provider.CreateSubModule(subModule);

      var release = new Release { AppId = app.Id, Title = "Release1" };
      release = Provider.CreateRelease(release);


      var revision1 = Provider.CreateRevision(
        new Revision
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
            ContributorId = contributor.Id,
            ModuleId = module.Id,
            SubModuleId = subModule.Id,
            Summary = "Summary text"
          });

      var revision2 = Provider.CreateRevision(
        new Revision
          {
            AppId = app.Id,
            BugNumber = 10,
            Rev = 2,
            Date = DateTime.Now.Date,
            Type = BugType.Bug,
            FoundReleaseId = release.Id,
            TargetReleaseId = release.Id,
            Severity = BugSeverity.Critical,
            Priority = 1,
            Status = BugStatus.Verified,
            ContributorId = contributor.Id,
            TeamLeaderId = leader.Id,
            DeveloperId = dev.Id,
            TesterId = tester.Id,
            ModuleId = module.Id,
            SubModuleId = subModule.Id,
            Summary = "Summary text 2"
          });

      Provider.CreateRevision(
        new Revision
          {
            AppId = app.Id,
            BugNumber = 12,
            Rev = 1,
            Date = DateTime.Now.Date,
            Type = BugType.Bug,
            Status = BugStatus.Open,
            ContributorId = contributor.Id,
            Summary = "Summary text 2"
          });

      var revisions = Provider.GetRevisions(10);

      Assert.IsNotNull(revisions);
      Assert.AreEqual(2, revisions.Length);
      Assert.AreEqual(revision1, revisions[0]);
      Assert.AreEqual(revision2, revisions[1]);
    }

    /// <summary>
    /// Tests GetBugs().
    /// </summary>
    [TestMethod]
    public void GetBugsTest()
    {
      CreateRevisionTest();

      Bug[] bugs = Provider.GetBugs();
      Assert.IsNotNull(bugs);
      Assert.AreEqual(1, bugs.Length);
      Assert.AreEqual(10, bugs[0].Number);
    }

    ///////////////////////////////////////////////
    /// <summary>
    /// Test for CleanStorage.
    /// </summary>
    [TestMethod]
    public void CleanStorageTest()
    {
      // Add some records to all collections
      var app = new Application {Title = "App1"};
      app = Provider.CreateApplicaton(app);

      var person = new Person {Login = "user1", Title = "User Clever"};
      person = Provider.CreatePerson(person);

      var module = new Module {AppId = app.Id, Title = "Module1"};
      module = Provider.CreateModule(module);

      var subModule = new SubModule {ModuleId = module.Id, Title = "SubModule1"};
      subModule = Provider.CreateSubModule(subModule);

      var release = new Release {AppId = app.Id, Title = "Release1"};
      release = Provider.CreateRelease(release);

      var revision = new Revision
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

      Provider.CreateRevision(revision);

      // Check that everything is added to storage
      Assert.AreNotEqual(0, Provider.GetApplications().Length);
      Assert.AreNotEqual(0, Provider.GetModules(app.Id).Length);
      Assert.AreNotEqual(0, Provider.GetSubModules(module.Id).Length);
      Assert.AreNotEqual(0, Provider.GetStaff().Length);
      //      Assert.AreNotEqual(0, Provider.GetBugs().Length);
      Assert.AreNotEqual(0, Provider.GetRevisions(10).Length);
      //      Assert.AreNotEqual(0, Provider.GetReleases().Length);

      // Clean storage
      Provider.CleanStorage();
      // And check that it is cleared
      Assert.AreEqual(0, Provider.GetApplications().Length);
      Assert.AreEqual(0, Provider.GetModules(app.Id).Length);
      Assert.AreEqual(0, Provider.GetSubModules(module.Id).Length);
      Assert.AreEqual(0, Provider.GetStaff().Length);
      //      Assert.AreEqual(0, Provider.GetBugs().Length);
      Assert.AreEqual(0, Provider.GetRevisions(10).Length);
      //      Assert.AreEqual(0, Provider.GetReleases().Length);
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