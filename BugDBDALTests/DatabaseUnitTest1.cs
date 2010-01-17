using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TeamSystem.Data.UnitTesting;
using Microsoft.VisualStudio.TeamSystem.Data.UnitTesting.Conditions;

namespace BugDB.DAL.Tests
{
  [TestClass()]
  public class DatabaseUnitTest1 : DatabaseTestClass
  {

    public DatabaseUnitTest1()
    {
      InitializeComponent();
    }

    [TestInitialize()]
    public void TestInitialize()
    {
      base.InitializeTest();
    }
    [TestCleanup()]
    public void TestCleanup()
    {
      base.CleanupTest();
    }

    [TestMethod()]
    public void DatabaseTest1()
    {
      DatabaseTestActions testActions = this.DatabaseTest1Data;
      // Execute the pre-test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
      ExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
      // Execute the test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
      ExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
      // Execute the post-test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
      ExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
    }

    #region Designer support code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      Microsoft.VisualStudio.TeamSystem.Data.UnitTesting.DatabaseTestAction DatabaseTest1_TestAction;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseUnitTest1));
      Microsoft.VisualStudio.TeamSystem.Data.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition1;
      this.DatabaseTest1Data = new Microsoft.VisualStudio.TeamSystem.Data.UnitTesting.DatabaseTestActions();
      DatabaseTest1_TestAction = new Microsoft.VisualStudio.TeamSystem.Data.UnitTesting.DatabaseTestAction();
      inconclusiveCondition1 = new Microsoft.VisualStudio.TeamSystem.Data.UnitTesting.Conditions.InconclusiveCondition();
      // 
      // DatabaseTest1Data
      // 
      this.DatabaseTest1Data.PosttestAction = null;
      this.DatabaseTest1Data.PretestAction = null;
      this.DatabaseTest1Data.TestAction = DatabaseTest1_TestAction;
      // 
      // DatabaseTest1_TestAction
      // 
      DatabaseTest1_TestAction.Conditions.Add(inconclusiveCondition1);
      resources.ApplyResources(DatabaseTest1_TestAction, "DatabaseTest1_TestAction");
      // 
      // inconclusiveCondition1
      // 
      inconclusiveCondition1.Enabled = true;
      inconclusiveCondition1.Name = "inconclusiveCondition1";
    }

    #endregion


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
    #endregion

    private DatabaseTestActions DatabaseTest1Data;
  }
}
