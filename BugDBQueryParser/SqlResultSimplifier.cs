using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace BugReportTransformer
{
    enum OutputType
    {
        View,
        Tsv
    }

    class ColumnInfo
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

    class SqlResultSimplifier
    {
        private int m_linesToMerge = -1;
        private int m_headerBlocks = -1;

        public SqlResultSimplifier(int linesToMerge, int headerBlocks)
        {
            m_linesToMerge = linesToMerge;
            m_headerBlocks = headerBlocks;
        }

        /// <summary>
        /// Read input, merge lines and write to output.
        /// </summary>
        public void Process(OutputType outType, TextReader input, TextWriter output)
        {
            string line;
            string headerLine = null;
            StringBuilder stringBuilder = null;
            int lineCount = 0;
            ColumnInfo[] columns = null;

            // Merge all lines in block into one line
            // All the rest lines in group start with TAB
            while ((line = input.ReadLine()) != null)
            {
                // Second and the rest in block start with TAB
                if (line.StartsWith("\t"))
                {
                    // Check that builder is initialized
                    Debug.Assert(stringBuilder != null);
                    // Remove leading TAB
                    line = line.Substring(1);
                    // Add line to builder
                    stringBuilder.Append(line);
                }
                else
                {
                    // Handle previously built string if any
                    if (stringBuilder != null)
                    {
                        string mergedLine = stringBuilder.ToString();

                        // First line contains colum names
                        if (lineCount == 0)
                        {
                            // Just remember
                            headerLine = mergedLine;
                        }
                        // Second line contains field length information
                        if (lineCount == 1)
                        {
                            columns = ParseColumns(headerLine, mergedLine);
                        }

                        if (outType == OutputType.Tsv)
                        {
                            // Postpone first line until second is known for TSV
                            // Because column widths aren't known yet
                            if (lineCount == 1)
                            {
                                // Write correct header line
                                // instead of width markers line
                                output.WriteLine(MakeTSVLine(columns, headerLine));
                            }
                            else if (lineCount > 1)
                            {
                                // Write corrected line
                                output.WriteLine(MakeTSVLine(columns, mergedLine));
                            }
                        }
                        else
                        {
                            // Write
                            output.WriteLine(mergedLine);
                        }
                        // And count
                        lineCount++;
                    }

                    // First line starts with space
                    if (line.StartsWith(" ")) 
                    {
                        // Remove leading space
                        line = line.Substring(1);
                        // Initialize new string builder
                        stringBuilder = new StringBuilder(line);
                    }
                    else // Final block
                    {
                        // Indicate all lines were processed
                        stringBuilder = null;

                        // Write final block as is to debug output
                        while ((line = input.ReadLine()) != null)
                        {
                            Debug.WriteLine(line);
                        }
                    }
                }
            }

            // Write last built string if any
            if (stringBuilder != null)
            {
                // Shouldn't get here
                Debug.Fail("No final block");

                // Write
                output.WriteLine(stringBuilder.ToString());
                // And count
                lineCount++;
            }

            // Report count
            Debug.WriteLine(String.Format("Count: {0}", lineCount - m_headerBlocks));
        }

        /// <summary>
        /// Replace spaces between columns to TABs.
        /// </summary>
        private string MakeTSVLine(ColumnInfo[] columns, string headerLine)
        {
            StringBuilder tsvLine = new StringBuilder();
            foreach (ColumnInfo column in columns)
            {
                if (tsvLine.Length != 0)
                {
                    tsvLine.Append('\t');
                }
                tsvLine.Append(headerLine.Substring(column.Position, column.Width));
            }
            return tsvLine.ToString();
        }

        /// <summary>
        /// Returns width of columns.
        /// </summary>
        private ColumnInfo[] ParseColumns(string headerLine, string columnWidthLine)
        {
            string[] widths = columnWidthLine.Split(' ');

            int count = widths.Length - 1;
            ColumnInfo[] columns = new ColumnInfo[count];

            int pos = 0;
            for (int i = 0; i < count; i++)
            {
                int width = widths[i].Length;

                columns[i] = new ColumnInfo();
                columns[i].Title = headerLine.Substring(pos, width).Trim();
                columns[i].Width = width;
                columns[i].Position = pos;

                pos += width + 1;
            }
            return columns;
        }
    }
}
