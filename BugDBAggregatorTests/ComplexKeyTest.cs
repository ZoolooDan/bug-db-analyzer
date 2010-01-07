using BugDB.Aggregator;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BugDBAggregatorTests
{
  /// <summary>
  ///This is a test class for ComplexKeyTest and is intended
  ///to contain all ComplexKeyTest Unit Tests
  ///</summary>
  [TestClass()]
  public class ComplexKeyTest
  {
    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
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

    /// <summary>
    /// Tests Object Equals().
    /// </summary>
    [TestMethod]
    public void ObjectEqualsTest()
    {
      ComplexKey ck1 = new ComplexKey("aaa", 123, 5.4);
      ComplexKey ck2 = new ComplexKey("aaa", 123, 5.4);
      ComplexKey ck3 = new ComplexKey("bbb", 123, 5.4);
      ComplexKey ck4 = new ComplexKey("aaa", 234, 5.4);

      Assert.AreEqual(ck1, ck2);
      Assert.AreNotEqual(ck1, ck3);
      Assert.AreNotEqual(ck1, ck4);
    }

    /// <summary>
    /// Tests IEquatable Equals().
    /// </summary>
    [TestMethod]
    public void EquatableEqualsTest()
    {
      ComplexKey ck1 = new ComplexKey("aaa", 123, 5.4);
      ComplexKey ck2 = new ComplexKey("aaa", 123, 5.4);
      ComplexKey ck3 = new ComplexKey("bbb", 123, 5.4);
      ComplexKey ck4 = new ComplexKey("aaa", 234, 5.4);

      Assert.IsTrue(ck1.Equals(ck2));
      Assert.IsFalse(ck1.Equals(ck3));
      Assert.IsFalse(ck1.Equals(ck4));
    }

    /// <summary>
    ///A test for GetHashCode
    ///</summary>
    [TestMethod()]
    public void GetHashCodeTest()
    {
      ComplexKey ck1 = new ComplexKey("aaa", 123, 5.4);
      ComplexKey ck2 = new ComplexKey("aaa", 123, 5.4);
      ComplexKey ck3 = new ComplexKey("bbb", 123, 5.4);
      ComplexKey ck4 = new ComplexKey("aaa", 234, 5.4);

      Assert.AreEqual(ck1.GetHashCode(), ck2.GetHashCode());
      Assert.AreNotEqual(ck1.GetHashCode(), ck3.GetHashCode());
      Assert.AreNotEqual(ck1.GetHashCode(), ck4.GetHashCode());
    }
  }
}