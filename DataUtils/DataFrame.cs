using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtils
{
    /// <summary>
    /// The DataFrame class represents a data table that holds structured data. Each row in the table represents a record, and each column in the table represents a kind of attribute.
    /// </summary>
    public class DataFrame
    {
        /// <summary>
        /// Initializes a new instance of DataFrame from a CSV file with given URI.
        /// </summary>
        /// <param name="csvPath">The path of the CSV file.</param>
        public DataFrame ReadCsv(string csvPath)
        {
            return null;
        }

        /// <summary>
        /// Initializes a new instance of DataFrame from a txt file with given URI.
        /// </summary>
        /// <param name="txtPath">The path of the txt file.</param>
        public DataFrame ReadTxt(string txtPath)
        {
            return null;
        }

        /// <summary>
        /// Returns the first <paramref name="numLines" /> lines of data in this DataFrame.  It is useful for quickly testing if your object has the right type of data in it.
        /// </summary>
        /// <param name="numLines"></param>
        /// <returns></returns>
        public IEnumerable<string> Head(int numLines = 5)
        {
            return null;
        }

        /// <summary>
        /// Removes a specified row from this DataFrame and return the new DataFrame.
        /// </summary>
        /// <param name="rowIndex">The index of the row to be removed.</param>
        /// <returns>A specified row from this DataFrame and return the new DataFrame.</returns>
        public DataFrame RemoveRow(int rowIndex)
        {
            return null;
        }

        /// <summary>
        /// Removes a specified row from this DataFrame in-place without creating a new instance of DataFrame.
        /// </summary>
        /// <param name="rowIndex">The index of the row to be removed.</param>
        public void RemoveRowInPlace(int rowIndex)
        {

        }

        /// <summary>
        /// Removes a specified column from this DataFrame and return the new DataFrame.
        /// </summary>
        /// <param name="columnIndex">The index of the column to be removed.</param>
        /// <returns>A specified column from this DataFrame and return the new DataFrame.</returns>
        public DataFrame RemoveColumn(int columnIndex)
        {
            return null;
        }

        /// <summary>
        /// Removes a specified column from this DataFrame and return the new DataFrame.
        /// </summary>
        /// <param name="columnIndex">The name of the column to be removed.</param>
        /// <returns>A specified column from this DataFrame and return the new DataFrame.</returns>
        public DataFrame RemoveColumn(string columnName)
        {
            return null;
        }

        /// <summary>
        /// Removes a specified column from this DataFrame in-place without creating a new instance of DataFrame.
        /// </summary>
        /// <param name="rowIndex">The index of the column to be removed.</param>
        public void RemoveColumnInPlace(int columnIndex)
        {

        }

        /// <summary>
        /// Removes a specified column from this DataFrame in-place without creating a new instance of DataFrame.
        /// </summary>
        /// <param name="rowIndex">The name of the column to be removed.</param>
        public void RemoveColumnInPlace(string columnName)
        {

        }

        /// <summary>
        /// Renames and returns the specified columns in the keys of the given dictionary to the values in the same
        /// dictionary, respectively.
        /// </summary>
        /// <param name="columns">The dictionary that contains the names to rename.</param>
        /// <returns>A new <see cref="DataFrame" /> instance with the specified columns renamed.</returns>
        /// <remarks>
        /// The original names of columns are the keys of the given dictionary, the new names of columns are the values
        /// in the same dictionary, respectively. This operation only change the name of the columns, without changing
        /// the data of the columns.
        /// </remarks>
        public DataFrame Rename(Dictionary<string, string> columns)
        {
            return null;
        }

        /// <summary>
        /// Renames the specified columns in the keys of the given dictionary to the values in the same dictionary,
        /// respectively, in-place.
        /// </summary>
        /// <param name="columns">The dictionary that contains the names to rename.</param>
        /// <remarks>
        /// The original names of columns are the keys of the given dictionary, the new names of columns are the values
        /// in the same dictionary, respectively. This operation only change the name of the columns, without changing
        /// the data of the columns.
        /// </remarks>
        public void RenameInPlace(Dictionary<string, string> columns)
        {
        }

        /// <summary>
        /// Return reshaped DataFrame organized by given row / column values. This is like the pivot function of DataFrame in Python Pandas.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="values"></param>
        public void Reshape(string row, string column, params string[] values)
        {
        }
    }
}
