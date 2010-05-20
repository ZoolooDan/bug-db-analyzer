using System;
using System.Configuration;
using System.IO;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.BLToolkitProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BugDB.Tests.Utils
{
  /// <summary>
  /// Storage initialization method.
  /// </summary>
  internal enum StorageInitMethod
  {
    /// <summary>
    /// Recreate storage from scratch.
    /// </summary>
    Initialize,
    /// <summary>
    /// Just clean collections.
    /// </summary>
    Clean
  }

  /// <summary>
  /// Base class for tests which require initialization of
  /// database storage.
  /// </summary>
  public class DatabaseStorageTestBase
  {
    #region Private Fields

    private const string DbCreateScript = @"DatabaseScripts\BugDB3.sql";

    /// <summary>
    /// Set to true to initialize provider after creation.
    /// </summary>
    private bool m_initProvOnCreate;

    /// <summary>
    /// Set to true to clean storage before each test run.
    /// </summary>
    private bool m_cleanStorageEachTest = true;

    /// <summary>
    /// BugDB data storage provider.
    /// </summary>
    private IDataProvider m_provider;

    #endregion Private Fields

    #region Constructors
    public DatabaseStorageTestBase()
    {
      // Read initialization method from configuration
      string methodValue = ConfigurationManager.AppSettings["StorageInitMethod"] ?? "Initialize";
      var method = (StorageInitMethod)Enum.Parse(
        typeof(StorageInitMethod), methodValue);

      m_initProvOnCreate = method == StorageInitMethod.Initialize;
    }

    public DatabaseStorageTestBase(bool cleanStorageEachTest) : this()
    {
      m_cleanStorageEachTest = cleanStorageEachTest;
    }

    public DatabaseStorageTestBase(bool cleanStorageEachTest, bool initProvOnCreate)
    {
      m_initProvOnCreate = initProvOnCreate;
      m_cleanStorageEachTest = cleanStorageEachTest;
    }
    #endregion Constructors

    #region Public Properties

    /// <summary>
    /// Gets or sets the test context which provides
    /// information about and functionality for the current test run.
    /// </summary>
    public TestContext TestContext { get; set; }

    /// <summary>
    /// Gets data provider.
    /// </summary>
    /// <remarks>
    /// Provider is created when first time accessed.
    /// Storage initialized if specified.
    /// </remarks>
    public IDataProvider Provider
    {
      get
      {
        // Create if not created yet
        if( m_provider == null )
        {
          // Path to create script
          string dbCreateScriptPath = Path.Combine(
            TestContext.TestDeploymentDir, DbCreateScript);
          // Create provider
          m_provider = new BLToolkitDataProvider(dbCreateScriptPath);

          // Initialize storage if specified
          if( m_initProvOnCreate )
          {
            m_provider.InitializeStorage();
          }
        }
        return m_provider;
      }
      set { throw new NotImplementedException(); }
    }

    #endregion Public Properties

    /// <summary>
    /// Overridable for derived tests to fill storage with data.
    /// </summary>
    public virtual void FillStorage()
    {
    }

    /// <summary>
    /// Perform test initialization steps.
    /// </summary>
    [TestInitialize]
    public void TestInitialize()
    {
      bool firstCall = m_provider == null;

      // Clean and fill storage each test run
      // or just during first run
      if( m_cleanStorageEachTest || firstCall )
      {
        Provider.CleanStorage();
        FillStorage();
      }
    }
  }
}