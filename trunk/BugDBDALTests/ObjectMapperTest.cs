using BugDB.DataAccessLayer.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BugDB.DAL.Tests
{
  /// <summary>
  /// ObjectMapper tests
  ///</summary>
  [TestClass]
  public class ObjectMapperTest
  {
    #region Public Properties
    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
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
    ///A test for Map
    ///</summary>
    public void MapTestHelper<S, T>()

        where T : new()
    {
      S source = default(S); // TODO: Initialize to an appropriate value
      T expected = new T(); // TODO: Initialize to an appropriate value
      T actual;
      actual = ObjectMapper.Map<S, T>(source);
      Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }


    internal class A
    {
      public const int PropAValue = 1;
      public const string PropBValue = "FromA";
      public const double PropDValue = 2.5;

      private double m_d = PropDValue;

      public A()
      {
        PropA = PropAValue;
        PropB = PropBValue;
      }

      public int PropA { get; set; }
      private string PropB { get; set; }

      public string GetB() { return PropB;  }
      public double GetD() { return m_d;  }
    }

    internal class B
    {
      public const int PropAValue = 2;
      public const string PropBValue = "FromB";
      public const double PropDValue = 3.5;

      private double m_d = PropDValue;

      public B()
      {
        PropA = PropAValue;
        PropB = PropBValue;
      }

      public int PropA { get; set; }
      private string PropB { get; set; }

      public string GetB() { return PropB; }
      public double GetD() { return m_d; }
    }

    [TestMethod]
    public void MapTest()
    {
      MapTestHelper<GenericParameterHelper, GenericParameterHelper>();
      A a = new A() {PropA = 10};
      B b = ObjectMapper.Map<A, B>(a);

      Assert.AreEqual(a.PropA, b.PropA);
      Assert.AreEqual(B.PropBValue, b.GetB());
      Assert.AreEqual(B.PropDValue, b.GetD());
    }

    [TestMethod]
    public void CopyTest()
    {
      var copier = new ObjectCopier<A, B>();

      A a1 = new A();
      B b1 = copier.Copy(a1);

      Assert.IsNotNull(b1);
      Assert.AreEqual(a1.PropA, b1.PropA);
      Assert.AreEqual(B.PropBValue, b1.GetB());
      Assert.AreEqual(B.PropDValue, b1.GetD());

      B b2 = new B();
      A a2 = copier.Copy(b2);

      Assert.IsNotNull(a2);
      Assert.AreEqual(b2.PropA, a2.PropA);
      Assert.AreEqual(A.PropBValue, a2.GetB());
      Assert.AreEqual(A.PropDValue, a2.GetD());
    }
  }
}
