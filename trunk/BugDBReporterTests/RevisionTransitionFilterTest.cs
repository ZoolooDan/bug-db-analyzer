using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using BugDB.Aggregator;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.BLToolkitProvider;
using BugDB.DataAccessLayer.DataTransferObjects;
using BugDB.Reporter.RevisionTransition;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BugDBReporterTests
{
  /// <summary>
  /// Test for RevisionTransitionFilter class.
  /// </summary>
  [TestClass]
  [DeploymentItem(@"DatabaseScripts\BugDB3.sql", "DatabaseScripts")]
  [DeploymentItem(@"Config\ReporterConfig.xml", "Config")]
  [DeploymentItem(@"ReferenceData\approachOne.txt", "ReferenceData")]
  public class RevisionTransitionFilterTest
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
                                            @"ReferenceData\approachOne.txt"));
      }
    }
    #endregion Test Setup
    
    /// <summary>
    /// Test for allowed transitions.
    ///</summary>
    [TestMethod]
//    [DeploymentItem("BugDB.Reporter.dll")]
    public void AllowedTransitionsTest()
    {
      RevisionTransitionFilter filter = new RevisionTransitionFilter();
      Bug[] bugs = m_provider.GetBugs();
      Revision[] revisions = m_provider.GetRevisions(bugs[0].Number);

      var transitions = new List<RevisionStatusTransition>(filter.GetTransitions(revisions));
      var transitionsExp = new List<RevisionStatusTransition>
                           {
                             new RevisionStatusTransition
                             {
                               Name = "Added",
                               PreviousGroup = new RevisionStatusGroup { Name = "DontExist" },
                               PreviousRevision = null,
                               CurrentGroup = new RevisionStatusGroup { Name = "ForAnalysis" },
                               CurrentRevision = new Revision { BugNumber = 1, Rev = 0 }
                             },
                             new RevisionStatusTransition
                             {
                               Name = "Removed",
                               PreviousGroup = new RevisionStatusGroup { Name = "ForWork" },
                               PreviousRevision = new Revision { BugNumber = 1, Rev = 3 },
                               CurrentGroup = new RevisionStatusGroup { Name = "ForTesting" },
                               CurrentRevision = new Revision { BugNumber = 1, Rev = 4 }
                             }
                           };
      CollectionAssert.AreEqual(transitionsExp, transitions, new RevisionStatusTransitionComparer());
    }

    /// <summary>
    /// Test for forbidden transitions.
    ///</summary>
    [TestMethod]
    public void ForbiddenTransitionsTest()
    {
      RevisionTransitionFilter filter = new RevisionTransitionFilter();
      Bug[] bugs = m_provider.GetBugs();
      Revision[] revisions = m_provider.GetRevisions(bugs[2].Number);

      var transitions = new List<RevisionStatusTransition>(filter.GetTransitions(revisions));
      var transitionsExp = new List<RevisionStatusTransition>
                           {
                             new RevisionStatusTransition
                             {
                               Name = "Added",
                               PreviousGroup = new RevisionStatusGroup { Name = "DontExist" },
                               PreviousRevision = null,
                               CurrentGroup = new RevisionStatusGroup { Name = "ForAnalysis" },
                               CurrentRevision = new Revision { BugNumber = 3, Rev = 0 },
                               IsForbidden = false
                             },
                             new RevisionStatusTransition
                             {
                               Name = "Removed",
                               PreviousGroup = new RevisionStatusGroup { Name = "ForAnalysis" },
                               PreviousRevision = new Revision { BugNumber = 3, Rev = 0 },
                               CurrentGroup = new RevisionStatusGroup { Name = "Finished" },
                               CurrentRevision = new Revision { BugNumber = 3, Rev = 1 },
                               IsForbidden = true
                             },
                             new RevisionStatusTransition
                             {
                               Name = "Added",
                               PreviousGroup = new RevisionStatusGroup { Name = "Finished" },
                               PreviousRevision = new Revision { BugNumber = 3, Rev = 1 },
                               CurrentGroup = new RevisionStatusGroup { Name = "ForWork" },
                               CurrentRevision = new Revision { BugNumber = 3, Rev = 2 },
                               IsForbidden = true
                             }
                           };
      CollectionAssert.AreEqual(transitionsExp, transitions, new RevisionStatusTransitionComparer());
    }


    /// <summary>
    /// Compares RevisionStatusTransition only for equality.
    /// </summary>
    /// <remarks>
    /// Returns 0 if equal and -1 if not equal. 
    /// So don't use for sorting.
    /// </remarks>
    public class RevisionStatusTransitionComparer : IComparer
    {
      #region Implementation of IComparer
      public int Compare(object x, object y)
      {
        var lhs = (RevisionStatusTransition)x;
        var rhs = (RevisionStatusTransition)y;

        if( (lhs == null && rhs == null) ||
          Object.ReferenceEquals(lhs, rhs) )
        {
          return 0;
        }

        if( lhs == null || rhs == null )
        {
          return -1;
        }
        
        if( lhs.Name != rhs.Name )
        {
          return -1;
        }

        if( lhs.PreviousGroup == null || rhs.PreviousGroup == null ||
          lhs.PreviousGroup.Name != lhs.PreviousGroup.Name )
        {
          return -1;
        }

        if( (lhs.PreviousRevision == null && rhs.PreviousRevision != null) ||
          (lhs.PreviousRevision != null && rhs.PreviousRevision == null ) )
        {
          return -1;
        }
        if( lhs.PreviousRevision != null && rhs.PreviousRevision != null )
        {
          if( lhs.PreviousRevision.BugNumber != rhs.PreviousRevision.BugNumber || 
            lhs.PreviousRevision.Rev != rhs.PreviousRevision.Rev )
          {
            return -1;
          }
        }

        if( lhs.CurrentRevision == null || rhs.CurrentRevision == null )
        {
          return -1;
        }

        if( lhs.CurrentRevision.BugNumber != rhs.CurrentRevision.BugNumber || 
          lhs.CurrentRevision.Rev != rhs.CurrentRevision.Rev ||
          lhs.IsForbidden != rhs.IsForbidden )
        {
          return -1;
        }

        return 0;
      }
      #endregion
    }
  }
}