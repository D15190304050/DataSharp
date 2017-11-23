using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public class Matrix
    {
        private double[][] matrix;

        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }

        public double this[int row, int column]
        {
            get { return matrix[row][column]; }
            set { matrix[row][column] = value; }
        }

        public void ClearRow(int rowIndex)
        {

        }

        public void ClearColumn(int columnIndex)
        {

        }

        public void ClearRows(params int[] rowIndicies)
        {

        }

        public void ClearColumns(params int[] columnIndicies)
        {

        }

        public void ClearSubMatrix(int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {

        }

        /// <summary>
        /// Set all values whose absolute value is smaller than the threshold to zero, in-place.
        /// </summary>
        /// <param name="threshold"></param>
        public void CoerceZero(double threshold)
        {

        }

        /// <summary>
        /// Set all values that meet the predicate to zero, in-place.
        /// </summary>
        /// <param name="zeroPredicate"></param>
        public void CoerceZero(Func<double, bool> zeroPredicate)
        {

        }

        // A deep copy.
        public void Clone()
        {

        }

        public Vector GetRow(int rowIndex)
        {
            return null;
        }

        public Vector GetSubRow(int rowIndex, int startColumnIndex, int endColumnIndex)
        {
            return null;
        }

        public Vector GetColumn(int columnIndex)
        {
            return null;
        }

        public Vector GetSubColumn(int columnIndex, int startRowIndex, int endRowIndex)
        {
            return null;
        }

        // Returns a new matrix containing the upper triangle of this matrix.
        public Matrix UpperTriangle()
        {
            return null;
        }

        // Returns a new matrix containing the lower triangle of this matrix.
        public Matrix LowerTriangle()
        {
            return null;
        }

        public Matrix SubMatrix(int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            return null;
        }

        public Vector GetDiagonal()
        {
            return null;
        }

        public Matrix InsertColumn(Vector column)
        {
            return null;
        }

        public Matrix RemoveColumn(int columnIndex)
        {
            return null;
        }

        public Matrix SetColumn(Vector column, int columnIndex)
        {
            return null;
        }

        public Matrix SetColumn(Vector column, int columnIndex, int startRowIndex, int endRowIndex)
        {
            return null;
        }
    }
}
