using System;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using System.Collections.Generic;


namespace BugDB.QueryParser.Tests
{
  /// <summary>
  /// Test QueryResultParser.
  /// </summary>
  [TestClass]
  public class QueryResultParserTest
  {
    #region Private Fields
    private const string RawSourceFileName = "rawSource.txt";
    private const string RawRefFileName = "rawRef.txt";
    private const string RecordSourceFileName = "recordsSource.txt";
    private const string RecordSourceFileName2 = "recordsSource2.txt";
    private const string CsvSourceFileName = "csvSource.txt";
    private const string CsvRefFileName = "csvRef.txt";
    #endregion

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

    /// <summary>
    ///A test for CreateRawStringsEnumerator
    ///</summary>
    [TestMethod]
    public void CreateRawStringsEnumeratorTest()
    {
      string inputFileName = Path.Combine(this.TestContext.TestDeploymentDir,
                                          RawSourceFileName);
      string refFileName = Path.Combine(this.TestContext.TestDeploymentDir,
                                        RawRefFileName);

      using(TextReader reader = new StreamReader(inputFileName))
      {
        using(IEnumerator<string> actual = QueryResultParser.
          CreateRawStringsEnumerator(reader))
        {
          using(IEnumerator<string> expected = CreateFileEnumerator(refFileName))
          {
            CheckEnumeratorsAreEqual(expected, actual);
          }
        }
      }
    }

    /// <summary>
    ///A test for CreateCsvStringsEnumerator
    ///</summary>
    [TestMethod]
    public void CreateCsvStringsEnumeratorTest()
    {
      //Stream stream = null; // TODO: Initialize to an appropriate value
      //IEnumerator<string> expected = null; // TODO: Initialize to an appropriate value
      //IEnumerator<string> actual;
      //actual = QueryResultParser.CreateCsvStringsEnumerator(stream);
      //Assert.AreEqual(expected, actual);
      Assert.Inconclusive("Verify the correctness of this test method.");
    }

