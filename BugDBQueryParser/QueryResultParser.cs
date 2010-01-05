using System;
using System.Collections.Generic;
using System.IO;

namespace BugDB.QueryParser
{
  /// <summary>
  /// Alias for long interface for convenience.
  /// </summary>
  public interface IRecord : IDictionary<string, string>
  {
  }

  /// <summary>
  /// BugDB database query results parser.
  /// </summary>
  /// <remarks>
  /// Query results contains multiple logically distinct
  /// blocks of text lines. Most of blocks except footer block
  /// has the same structure. First line of block starts with SPACE
  /// and subsequent lines - with TAB. Footer block it is 
  /// separated from another part with empty line.
  /// 
  /// Query results contains several sections:
  /// +) column names block
  /// +) column widths markers block
  /// +) record blocks
  /// +) footer block
  /// 
  /// All sections except "record blocks" contains only 
  /// one block,  "record blocks" contains multiple blocks.
  /// 
  /// Column names section obviously contains names
  /// of columns in query.
  /// 
  /// Column widths markers section contains set of 
  /// blocks of symbol '-' delimited with SPACE. 
  /// Length of each block defines width of corresponding
  /// column.
  /// 
  /// Records section contains multiple blocks representing
  /// actual data of each record from database.
  /// 
  /// Footer section contains information on how many 
  /// records query results contains. It is separated from
  /// other sections with one EMPTY line.
  /// 
  /// -------------------------------------------------------
  /// Parser reads input stream and concatenates separate 
  /// lines of each block into one line removing TAB symbols.
  /// 
  /// Depending on required information parser may return
  /// raw strings (appropriate for simple viewing),
  /// comma separated values file (appropriate for opening 
  /// in Excel) or parsed into name-value objects (very convenient
  /// for future handling of information).
  /// 
  /// -------------------------------------------------------
  /// See example of query results where lilnes of each block
  /// are concatenated to one line at the bottom of this source file.
  /// </remarks>
  public class QueryResultParser
  {
    #region Private Fields
    #endregion Private Fields

    #region Public Methods
    /// <summary>
    /// Creates enumerator which returns one string for each block
    /// without any additional modifications.
    /// </summary>
    /// <remarks>
    /// Creates enumerator which returns one concatenated 
    /// string for each block. 
    /// Multiline blocks are concatenated into one string.
    /// No any modifications except removeing of excess TAB 
    /// characters concatenation are made.
    /// All string (including empty ones) are returned.
    /// </remarks>
    public static IEnumerator<string> CreateRawStringsEnumerator(TextReader reader)
    {
      return new QueryRawStringsEnumerator(reader);
    }

    /// <summary>
    /// Creates enumerator which returns CSV strings.
    /// </summary>
    /// <remarks>
    /// Creates enumerator which returns CSV strings suitiable
    /// for opening in Excel or other CSV aware program. 
    /// Only "colum names" block and "record blocks" are returned.
    /// </remarks>
    public static IEnumerator<string> CreateCsvStringsEnumerator(TextReader reader)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Creates enumerator which returns dictionary for each record block.
    /// </summary>
    /// <remarks>
    /// Creates enumerator which returns dictionary for 
    /// each record block.
    /// Each returned dictionary contains record for all columns.
    /// If value for some column is empty string then it's 
    /// represented with "null".
    /// </remarks>
    public static IEnumerator<IDictionary<string, string>> CreateRecordsEnumerator(TextReader reader)
    {
      return new QueryRecordsEnumerator(reader);
    }

    #endregion Public Methods

    #region Inner Types
    #endregion Inner Types
  }
}

/*
 number      subnumber deadline     status                         date         closedate    application                                        modul                                              submodul                                           apprelease                     frelease                       severity                       priority                       contributor                    leader                         developer                      qa                             summary                                                                                                                                                          
 ----------- --------- ------------ ------------------------------ ------------ ------------ -------------------------------------------------- -------------------------------------------------- -------------------------------------------------- ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
       15210         0 bug          open                           2004/02/23                Installer                                                                                                                                                unknown                                                                                      n/a                            viktorov                                                                                                                    VPIplayer: the error message comes up during deinstallation: "-1612..."                                                                                           
       13624         1 bug          to_be_assigned                 2003/10/13                VPIplayer                                                                                                                                                1.0                            1.0                            2                              1                              a.lowery                       ronnie                                                                                       Player sliders: top of inter slider gives invalid integer! (again) 55                                                                                            

 (476 rows affected) 
 */
