using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public class Matrix
    {
        /// <summary>
        /// The internal representation of this matrix.
        /// </summary>
        private double[][] matrix;

        /// <summary>
        /// Gets the number of rows of this matrix.
        /// </summary>
        public int RowCount { get; private set; }

        /// <summary>
        /// Gets the number of columns of this matrix.
        /// </summary>
        public int ColumnCount { get; private set; }

        /// <summary>
        /// Gets or sets the element of this Matrix with specified row index and column index.
        /// </summary>
        /// <param name="rowIndex">The row index of the specified element.</param>
        /// <param name="columnIndex">The column index of the specified element.</param>
        /// <returns>The element of this Matrix with specified row index and column index.</returns>
        public double this[int rowIndex, int columnIndex]
        {
            get { return matrix[rowIndex][columnIndex]; }
            set { matrix[rowIndex][columnIndex] = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this Matris is a square matrix or not.
        /// </summary>
        public bool IsSquareMatrix { get { return this.RowCount == this.ColumnCount; } }

        /// <summary>
        /// Returns true if the input 2-D double array has the shape as a matrix, otherwise, false.
        /// </summary>
        /// <param name="array">A 2-D double array.</param>
        /// <returns>true if the input 2-D double array has the same shape as a matrix, otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="array"/> is null.</exception>
        public static bool IsMatrix(double[][] array)
        {
            if (array == null)
                throw new ArgumentNullException("array", "The input 2-D double array must not be null.");

            int columnCount = array[0].Length;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].Length != columnCount)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Initiailizes a matrix using a 2-D double array. This initialization will make a deep copy of the input array.
        /// </summary>
        /// <param name="matrix">The 2-D double array that provides the values for the initialization of this Matrix.</param>
        /// <exception cref="ArgumentException">If the input 2-D double array doesn't have the shape as a matrix.</exception>
        public Matrix(double[][] matrix)
        {
            // Check whether the input 2-D array has the shape as a matrix or not.
            if (!IsMatrix(matrix))
                throw new ArgumentException("The input 2-D double array doesn't have the shape as a matrix.");

            // Get the number of rows and columns of this matrix.
            this.RowCount = matrix.Length;
            this.ColumnCount = matrix[0].Length;

            // Initialize the internal matrix.
            this.matrix = new double[RowCount][];
            for (int i = 0; i < this.RowCount; i++)
            {
                this.matrix[i] = new double[ColumnCount];
                for (int j = 0; j < this.ColumnCount; j++)
                    this.matrix[i][j] = matrix[i][j];
            }
        }

        /// <summary>
        /// Initializes a Matrix with specified number of rows and columns whose values are all equals the specified value.
        /// </summary>
        /// <param name="numRows">The number of rows.</param>
        /// <param name="numColumns">The number of columns.</param>
        /// <param name="value">The specified double value.</param>
        public Matrix(int numRows, int numColumns, double value = 0)
        {
            // Get the number of rows and columns of this matrix.
            this.RowCount = numRows;
            this.ColumnCount = numColumns;

            // Initialize the internal matrix.
            matrix = new double[RowCount][];
            for (int i = 0; i < this.RowCount; i++)
            {
                matrix[i] = new double[ColumnCount];
                for (int j = 0; j < this.ColumnCount; j++)
                    matrix[i][j] = value;
            }
        }

        /// <summary>
        /// Returns a Matrix whose values are all 0 with specified number of rows and columns.
        /// </summary>
        /// <param name="numRows">The number of rows.</param>
        /// <param name="numColumns">The number of columns.</param>
        /// <returns>A Matrix whose values are all 0 with specified number of rows and columns.</returns>
        public static Matrix Zeros(int numRows, int numColumns)
        {
            return new Matrix(numRows, numColumns, 0);
        }

        /// <summary>
        /// Returns a Matrix whose values are all 1 with specified number of rows and columns.
        /// </summary>
        /// <param name="numRows">The number of rows.</param>
        /// <param name="numColumns">The number of columns.</param>
        /// <returns>A Matrix whose values are all 1 with specified number of rows and columns.</returns>
        /// <returns></returns>
        public static Matrix Ones(int numRows, int numColumns)
        {
            return new Matrix(numRows, numColumns, 1);
        }

        /// <summary>
        /// Returns a Matrix with specified number of rows and columns whose values are random.
        /// </summary>
        /// <param name="numRows">The number of rows.</param>
        /// <param name="numColumns">The number of columns.</param>
        /// <param name="lowerLimit">The minimum value can an element of this Matrix have (inclusive).</param>
        /// <param name="upperLimit">The maximum value can an element of this Matrix have (inclusive).</param>
        /// <returns></returns>
        public static Matrix RandomIntMatrix(int numRows, int numColumns, int lowerLimit, int upperLimit)
        {
            Matrix matrix = Zeros(numRows, numColumns);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                    matrix[i, j] = StdRandom.Uniform(lowerLimit, upperLimit + 1);
            }
            return matrix;
        }

        /// <summary>
        /// Returns a Matrix with specified number of rows and columns whose values are random.
        /// </summary>
        /// <param name="numRows">The number of rows.</param>
        /// <param name="numColumns">The number of columns.</param>
        /// <param name="lowerLimit">The minimum value can an element of this Matrix have (inclusive).</param>
        /// <param name="upperLimit">The maximum value can an element of this Matrix have (inclusive).</param>
        public static Matrix RandomDoubleMatrix(int numRows, int numColumns, double lowerLimit, double upperLimit)
        {
            Matrix matrix = Zeros(numRows, numColumns);
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                    matrix[i, j] = StdRandom.Uniform(lowerLimit, upperLimit + 1);
            }
            return matrix;
        }

        /// <summary>
        /// Returns true if the given row index is in the range of this Matrix, otherwise, false.
        /// </summary>
        /// <param name="rowIndex">The given row index.</param>
        /// <returns>true if the given row index is in the range of this Matrix, otherwise, false.</returns>
        private bool RowIndexIsInRange(int rowIndex)
        {
            return ((rowIndex >= 0) && (rowIndex < this.RowCount));
        }

        /// <summary>
        /// Returns true if the given column index is in the range of this Matrix, otherwise, false.
        /// </summary>
        /// <param name="rowIndex">The given column index.</param>
        /// <returns>true if the given column index is in the range of this Matrix, otherwise, false.</returns>
        private bool ColumnIndexIsInRange(int columnIndex)
        {
            return ((columnIndex >= 0) && (columnIndex < this.ColumnCount));
        }

        /// <summary>
        /// Throws an exception if the input row index is out of the range of this Matrix, otherwise, do nothing.
        /// </summary>
        /// <param name="rowIndex">The index of a row.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the input row index is out of the range of this Matrix, otherwise, do nothing.</exception>
        private void CheckRowIndex(int rowIndex)
        {
            if (!RowIndexIsInRange(rowIndex))
                throw new ArgumentOutOfRangeException("rowIndex", "rowIndex is out of the range of this Matrix.");
        }

        /// <summary>
        /// Throws an exception if the input column index is out of the range of this Matrix, otherwise, do nothing.
        /// </summary>
        /// <param name="columnIndex">The index of a column.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the input column index is out of the range of this Matrix, otherwise, do nothing.</exception>
        private void CheckColumnIndex(int columnIndex)
        {
            if (!ColumnIndexIsInRange(columnIndex))
                throw new ArgumentOutOfRangeException("columnIndex", "columnIndex is out of the range of this Matrix.");
        }

        private void CheckIndicies(int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            // Write them here because this operation will return the paramter name as well.
            if (!RowIndexIsInRange(startRowIndex))
                throw new ArgumentOutOfRangeException("startRowIndex", "startRowIndex is out of the range of this Matrix.");
            if (!RowIndexIsInRange(endRowIndex))
                throw new ArgumentOutOfRangeException("endRowIndex", "endRowIndex is out or the range of this Matrix.");
            if (!ColumnIndexIsInRange(startColumnIndex))
                throw new ArgumentOutOfRangeException("startColumnIndex", "startColumnIndex is out of the range of this Matrix.");
            if (!ColumnIndexIsInRange(endColumnIndex))
                throw new ArgumentOutOfRangeException("endColumIndex", "endColumnIndex is out of the range of this Matrix.");

            if (endRowIndex <= startRowIndex)
                throw new ArgumentException("startRowIndex must be less than endRowIndex.");
            if (endColumnIndex <= startColumnIndex)
                throw new ArgumentException("startColumnIndex must be less than endColumnIndex.");
        }

        private void CheckIsSquareMatrix()
        {
            if (!IsSquareMatrix)
                throw new ArgumentException("This is not a square matrix.");
        }

        private void CheckColumnVector(Vector column)
        {
            if (column == null)
                throw new ArgumentNullException("column", "The column to insert is null.");

            if (column.Length != this.RowCount)
                throw new ArgumentException("The length of the vector is not equalt to the row count of this Matrix.");
        }

        private void CheckRowVector(Vector row)
        {
            if (row == null)
                throw new ArgumentNullException("column", "The column to insert is null.");

            if (row.Length != this.ColumnCount)
                throw new ArgumentException("The length of the vector is not equalt to the column count of this Matrix.");
        }

        /// <summary>
        /// Sets the values of the elements in the specified row of this Matrix to 0.
        /// </summary>
        /// <param name="rowIndex">The index of the specified row.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the specified row index is out of range.</exception>
        public void ClearRow(int rowIndex)
        {
            // Check whether the argument is in range.
            CheckRowIndex(rowIndex);

            // Clear the specified row.
            for (int i = 0; i < this.ColumnCount; i++)
                matrix[rowIndex][i] = 0;
        }

        /// <summary>
        /// Sets the values of the elements in the specified column of this Matrix to 0.
        /// </summary>
        /// <param name="columnIndex">The index of the specified column.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the specified row index is out of range.</exception>
        public void ClearColumn(int columnIndex)
        {
            // Check whether the argument is in range.
            CheckColumnIndex(columnIndex);

            // Clear the specified column.
            for (int i = 0; i < this.RowCount; i++)
                matrix[i][columnIndex] = 0;
        }

        /// <summary>
        /// Sets the values of the elements in the specified rows of this Matrix to 0.
        /// </summary>
        /// <param name="rowIndicies">The indicies of the specified rows.</param>
        /// <exception cref="ArgumentOutOfRangeException">If one of the row indicies is out of the range of this Matrix.</exception>
        public void ClearRows(params int[] rowIndicies)
        {
            // Check whether the row indicies are in range.
            for (int i = 0; i < rowIndicies.Length; i++)
            {
                if (!RowIndexIsInRange(rowIndicies[i]))
                    throw new ArgumentOutOfRangeException("rowIndicies", "One of the row indicies is out of the range of this Matrix.");
            }

            // Clear those rows.
            for (int i = 0; i < rowIndicies.Length; i++)
                ClearRow(rowIndicies[i]);
        }

        /// <summary>
        /// Sets the values of the elements in the specified columns of this Matrix to 0.
        /// </summary>
        /// <param name="columnIndicies">The indicies of the specified columns.</param>
        /// <exception cref="ArgumentOutOfRangeException">If one of the column indicies is out of the range of this Matrix.</exception>
        public void ClearColumns(params int[] columnIndicies)
        {
            // Check whether the column indicies are in range.
            for (int i = 0; i < columnIndicies.Length; i++)
            {
                if (!ColumnIndexIsInRange(columnIndicies[i]))
                    throw new ArgumentOutOfRangeException("columnIndicies", "One of the column indicies is out or the range of this Matrix.");
            }

            // Clear those columns.
            for (int i = 0; i < columnIndicies.Length; i++)
                ClearColumn(columnIndicies[i]);
        }

        public void ClearSubMatrix(int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            CheckIndicies(startRowIndex, startColumnIndex, endRowIndex, endColumnIndex);

            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                for (int j = startColumnIndex; j < endColumnIndex; j++)
                    matrix[i][j] = 0;
            }
        }

        /// <summary>
        /// Set all values whose absolute value is smaller than the threshold to zero, in-place.
        /// </summary>
        /// <param name="threshold"></param>
        public void CoerceZero(double threshold)
        {
            if (threshold <= 0)
                throw new ArgumentException("threshold must be positive.", "threshold");

            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    if (Math.Abs(matrix[i][j]) < threshold)
                        matrix[i][j] = 0;
                }
            }
        }

        /// <summary>
        /// Set all values that meet the predicate to zero, in-place.
        /// </summary>
        /// <param name="zeroPredicate"></param>
        public void CoerceZero(Func<double, bool> zeroPredicate)
        {
            if (zeroPredicate == null)
                throw new ArgumentNullException("zeroPredicate", "The inpute delegate must not be null.");

            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    if (zeroPredicate(matrix[i][i]))
                        matrix[i][j] = 0;
                }
            }
        }

        // A deep copy.
        public Matrix Clone()
        {
            return new Matrix(this.matrix);
        }

        public Vector GetRow(int rowIndex)
        {
            CheckRowIndex(rowIndex);

            return new Vector(matrix[rowIndex]);
        }

        public Vector GetSubRow(int rowIndex, int startColumnIndex, int endColumnIndex)
        {
            CheckRowIndex(rowIndex);

            // Remainging check will be done here in the Vector's constructor.
            return new Vector(matrix[rowIndex], startColumnIndex, endColumnIndex);
        }

        public Vector GetColumn(int columnIndex)
        {
            CheckColumnIndex(columnIndex);

            double[] vector = new double[this.RowCount];
            for (int i = 0; i < vector.Length; i++)
                vector[i] = matrix[i][columnIndex];

            return new Vector(vector);
        }

        public Vector GetSubColumn(int columnIndex, int startRowIndex, int endRowIndex)
        {
            CheckColumnIndex(columnIndex);

            double[] vector = new double[endRowIndex - startRowIndex + 1];
            for (int i = 0; i < vector.Length; i++)
                vector[i] = matrix[i + startRowIndex][columnIndex];

            return new Vector(vector);
        }

        // Returns a new matrix containing the upper triangle of this matrix.
        public Matrix UpperTriangular()
        {
            CheckIsSquareMatrix();

            Matrix result = this.Clone();
            for (int i = 1; i < this.RowCount; i++)
            {
                for (int j = 0; j < i; j++)
                    result[i, j] = 0;
            }
            return result;
        }

        // Returns a new matrix containing the lower triangle of this matrix.
        public Matrix LowerTriangular()
        {
            CheckIsSquareMatrix();

            Matrix result = this.Clone();
            for (int j = 1; j < this.ColumnCount; j++)
            {
                for (int i = 0; i < j; i++)
                    result[i, j] = 0;
            }

            return result;
        }

        public Matrix SubMatrix(int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            CheckIndicies(startRowIndex, startColumnIndex, endRowIndex, endColumnIndex);
            int numRows = endRowIndex - startRowIndex + 1;
            int numColumns = endColumnIndex - startColumnIndex + 1;
            Matrix result = new Matrix(numRows, numColumns);
            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < result.ColumnCount; j++)
                    result[i, j] = this[i + startRowIndex, j + startColumnIndex];
            }

            return result;
        }

        public Vector GetDiagonal()
        {
            CheckIsSquareMatrix();
            Vector result = new Vector(this.RowCount);
            for (int i = 0; i < result.Length; i++)
                result[i] = matrix[i][i];

            return result;
        }

        public Matrix InsertColumn(Vector column)
        {
            CheckColumnVector(column);

            Matrix result = new Matrix(this.RowCount + 1, this.ColumnCount);
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i, j] = this[i, j];
            }

            for (int i = 0; i < column.Length; i++)
                result[i, this.ColumnCount] = column[i];

            return result;
        }

        public Matrix RemoveColumn(int columnIndex)
        {
            CheckColumnIndex(columnIndex);

            Matrix result = new Matrix(this.RowCount, this.ColumnCount - 1);
            for (int j = 0; j < columnIndex; j++)
            {
                for (int i = 0; i < result.RowCount; i++)
                    result[i, j] = this[i, j];
            }
            for (int j = columnIndex + 1; j < this.ColumnCount; j++)
            {
                for (int i = 0; i < result.RowCount; i++)
                    result[i, j - 1] = this[i, j];
            }

            return result;
        }

        public void SetColumn(Vector column, int columnIndex)
        {
            CheckColumnIndex(columnIndex);
            CheckColumnVector(column);

            for (int i = 0; i < this.RowCount; i++)
                this[i, columnIndex] = column[i];
        }

        public void SetColumn(Vector column, int columnIndex, int startRowIndex, int endRowIndex)
        {
            if(column == null)
                throw new ArgumentNullException("column", "The column to insert is null.");

            if (!RowIndexIsInRange(startRowIndex))
                throw new ArgumentOutOfRangeException("startRowIndex", "startRowIndex is out of the range of this Matrix.");
            if (!RowIndexIsInRange(endRowIndex))
                throw new ArgumentOutOfRangeException("endRowIndex", "endRowIndex is out or the range of this Matrix.");
            if (!ColumnIndexIsInRange(columnIndex))
                throw new ArgumentOutOfRangeException("startColumnIndex", "startColumnIndex is out of the range of this Matrix.");

            if (endRowIndex <= startRowIndex)
                throw new ArgumentException("startRowIndex must be less than endRowIndex.");

            int length = endRowIndex - startRowIndex + 1;
            if (column.Length != length)
                throw new ArgumentException("The length of the range is not equal to the length of the column to insert.");

            for (int i = 0; i < length; i++)
                this[i + startRowIndex, columnIndex] = column[i];
        }

        // Copies the values of the given array to the specified column.
        public void SetColumn(double[] column, int columnIndex)
        {
            if (column == null)
                throw new ArgumentNullException("column", "The column to insert is null.");
            if (column.Length != this.RowCount)
                throw new ArgumentException("The length of the vector is not equal to the row count of this Matrix.");

            for (int i = 0; i < this.RowCount; i++)
                this[i, columnIndex] = column[i];
        }

        public Matrix InsertRow(Vector row)
        {
            CheckRowVector(row);

            Matrix result = new Matrix(this.RowCount, this.ColumnCount + 1);
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i, j] = this[i, j];
            }
            for (int j = 0; j < this.RowCount; j++)
                result[this.RowCount, j] = row[j];

            return result;
        }

        public Matrix RemoveRow(int rowIndex)
        {
            CheckRowIndex(rowIndex);
            Matrix result = new Matrix(this.RowCount - 1, this.ColumnCount);

            for (int i = 0; i < rowIndex; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i, j] = this[i, j];
            }
            for (int i = rowIndex + 1; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i - 1, j] = this[i, j];
            }


            return result;
        }

        public void SetRow(Vector row, int rowIndex)
        {
            CheckRowIndex(rowIndex);
            CheckRowVector(row);

            for (int j = 0; j < this.ColumnCount; j++)
                this[rowIndex, j] = row[j];
        }

        public void SetRow(Vector row, int rowIndex, int startColumnIndex, int endColumnIndex)
        {
            if (row == null)
                throw new ArgumentNullException("column", "The row to insert is null.");
            if (row.Length != this.ColumnCount)
                throw new ArgumentException("The length of the vector is not equal to the column count of this Matrix.");
        }

        // Copies the values of a given matrix into a region in this matrix.
        public void SetSubMatrix(Matrix subMatrix, int startRowIndex, int startColumnIndex)
        {
            CheckRowIndex(startColumnIndex);
            CheckColumnIndex(startColumnIndex);

            if (this.RowCount < subMatrix.RowCount + startRowIndex)
                throw new ArgumentOutOfRangeException("subMatrix", "The row count of subMatrix is larger than the remaining capacity of this Matrix.");
            if (this.ColumnCount < subMatrix.ColumnCount + startColumnIndex)
                throw new ArgumentOutOfRangeException("subMatrix", "The column count of subMatrix is larger than the remaining capacity of this Matrix.");

            for (int i = 0; i < subMatrix.RowCount; i++)
            {
                for (int j = 0; j < subMatrix.ColumnCount; j++)
                    this[i + startRowIndex, j + startColumnIndex] = subMatrix[i, j];
            }
        }

        public void SetDiagonal(Vector diagonal)
        {
            CheckIsSquareMatrix();
            if (this.RowCount != diagonal.Length)
                throw new ArgumentException("The length of the input vector is not equal to the size of the diagonal of this Matrix.");

            for (int i = 0; i < diagonal.Length; i++)
                this[i, i] = diagonal[i];
        }

        public void SetDiagonal(double[] diagonal)
        {
            CheckIsSquareMatrix();
            if (this.RowCount != diagonal.Length)
                throw new ArgumentException("The length of the input vector is not equal to the size of the diagonal of this Matrix.");

            for (int i = 0; i < diagonal.Length; i++)
                this[i, i] = diagonal[i];
        }

        // Returns the transpose of this matrix.
        public Matrix Transpose()
        {
            Matrix result = new Matrix(this.ColumnCount, this.RowCount);
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[j, i] = this[i, j];
            }

            return result;
        }

        // Concatenates this matrix with the given matrix.
        public Matrix Append(Matrix right)
        {
            if (right == null)
                throw new ArgumentNullException("right", "The Matrix to append is null.");
            if (this.RowCount != right.RowCount)
                throw new ArgumentException("The row count of the input Matrix is not equal to the the row count of this Matrix.");

            Matrix result = new Matrix(this.RowCount, this.ColumnCount + right.ColumnCount);
            for (int j = 0; j < this.ColumnCount; j++)
            {
                for (int i = 0; i < this.RowCount; i++)
                    result[i, j] = this[i, j];
            }
            for (int j = 0; j < right.ColumnCount; j++)
            {
                for (int i = 0; i < right.RowCount; i++)
                    result[i, j + this.ColumnCount] = right[i, j];
            }

            return result;
        }

        /// <summary>
        /// Stacks this matrix on top of the given matrix and places the result into the result matrix.
        /// </summary>
        /// <param name="lower">The matrix to stack this matrix upon.</param>
        public Matrix Stack(Matrix lower)
        {
            if (lower == null)
                throw new ArgumentNullException("lower", "The Matrix to stack is null.");
            if (this.ColumnCount != lower.ColumnCount)
                throw new ArgumentException("The column count of the input Matrix is not equal to the column count of this Matrix.");

            Matrix result = new Matrix(this.RowCount + lower.RowCount, this.ColumnCount);
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i, j] = this[i, j];
            }
            for (int i = 0; i < lower.RowCount; i++)
            {
                for (int j = 0; j < lower.ColumnCount; j++)
                    result[i + this.RowCount, j] = lower[i, j];
            }

            return result;
        }

        // Evaluates whether this matrix is hermitian (conjugate symmetric).
        public bool IsSymmetric(double epsilon = 1e-5)
        {
            CheckIsSquareMatrix();

            for (int i = 1; i < this.RowCount; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Math.Abs(matrix[i][j] - matrix[j][i]) >= epsilon)
                        return false;
                }
            }

            return true;
        }

        public double[,] ToArray()
        {
            double[,] result = new double[this.RowCount, this.ColumnCount];
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i, j] = this[i, j];
            }

            return result;
        }

        /// <summary>
        /// Returns the matrix's elements as an array with the data laid out column by column (column major).
        /// The returned array will be independent from this matrix.
        /// A new memory block will be allocated for the array.
        /// </summary>
        /// <example><pre>
        /// 1, 2, 3
        /// 4, 5, 6  will be returned as  1, 4, 7, 2, 5, 8, 3, 6, 9
        /// 7, 8, 9
        /// </pre></example>
        /// <returns>An array containing the matrix's elements.</returns>
        public double[] ToColumnMajorArray()
        {
            double[] result = new double[this.RowCount * this.ColumnCount];
            int nextIndex = 0;

            for (int j = 0; j < this.ColumnCount; j++)
            {
                for (int i = 0; i < this.RowCount; i++)
                    result[nextIndex++] = this[i, j];
            }

            return result;
        }

        /// <summary>
        /// Returns the matrix's elements as an array with the data laid row by row (row major).
        /// The returned array will be independent from this matrix.
        /// A new memory block will be allocated for the array.
        /// </summary>
        /// <example><pre>
        /// 1, 2, 3
        /// 4, 5, 6  will be returned as  1, 2, 3, 4, 5, 6, 7, 8, 9
        /// 7, 8, 9
        /// </pre></example>
        /// <returns>An array containing the matrix's elements.</returns>
        public double[] ToRowMajorArray()
        {
            double[] result = new double[this.RowCount * this.ColumnCount];
            int nextIndex = 0;

            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[nextIndex++] = this[i, j];
            }

            return result;
        }

        /// <summary>
        /// Returns this matrix as array of row arrays.
        /// The returned arrays will be independent from this matrix.
        /// A new memory block will be allocated for the arrays.
        /// </summary>
        public double[][] ToRowArrays()
        {
            double[][] result = new double[this.RowCount][];
            for (int i = 0; i < this.RowCount; i++)
            {
                result[i] = new double[this.ColumnCount];
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i][j] = matrix[i][j];
            }

            return result;
        }

        /// <summary>
        /// Returns this matrix as array of column arrays.
        /// The returned arrays will be independent from this matrix.
        /// A new memory block will be allocated for the arrays.
        /// </summary>
        public double[][] ToColumnArrays()
        {
            double[][] result = new double[this.ColumnCount][];
            for (int j = 0; j < this.ColumnCount; j++)
            {
                result[j] = new double[this.RowCount];
                for (int i = 0; i < this.RowCount; i++)
                    result[j][i] = matrix[i][j];
            }

            return result;
        }


        public static Matrix operator +(Matrix matrix, double scalar)
        {
            Matrix result = matrix.Clone();
            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < result.ColumnCount; j++)
                    result[i, j] += scalar;
            }

            return result;
        }

        public static Matrix operator +(double scalar, Matrix matrix)
        {
            return matrix + scalar;
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            return null;
        }

        public static Matrix operator -(Matrix matrix, double scalar)
        {
            return null;
        }

        public static Matrix operator -(double scalar, Matrix matrix)
        {
            return null;
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return null;
        }

        public static Matrix operator *(Matrix matrix, double scalar)
        {
            return null;
        }

        public static Matrix operator *(double scalar, Matrix matrix)
        {
            return matrix * scalar;
        }

        public static Matrix operator *(Matrix matrix, Vector columnVector)
        {
            return null;
        }

        public static Matrix operator *(Matrix matrixLeft, Matrix matrixRight)
        {
            return null;
        }

        public static Matrix operator /(Matrix matrix, double scalar)
        {
            return null;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}