    /// <summary>
    /// A test for CreateRecordsEnumerator
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"ReferenceData\recordsSource.txt")]
    public void CreateRecordsEnumeratorTest()
    {
      string inputFileName = Path.Combine(this.TestContext.TestDeploymentDir,
                                          RecordSourceFileName);

      using(TextReader reader = new StreamReader(inputFileName))
      {
        var actual = QueryResultParser.CreateRecordsEnumerator(reader);
        // Reference data
        IDictionary<string, string> record1 = new Dictionary<string, string>()
                                              {
                                                {"number", "15210"},
                                                {"subnumber", "0"},
                                                {"deadline", "bug"},
                                                {"status", "open"},
                                                {"date", "2004/02/23"},
                                                {"closedate", null},
                                                {"application", "Installer"},
                                                {"modul", null},
                                                {"submodul", null},
                                                {"apprelease", "unknown"},
                                                {"frelease", null},
                                                {"severity", null},
                                                {"priority", "n/a"},
                                                {"contributor", "viktorov"},
                                                {"leader", null},
                                                {"developer", null},
                                                {"qa", null},
                                                {
                                                  "summary",
                                                  "VPIplayer: the error message comes up during deinstallation: \"-1612...\""
                                                  }
                                              };
        IDictionary<string, string> record2 = new Dictionary<string, string>()
                                              {
                                                {"number", "12807"},
                                                {"subnumber", "4"},
                                                {"deadline", "bug"},
                                                {"status", "closed"},
                                                {"date", "2004/04/05"},
                                                {"closedate", "2004/04/05"},
                                                {"application", "VPIplayer"},
                                                {"modul", null},
                                                {"submodul", null},
                                                {"apprelease", "1.0"},
                                                {"frelease", "1.0"},
                                                {"severity", "2"},
                                                {"priority", "1"},
                                                {"contributor", "a.lowery"},
                                                {"leader", "k.soh"},
                                                {"developer", null},
                                                {"qa", "sohryn"},
                                                {"summary", "Integer Controls in Player don't work at top setting"}
                                              };

        var records = new List<IDictionary<string, string>>() {record1, record2};


        // Compare
        CheckEnumeratorsAreEqual2(records.GetEnumerator(), actual);
      }
    }

    /// <summary>
    /// A test for CreateRecordsEnumerator
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"ReferenceData\recordsSource2.txt")]
    public void CreateRecordsEnumeratorTest2()
    {
      string inputFileName = Path.Combine(this.TestContext.TestDeploymentDir,
                                          RecordSourceFileName2);

      using(TextReader reader = new StreamReader(inputFileName))
      {
        int count = 0;
        var actual = QueryResultParser.CreateRecordsEnumerator(reader);
        while( actual.MoveNext() )
        {
          count++;
        }
        Assert.AreEqual(18, count);
      }
    }

    #region Helper Functions
    /// <summary>
    /// Checks that enumerators enumerate same values.
    /// </summary>
    private static void CheckEnumeratorsAreEqual<T>(IEnumerator<T> expected, IEnumerator<T> actual)
    {
      bool cont1, cont2;
      do
      {
        cont1 = expected.MoveNext();
        cont2 = actual.MoveNext();

        // Check length
        if( cont1 != cont2 )
        {
          Assert.Fail("Enumerators are not of the same length.");
        }
        else if( cont1 )
        {
          // Check values
          Assert.AreEqual(expected.Current, actual.Current);
        }
      }
      while( cont1 );
    }

    /// <summary>
    /// Checks that enumerators enumerate same values.
    /// </summary>
    private static void CheckEnumeratorsAreEqual2(
      IEnumerator<IDictionary<string, string>> expected,
      IEnumerator<IDictionary<string, string>> actual)
    {
      bool cont1, cont2;
      do
      {
        cont1 = expected.MoveNext();
        cont2 = actual.MoveNext();

        // Check length
        if( cont1 != cont2 )
        {
          Assert.Fail("Enumerators are not of the same length.");
        }
        else if( cont1 )
        {
          // Check values
          CheckDictionariesAreEqual(expected.Current, actual.Current);
        }
      }
      while( cont1 );
    }

    /// <summary>
    /// Checks that dictionaries are equal.
    /// </summary>
    private static void CheckDictionariesAreEqual(IDictionary<string, string> expected,
                                                  IDictionary<string, string> actual)
    {
      if( expected.Count != actual.Count )
      {
        Assert.Fail("Dictionaries has different size");
      }
      foreach(string key in expected.Keys)
      {
        if( actual.ContainsKey(key) )
        {
          Assert.AreEqual(expected[key], actual[key]);
        }
        else
        {
          Assert.Fail(String.Format(
                        "Key '{0}' doesn't exist in target dictionary", key));
        }
      }
    }

    /// <summary>
    /// Returns enumerator which can enumerate lines in file.
    /// </summary>
    private static IEnumerator<string> CreateFileEnumerator(string path)
    {
      return new FileLinesEnumerator(new FileStream(path, FileMode.Open, FileAccess.Read));
    }
    #endregion Helper Functions
  }


  internal class FileLinesEnumerator : IEnumerator<string>
  {
    #region Private Fields
    private readonly StreamReader m_reader;
    private string m_currentString;
    #endregion Private Fields

    #region Constructors
    public FileLinesEnumerator(Stream stream)
    {
      m_reader = new StreamReader(stream);
    }
    #endregion Constructors

    #region Implementation of IDisposable
    public void Dispose()
    {
      m_reader.Dispose();
    }
    #endregion

    #region Implementation of IEnumerator
    public bool MoveNext()
    {
      m_currentString = m_reader.ReadLine();
      return m_currentString != null;
    }

    public void Reset()
    {
      throw new NotSupportedException();
    }

    public string Current
    {
      get { return m_currentString; }
    }

    object IEnumerator.Current
    {
      get { return Current; }
    }
    #endregion
  }
}