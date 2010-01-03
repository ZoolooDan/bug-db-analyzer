using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace BugDB.QueryParser
{
  /// <summary>
  /// Implementation of enumerator which returns dictionaries
  /// for each record.
  /// </summary>
  /// <remarks>
  /// Implementation of this enumerator is based on 
  /// QueryRawStringsEnumerator.
  /// First string is treated as column names, second
  /// as column widths markers, all the rest till empty line - 
  /// as record strings.
  /// </remarks>
  internal class QueryRecordsEnumerator : IEnumerator<IDictionary<string, string>>
  {
    #region Private Fields
    // Regex to check that line contains only columns width markers
    private static readonly Regex s_markersRegex = new Regex("^( -+)+$",
      RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | 
      RegexOptions.CultureInvariant | RegexOptions.Compiled );

    private QueryRawStringsEnumerator m_rawEnum; // Raw strings enumerator

    private ColumnInfo[] m_columns; // Columns information

    private IDictionary<string, string> m_currentRecord;

    private bool m_finished; // True when end of records block reached
    #endregion Private Fields

    #region Constructors
    public QueryRecordsEnumerator(Stream stream)
    {
      m_rawEnum = new QueryRawStringsEnumerator(stream);

      string namesBlock = null;
      string widthsBlock = null;
      // Vital condition for proper work of this enumerator is 
      // existence of two header blocks in file:
      // +) column names and 
      // +) column width markers
      // So read those blocks and parse
      if( m_rawEnum.MoveNext() )
      {
        namesBlock = m_rawEnum.Current;
 
        if( m_rawEnum.MoveNext() )
        {
          widthsBlock = m_rawEnum.Current;
        }
      }

      // If lines available - parse them
      if( namesBlock != null && widthsBlock != null )
      {
        m_columns = ParseColumns(namesBlock, widthsBlock);
      }
      else
      {
        // Else throw exception
        throw new Exception("No header lines.");
      }
    }
    #endregion Constructors

    #region Implementation of IDisposable
    /// <summary>
    /// Performs application-defined tasks associated with freeing, 
    /// releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      m_rawEnum.Dispose();
    }

    #endregion

    #region Implementation of IEnumerator
    /// <summary>
    /// Advances the enumerator to the next element of the collection.
    /// </summary>
    /// <returns>
    /// true if there are more record blocks; 
    /// false if there are no more record blocks (footerr reached).
    /// </returns>
    public bool MoveNext()
    {
      // Assume no more records
      m_currentRecord = null;

      if( !m_finished && m_rawEnum.MoveNext() )
      {
        string recordBlock = m_rawEnum.Current;
        Debug.Assert(recordBlock != null);
        int length = recordBlock.Length;

        // If it's empty line then end of records block is reached
        if( length != 0 )
        {
          // Create record
          m_currentRecord = new Dictionary<string, string>();
          // Process all columns
          foreach (ColumnInfo info in m_columns)
          {
            // Check that block contains record data
            if (length < info.Position + info.Width)
            {
              throw new Exception(
                String.Format("No record data in block for column '{0}'", info.Title));
            }
            // Extract value
            string value = recordBlock.Substring(info.Position, info.Width).Trim();
            // replace empty string with null value
            value = value.Length == 0 ? null : value;
            // Store in current record
            m_currentRecord.Add(info.Title, value);
          }
        }
        else
        {
          m_finished = true;
        }
      }

      return m_currentRecord != null;
    }

    /// <summary>
    /// Not supported.
    /// </summary>
    public void Reset()
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// Gets current record.
    /// </summary>
    public IDictionary<string, string> Current
    {
      get 
      { 
        if( m_currentRecord == null )
        {
          throw new InvalidOperationException();
        }
        return m_currentRecord;
      }
    }

    /// <summary>
    /// Gets the current element in the collection.
    /// </summary>
    /// <returns>
    /// The current element in the collection.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.
    ///                 </exception><filterpriority>2</filterpriority>
    object IEnumerator.Current
    {
      get { return Current; }
    }

    #endregion

    #region Helper Methods
    /// <summary>
    /// Returns true if string matches column markers block.
    /// </summary>
    private static bool IsWidthMarkersBlock(string block)
    {
      return s_markersRegex.IsMatch(block);
    }

    /// <summary>
    /// Returns columns information.
    /// </summary>
    private static ColumnInfo[] ParseColumns(string headerLine, string columnWidthLine)
    {
      string[] widths = columnWidthLine.Split(' ');

      int count = widths.Length - 2;
      ColumnInfo[] columns = new ColumnInfo[count];

      int pos = 1;
      for (int i = 0; i < count; i++)
      {
        int width = widths[i + 1].Length;

        columns[i] = new ColumnInfo
                       {
                         Title = headerLine.Substring(pos, width).Trim(), 
                         Width = width, 
                         Position = pos
                       };

        pos += width + 1;
      }
      return columns;
    }
    #endregion Helper Methods

    #region Inner Classes
    /// <summary>
    /// Stores columns information (title, widht and position in source string).
    /// </summary>
    private class ColumnInfo
    {
      /// <summary>
      /// Title of column
      /// </summary>
      public string Title;
      /// <summary>
      /// Width of column
      /// </summary>
      public int Width;
      /// <summary>
      /// Position of column field in line
      /// </summary>
      public int Position;
    }
    #endregion Inner Classes
  }
}