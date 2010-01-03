using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BugDB.QueryParser
{
  /// <summary>
  /// Implementation of enumerator which returns 
  /// concatenated strings for each block.
  /// </summary>
  internal class QueryRawStringsEnumerator : IEnumerator<string>
  {
    #region Private Fields
    private readonly TextReader m_reader;
    private string m_currentBlock; // Current concatenated block string
    private string m_nextLine; // Line read previously if any
//    private bool m_finished; // Set to true when end is reached
    #endregion Private Fields

    #region Constructors
    public QueryRawStringsEnumerator(Stream stream)
    {
      m_reader = new StreamReader(stream);
    }
    #endregion Constructors

    #region Implementation of IDisposable

    /// <summary>
    /// Performs application-defined tasks associated with freeing, 
    /// releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      m_reader.Dispose();
    }

    #endregion

    #region Implementation of IEnumerator
    /// <summary>
    /// Advances the enumerator to the next block in file.
    /// </summary>
    /// <returns>
    /// true if next block was successfully read; 
    /// false if there are no more blocks in file.
    /// </returns>
    public bool MoveNext()
    {
      m_currentBlock = ReadNextBlock(ref m_nextLine);
      return m_currentBlock != null;
    }

    /// <summary>
    /// Not supported.
    /// </summary>
    public void Reset()
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// Gets the current block.
    /// </summary>
    public string Current
    {
      get
      {
        if (m_currentBlock == null)
        {
          throw new InvalidOperationException(
            "Enumerator isn't started. Call MoveNext() first.");
        }
        return m_currentBlock;
      }
    }

    /// <summary>
    /// Gets the current block.
    /// </summary>
    object IEnumerator.Current
    {
      get { return Current; }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Read input, merge lines and write to output.
    /// </summary>
   // public void Process(OutputType outType, TextReader input, TextWriter output)
   
    /// <summary>
    /// Reads all lines of block, concatenates them and 
    /// returns concatenate string.
    /// </summary>
    /// <remarks>
    /// Method assumes that stream is pointed at the beginning of 
    /// the block if line == null or 
    /// at it's second line if line != null,
    /// line contains first in that case.
    /// </remarks>
    private string ReadNextBlock(ref string line)
    {
      StringBuilder blockBuilder = null;
      string blockString = null;

      // Merge all lines in block into one line
      // All the rest lines in group start with TAB
      do
      {
        // If next line is null or it is second iteration
        // then line should be read from stream
        if (line == null || blockBuilder != null )
        {
          line = m_reader.ReadLine();
        }

        // Second and the rest in block start with TAB
        if (line != null && line.StartsWith("\t"))
        {
          // Check that builder is initialized (e.g. not first line in block)
          // Throw exception if first line in block starts with TAB
          if (blockBuilder == null)
          {
            throw new Exception("First line in block shouldn't start with TAB");
          }
          // Remove leading TAB
          line = line.Substring(1);
          // Add line to builder
          blockBuilder.Append(line);
        }
        else
        {
          // Found beginning of the next block or end of file
          if (blockBuilder != null)
          {
            // Get block string and exit
            blockString = blockBuilder.ToString();
            break;
          }

          // Start of the current block
          if (line != null)
          {
            // Initialize new string builder
            blockBuilder = new StringBuilder(line);
          }
        }
      } while (line != null);

      return blockString;
    }

    /// <summary>
    /// Replace spaces between columns to TABs.
    /// </summary>
    //private string MakeTSVLine(ColumnInfo[] columns, string headerLine)
    //{
    //  StringBuilder tsvLine = new StringBuilder();
    //  foreach (ColumnInfo column in columns)
    //  {
    //    if (tsvLine.Length != 0)
    //    {
    //      tsvLine.Append('\t');
    //    }
    //    tsvLine.Append(headerLine.Substring(column.Position, column.Width));
    //  }
    //  return tsvLine.ToString();
    //}

    #endregion Private Methods
   }
}