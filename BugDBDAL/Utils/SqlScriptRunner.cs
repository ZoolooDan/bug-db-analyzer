using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace BugDB.DataAccessLayer.Utils
{
  /// <summary>
  /// Executes script files which contains GO statements.
  /// </summary>
  public class SqlScriptRunner
  {
    #region Private Fields
    private string m_connString;
    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Constructs runner for specific connection.
    /// </summary>
    public SqlScriptRunner(string connString)
    {
      m_connString = connString;
    }
    #endregion Constructors

    #region Public Methods
    /// <summary>
    /// Execute script from file.
    /// </summary>
    public void Execute(string path)
    {
      // Read file
      FileInfo fileInfo = new FileInfo(path);
      using (StreamReader reader = fileInfo.OpenText())
      {
        Execute(reader);
      }
    }

    /// <summary>
    /// Execute script from stream.
    /// </summary>
    public void Execute(TextReader reader)
    {
      // Read script
      string script = reader.ReadToEnd();
      // Execute
      ExecuteScriptText(script);
    }
    #endregion Public Methods

    #region Helper Methods
    /// <summary>
    /// Executes script text using SQL Server Management Objects (SMO).
    /// </summary>
    /// <remarks>
    /// Idea is from http://weblogs.asp.net/jgalloway/archive/2006/11/07/Handling-_2200_GO_2200_-Separators-in-SQL-Scripts-_2D00_-the-easy-way.aspx
    /// </remarks>
    private void ExecuteScriptText(string script)
    {
      SqlConnection connection = new SqlConnection(m_connString);
      Server server = new Server(new ServerConnection(connection));
      server.ConnectionContext.ExecuteNonQuery(script);
    }
    #endregion Helper Methods
  }
}

// Interesting article on SqlScriptRunner which supports template parameters
// http://haacked.com/archive/2007/11/04/a-library-for-executing-sql-scripts-with-go-separators-and.aspx
// 