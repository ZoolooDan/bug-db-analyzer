using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Data;

namespace BugDB.DAL
{
  /// <summary>
  /// Class for purging whole database.
  /// </summary>
  public class DBCleaner
  {
    public void CleanDB()
    {
      using (DbManager db = new DbManager())
      {
        int rowsAffected = db.SetCommand("DELETE FROM Applications").
          ExecuteNonQuery();
      }
    }
  }
}
