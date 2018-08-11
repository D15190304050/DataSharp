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
        /// <param name="csvFilePath">The URI of the CSV file.</param>
        public DataFrame(Uri csvFilePath)
        {

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
