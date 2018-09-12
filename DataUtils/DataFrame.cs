using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtils
{
    // Unsolved:
    // * Implement Linq interfaces such as Where().


    /// <summary>
    /// The <see cref="DataFrame" />  class represents a data table that holds structured data. Each row in the table represents a record, and each column in the table represents a kind of attribute.
    /// </summary>
    /// <remarks>
    /// This is the C# implementation of DataFrame Python Pandas.
    /// </remarks>
    public class DataFrame
    {
        /// <summary>
        /// Gets the <see cref="DataFrameRow" /> associated with the given row index.
        /// </summary>
        /// <param name="rowIndex">The index of the row to retrieve.</param>
        /// <returns>The <see cref="DataFrameRow" /> associated with the given row index.</returns>
        public DataFrameRow this[int rowIndex]
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the <see cref="DataFrameRow" /> associated with the given row label.
        /// </summary>
        /// <param name="rowName">The name of the row to reterive.</param>
        /// <returns>The <see cref="DataFrameRow" /> associated with the given row label.</returns>
        public DataFrameRow this[string rowName]
        {
            get { return null; }

            // Modify the data row in-place, add a new data row if the row label does not exist before.
            set { }
        }

        /// <summary>
        /// Gets the names of the row level indices.
        /// </summary>
        public IEnumerable<string> RowNames
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the names of the column level indices.
        /// </summary>
        public IEnumerable<string> ColumnNames
        {
            get { return null; }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DataFrame" />  from a CSV file with given URI.
        /// </summary>
        /// <param name="csvPath">The path of the CSV file.</param>
        public DataFrame ReadCsv(string csvPath)
        {
            return null;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DataFrame" />  from a txt file with given URI.
        /// </summary>
        /// <param name="txtPath">The path of the txt file.</param>
        public DataFrame ReadTxt(string txtPath)
        {
            return null;
        }

        /// <summary>
        /// Returns the first <paramref name="numLines" /> lines of data in this <see cref="DataFrame" /> .  It is useful for quickly testing if your object has the right type of data in it.
        /// </summary>
        /// <param name="numLines">The number of records to return.</param>
        /// <returns></returns>
        public IEnumerable<string> Head(int numLines = 5)
        {
            return null;
        }

        /// <summary>
        /// Returns a deep copy of this <see cref="DataFrame" />.
        /// </summary>
        /// <returns>A deep copy of this <see cref="DataFrame" />.</returns>
        public DataFrame DeepCopy()
        {
            return null;
        }

        /// <summary>
        /// Returns a deep copy of this <see cref="DataFrame" /> with the specified row removed.
        /// </summary>
        /// <param name="rowIndex">The index of the row to be removed.</param>
        /// <returns>A copy of this <see cref="DataFrame" /> with the specified row removed.</returns>
        public DataFrame RemoveRow(int rowIndex)
        {
            return null;
        }

        /// <summary>
        /// Returns a deep copy of this <see cref="DataFrame" /> with the specified row removed.
        /// </summary>
        /// <param name="rowName">The name of the row to be removed.</param>
        /// <returns>A copy of this <see cref="DataFrame" /> with the specified row removed.</returns>
        public DataFrame RemoveRow(string rowName)
        {
            return null;
        }

        /// <summary>
        /// Removes a specified row from this <see cref="DataFrame" /> in-place without creating a new instance of <see cref="DataFrame" />.
        /// </summary>
        /// <param name="rowIndex">The index of the row to be removed.</param>
        public void RemoveRowInPlace(int rowIndex)
        {

        }

        /// <summary>
        /// Removes a specified row from this <see cref="DataFrame" /> in-place without creating a new instance of <see cref="DataFrame" />.
        /// </summary>
        /// <param name="rowName">The name of the row to be removed.</param>
        public void RemoveRowInPlace(string rowName)
        {
            
        }

        /// <summary>
        /// Returns a deep copy of this <see cref="DataFrame" /> with the specified column removed.
        /// </summary>
        /// <param name="columnIndex">The index of the column to be removed.</param>
        /// <returns>A copy of this <see cref="DataFrame" /> with the specified column removed.</returns>
        public DataFrame RemoveColumn(int columnIndex)
        {
            return null;
        }

        /// <summary>
        /// Returns a deep copy of this <see cref="DataFrame" /> with the specified column removed.
        /// </summary>
        /// <param name="columnName">The name of the column to be removed.</param>
        /// <returns>A copy of this <see cref="DataFrame" /> with the specified column removed.</returns>
        public DataFrame RemoveColumn(string columnName)
        {
            return null;
        }

        /// <summary>
        /// Removes a specified column from this <see cref="DataFrame" /> in-place without creating a new instance of
        /// <see cref="DataFrame" />.
        /// </summary>
        /// <param name="columnIndex">The index of the column to be removed.</param>
        public void RemoveColumnInPlace(int columnIndex)
        {

        }

        /// <summary>
        /// Removes a specified column from this <see cref="DataFrame" /> in-place without creating a new instance of
        /// <see cref="DataFrame" />.
        /// </summary>
        /// <param name="columnName">The name of the column to be removed.</param>
        public void RemoveColumnInPlace(string columnName)
        {

        }

        /// <summary>
        /// Returns a deep copy of this <see cref="DataFrame" /> with the specified columns in the keys of the given
        /// dictionary changed to the values in the same dictionary, respectively.
        /// </summary>
        /// <param name="columns">The dictionary that contains the names to rename.</param>
        /// <returns>A new <see cref="DataFrame" /> instance with the specified columns renamed.</returns>
        /// <remarks>
        /// The original names of columns are the keys of the given dictionary, the new names of columns are the values
        /// in the same dictionary, respectively. This operation only change the name of the columns, without changing
        /// the data of the columns.
        /// </remarks>
        public DataFrame RenameColumns(Dictionary<string, string> columns)
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
        public void RenameColumnsInPlace(Dictionary<string, string> columns)
        {
        }

        /// <summary>
        /// Returns a deep copy of this <see cref="DataFrame" /> with the specified rows in the keys of the given
        /// dictionary changed to the values in the same dictionary, respectively.
        /// </summary>
        /// <param name="rows">The dictionary that contains the names to rename.</param>
        /// <returns>A new <see cref="DataFrame" /> instance with the specified rows renamed.</returns>
        /// <remarks>
        /// The original names of rows are the keys of the given dictionary, the new names of rows are the values
        /// in the same dictionary, respectively. This operation only change the name of the rows, without changing
        /// the data of the rows.
        /// </remarks>
        public DataFrame RenameRows(Dictionary<string, string> rows)
        {
            return null;
        }

        /// <summary>
        /// Renames the specified rows in the keys of the given dictionary to the values in the same dictionary,
        /// respectively, in-place.
        /// </summary>
        /// <param name="rows">The dictionary that contains the names to rename.</param>
        /// <remarks>
        /// The original names of rows are the keys of the given dictionary, the new names of rows are the values
        /// in the same dictionary, respectively. This operation only change the name of the rows, without changing
        /// the data of the rows.
        /// </remarks>
        public void RenameRowsInPlace(Dictionary<string, string> rows)
        {
        }

        // Maybe this method would be removed.
        /// <summary>
        /// Sets the <see cref="DataFrame" /> row names using one existing columns.
        /// </summary>
        /// <param name="columnName"></param>
        public void SetRowNames(string columnName)
        {
        }

        /// <summary>
        /// Return reshaped <see cref="DataFrame" />  organized by given row / column values. This is like the pivot function of <see cref="DataFrame" />  in Python Pandas.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="values"></param>
        public void Reshape(string row, string column, params string[] values)
        {
        }

        /// <summary>
        /// Returns the transpose of this <see cref="DataFrame" /> .
        /// </summary>
        /// <returns></returns>
        public DataFrame Transpose()
        {
            return null;
        }
    }
}
