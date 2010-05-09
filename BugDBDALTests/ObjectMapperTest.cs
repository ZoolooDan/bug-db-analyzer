using BugDB.DataAccessLayer.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BugDB.DAL.Tests
{
  /// <summary>
  /// ObjectMapper tests
  /// </summary>
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

    internal enum EnA
    {
      A,
      B,
      C
    }

    internal class A
    {
      public const int PropAValue = 1;
      public const string PropBValue = "FromA";
      public const double PropDValue = 2.5;
      public const EnA PropCValue = EnA.B;
      public const int PropEValue = 1;

      private double m_d = PropDValue;

      public A()
      {
        PropA = PropAValue;
        PropB = PropBValue;
        PropC = PropCValue;
        PropE = PropEValue;
      }

      public int PropA { get; set; }
      private string PropB { get; set; }
      public EnA? PropC { get; set; }

      public string GetB() { return PropB;  }
      public double GetD() { return m_d;  }

      public int? PropE { get; set; }
    }

    internal enum EnB
    {
      A,
      B,
      C
    }

    internal class B
    {
      public const int PropAValue = 2;
      public const string PropBValue = "FromB";
      public const double PropDValue = 3.5;
      public const EnB PropCValue = EnB.C;
      public const int PropEValue = 2;

      private double m_d = PropDValue;

      public B()
      {
        PropA = PropAValue;
        PropB = PropBValue;
        PropC = PropCValue;
        PropE = PropEValue;
      }

      public int PropA { get; set; }
      private string PropB { get; set; }
      public EnB? PropC { get; set; }

      public string GetB() { return PropB; }
      public double GetD() { return m_d; }

      public int? PropE { get; set; }
    }

    /// <summary>
    /// Tests copying similar objects.
    /// </summary>
    [TestMethod]
    public void CopyTestSimilar()
    {
      var copier = new ObjectMapper<A, B>();

      A a1 = new A();
      B b1 = copier.Copy(a1);

      Assert.IsNotNull(b1);
      Assert.AreEqual(a1.PropA, b1.PropA);
      Assert.AreEqual(B.PropBValue, b1.GetB());
      Assert.AreEqual(B.PropDValue, b1.GetD());
      Assert.AreEqual((int)a1.PropC, (int)b1.PropC);

      B b2 = new B();
      A a2 = copier.Copy(b2);

      Assert.IsNotNull(a2);
      Assert.AreEqual(b2.PropA, a2.PropA);
      Assert.AreEqual(A.PropBValue, a2.GetB());
      Assert.AreEqual(A.PropDValue, a2.GetD());
      Assert.AreEqual((int)b2.PropC, (int)a2.PropC);

      A a3 = new A {PropC = null};
      B b3 = copier.Copy(a3);
      Assert.IsFalse(b3.PropC.HasValue);
    }
    ///////////////////////////////////////////////////////////

    class C
    {
      public const int PropAValue = 10;
      public const double PropBValue = 145;

      public C()
      {
        this.PropA = PropAValue;
        this.PropB = PropBValue;
      }

      public int PropA { get; set; }
      public double PropB { get; set; }
    }

    class D
    {
      public const double PropAValue = 3.5;
      public const int PropBValue = 56;

      public D()
      {
        this.PropA = PropAValue;
        this.PropB = PropBValue;
      }

      public double PropA { get; set; }
      public int PropB { get; set; }
    }

    /// <summary>
    /// Tests copying a little bit different objects.
    /// </summary>
    [TestMethod]
    public void CopyTestDifferent()
    {
      var copier = new ObjectMapper<C, D>();

      C c1 = new C();
      D d1 = copier.Copy(c1);

      Assert.IsNotNull(d1);
      Assert.AreEqual(c1.PropA, d1.PropA);
      Assert.AreEqual(c1.PropB, d1.PropB);
    }
  }
}
