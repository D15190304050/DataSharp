using System;
using System.Text;

namespace Mathematics
{
    /// <summary>
    /// The Matrix class represents a matrix in mathematics. This class provides some corresponding operation.
    /// </summary>
    [Serializable]
    public class Matrix
    {
        /// <summary>
        /// The internal representation of this matrix.
        /// </summary>
        private Vector[] matrix;

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
            if (array.Length < 1)
                return false;

            int columnCount = array[0].Length;
            if (columnCount < 1)
                return false;

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].Length != columnCount)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true if the input 2-D double array has the shape as a matrix, otherwise, false.
        /// </summary>
        /// <param name="array">A 2-D double array.</param>
        /// <returns>true if the input 2-D double array has the same shape as a matrix, otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="array"/> is null.</exception>
        public static bool IsMatrix(double[,] array)
        {
            int rowCount = array.GetLength(0);
            int columnCount = array.GetLength(1);
            if ((rowCount < 1) || (columnCount < 1))
                return false;

            return true;
        }

        public static bool IsMatrix(Vector[] vectors)
        {
            if (vectors[0] == null)
                return false;

            int columnCount = vectors[0].Count;
            for (int i = 1; i < vectors.Length; i++)
            {
                Vector v = vectors[i];
                if (v == null)
                    return false;
                if (v.Count != columnCount)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Initiailizes a matrix using a 2-D double array. This initialization will make a deep copy of the input array.
        /// </summary>
        /// <param name="matrix">The 2-D double array that provides the values for the initialization of this Matrix.</param>
        /// <exception cref="ArgumentNullException">If the input 2-D double array is null.</exception>
        /// <exception cref="ArgumentException">If the input 2-D double array doesn't have the shape as a matrix.</exception>
        public Matrix(double[][] matrix)
        {
            // Check whether the input 2-D array is null.
            if (matrix == null)
                throw new ArgumentNullException("matrix", "The input 2-D double array must not be null.");

            // Check whether the input 2-D array has the shape as a matrix or not.
            if (!IsMatrix(matrix))
                throw new ArgumentException("The input 2-D double array doesn't have the shape as a matrix.");

            // Get the number of rows and columns of this matrix.
            this.RowCount = matrix.Length;
            this.ColumnCount = matrix[0].Length;

            // Initialize the internal matrix.
            this.matrix = new Vector[this.RowCount];
            for (int i = 0; i < this.RowCount; i++)
            {
                this.matrix[i] = new Vector(ColumnCount);
                for (int j = 0; j < this.ColumnCount; j++)
                    this[i, j] = matrix[i][j];
            }
        }

        /// <summary>
        /// Initiailizes a matrix using a 2-D double array. This initialization will make a deep copy of the input array.
        /// </summary>
        /// <param name="matrix">The 2-D double array that provides the values for the initialization of this Matrix.</param>
        /// <exception cref="ArgumentNullException">If the input 2-D double array is null.</exception>
        /// <exception cref="ArgumentException">If the input 2-D double array doesn't have the shape as a matrix.</exception>
        public Matrix(double[,] matrix)
        {
            // Check whether the input 2-D array is null.
            if (matrix == null)
                throw new ArgumentNullException("matrix", "The input 2-D double array must not be null.");

            // Check whether the input 2-D array has the shape as a matrix or not.
            if (!IsMatrix(matrix))
                throw new ArgumentException("The input 2-D double array doesn't have the shape as a matrix.");

            this.RowCount = matrix.GetLength(0);
            this.ColumnCount = matrix.GetLength(1);

            // Initialize the internal matrix.
            this.matrix = new Vector[this.RowCount];
            for (int i = 0; i < this.RowCount; i++)
            {
                this.matrix[i] = new Vector(this.ColumnCount);
                for (int j = 0; j < this.ColumnCount; j++)
                    this[i, j] = matrix[i, j];
            }
        }

        /// <summary>
        /// Initializes a matrix using an array of Vector. This initialization will make a deep copy of the input array.
        /// </summary>
        /// <param name="vectors">The array of vectors.</param>
        /// <param name="isRowVector">The bool variable that indicates whether the input Vector is a row vector or not (which means that is a column vector).</param>
        /// <exception cref="ArgumentNullException">If <paramref name="vectors" /> is null.</exception>
        /// <exception cref="ArgumentException">If <paramref name="vectors" /> doesn't have the shape as a matrix.</exception>
        public Matrix(Vector[] vectors, bool isRowVector = true)
        {
            if (vectors == null)
                throw new ArgumentNullException("vectors", "The input array of Vector is null.");

            if (!IsMatrix(vectors))
                throw new ArgumentException("The input array of Vector doesn't have the shape as a matrix.");
            
            if (isRowVector)
            {
                this.RowCount = vectors.Length;
                this.ColumnCount = vectors[0].Count;

                matrix = new Vector[this.RowCount];
                for (int i = 0; i < this.RowCount; i++)
                {
                    matrix[i] = new Vector(this.ColumnCount);
                    for (int j = 0; j < this.ColumnCount; j++)
                        this[i, j] = vectors[i][j];
                }
            }
            else
            {
                this.RowCount = vectors[0].Count;
                this.ColumnCount = vectors.Length;

                matrix = new Vector[this.RowCount];
                for (int i = 0; i < this.RowCount; i++)
                {
                    matrix[i] = new Vector(this.RowCount);
                    for (int j = 0; j < this.ColumnCount; j++)
                        this[i, j] = vectors[j][i];
                }
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
            if ((numRows < 1) || (numColumns < 1))
                throw new ArgumentException("The size of a Matrix must be greater than or equal to 1*1.");

            // Get the number of rows and columns of this matrix.
            this.RowCount = numRows;
            this.ColumnCount = numColumns;

            // Initialize the internal matrix.
            matrix = new Vector[this.RowCount];

            if (value == 0)
            {
                for (int i = 0; i < this.RowCount; i++)
                    matrix[i] = new Vector(this.ColumnCount);
            }
            else
            {
                for (int i = 0; i < this.RowCount; i++)
                {
                    matrix[i] = new Vector(this.ColumnCount);
                    for (int j = 0; j < this.ColumnCount; j++)
                        this[i, j] = value;
                }
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
        /// <exception cref="ArgumentOutOfRangeException">If the input row index is out of the range of this Matrix.</exception>
        private void CheckRowIndex(int rowIndex)
        {
            if (!RowIndexIsInRange(rowIndex))
                throw new ArgumentOutOfRangeException("rowIndex", "rowIndex is out of the range of this Matrix.");
        }

        /// <summary>
        /// Throws an exception if the input column index is out of the range of this Matrix, otherwise, do nothing.
        /// </summary>
        /// <param name="columnIndex">The index of a column.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the input column index is out of the range of this Matrix.</exception>
        private void CheckColumnIndex(int columnIndex)
        {
            if (!ColumnIndexIsInRange(columnIndex))
                throw new ArgumentOutOfRangeException("columnIndex", "columnIndex is out of the range of this Matrix.");
        }

        /// <summary>
        /// Check the indicies of startRowIndex, startColumnIndex, endRowIndex, endColumnIndex, which will be used operate some region of a Matrix.
        /// </summary>
        /// <param name="startRowIndex">The start row index of the sub region (inclusive).</param>
        /// <param name="startColumnIndex">The start column index of the sub region (inclusive).</param>
        /// <param name="endRowIndex">The end row index of the sub region (inclusive).</param>
        /// <param name="endColumnIndex">The end column index of the sub region (inclusive).</param>
        /// <exception cref="ArgumentOutOfRangeException">If one of the indicies is out of range of this Matrix.</exception>
        /// <exception cref="ArgumentException">If the end index is not greater than the start index (both row and column).</exception>
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

        /// <summary>
        /// Throws ArgumentException if this Matrix is not a square matrix.
        /// </summary>
        /// <exception cref="ArgumentException">If this Matrix is not a square matrix.</exception>
        private void CheckIsSquareMatrix()
        {
            if (!IsSquareMatrix)
                throw new ArgumentException("This is not a square matrix.");
        }

        /// <summary>
        /// Throws exception if the input column vector is null or doesn't have the same length as the row count of this matrix.
        /// </summary>
        /// <param name="column">The input column vector to check.</param>
        /// <exception cref="ArgumentNullException">If the input column vector is null.</exception>
        /// <exception cref="ArgumentException">If the input column vector doesn't have the same length as the row count of this matrix.</exception>
        private void CheckColumnVector(Vector column)
        {
            if (column == null)
                throw new ArgumentNullException("column", "The column to insert is null.");

            if (column.Count != this.RowCount)
                throw new ArgumentException("The length of the vector is not equalt to the row count of this Matrix.");
        }

        /// <summary>
        /// Throws exception if the input row vector is null or doesn't have the same length as the row count of this matrix, otherwise, do nothing.
        /// </summary>
        /// <param name="row">The input row vector to check.</param>
        /// <exception cref="ArgumentNullException">If the input row vector is null.</exception>
        /// <exception cref="ArgumentException">If the input row vector doesn't have the same length as the row count of this matrix.</exception>
        private void CheckRowVector(Vector row)
        {
            if (row == null)
                throw new ArgumentNullException("column", "The column to insert is null.");

            if (row.Count != this.ColumnCount)
                throw new ArgumentException("The length of the vector is not equalt to the column count of this Matrix.");
        }

        /// <summary>
        /// Sets all values of components in this Matrix to 0.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    this[i, j] = 0;
            }
        }

        /// <summary>
        /// Sets the values of the components in the specified row of this Matrix to 0.
        /// </summary>
        /// <param name="rowIndex">The index of the specified row.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the specified row index is out of range.</exception>
        public void ClearRow(int rowIndex)
        {
            // Check whether the argument is in range.
            CheckRowIndex(rowIndex);

            // Clear the specified row.
            for (int j = 0; j < this.ColumnCount; j++)
                this[rowIndex, j] = 0;
        }

        /// <summary>
        /// Sets the values of the components in the specified column of this Matrix to 0.
        /// </summary>
        /// <param name="columnIndex">The index of the specified column.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the specified row index is out of range.</exception>
        public void ClearColumn(int columnIndex)
        {
            // Check whether the argument is in range.
            CheckColumnIndex(columnIndex);

            // Clear the specified column.
            for (int i = 0; i < this.RowCount; i++)
                this[i, columnIndex] = 0;
        }

        /// <summary>
        /// Sets the values of the components in the specified rows of this Matrix to 0.
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
        /// Sets the values of the components in the specified columns of this Matrix to 0.
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

        /// <summary>
        /// Sets all the values of the components in the specified region of this Matrix to 0.
        /// </summary>
        /// <param name="startRowIndex">The start row index of the sub region (inclusive).</param>
        /// <param name="startColumnIndex">The start column index of the sub region (inclusive).</param>
        /// <param name="endRowIndex">The end row index of the sub region (inclusive).</param>
        /// <param name="endColumnIndex">The end column index of the sub region (inclusive).</param>
        /// <exception cref="ArgumentOutOfRangeException">If one of the indicies is out of range of this Matrix.</exception>
        /// <exception cref="ArgumentException">If the end index is not greater than the start index (both row and column).</exception>
        public void ClearSubMatrix(int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            CheckIndicies(startRowIndex, startColumnIndex, endRowIndex, endColumnIndex);

            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                for (int j = startColumnIndex; j <= endColumnIndex; j++)
                    this[i, j] = 0;
            }
        }

        /// <summary>
        /// Set all values whose absolute value is smaller than the threshold to zero, in-place.
        /// </summary>
        /// <param name="threshold">The threshold for this operation.</param>
        /// <exception cref="ArgumentException">If threshold is less than or equal to 0.</exception>
        public void CoerceZero(double threshold)
        {
            if (threshold <= 0)
                throw new ArgumentException("threshold must be positive.", "threshold");

            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    if (Math.Abs(this[i, j]) < threshold)
                        this[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Set all values that meet the predicate to zero, in-place.
        /// </summary>
        /// <param name="zeroPredicate">The predicate that determines whether the value a component of this Matrix should be set to 0.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="zeroPredicate" /> is null.</exception>
        public void CoerceZero(Func<double, bool> zeroPredicate)
        {
            if (zeroPredicate == null)
                throw new ArgumentNullException("zeroPredicate", "The inpute delegate must not be null.");

            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    if (zeroPredicate(this[i, j]))
                        this[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Returns a deep copy of this Matrix.
        /// </summary>
        /// <returns>A deep copy of this Matrix.</returns>
        public Matrix Clone()
        {
            return new Matrix(this.matrix);
        }

        /// <summary>
        /// Returns a vector that contains all the components of the specified row of this Matrix.
        /// </summary>
        /// <param name="rowIndex">The index of the specified row.</param>
        /// <returns>A vector that contains all the components of the specified row of this Matrix.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the input row index is out of the range of this Matrix.</exception>
        public Vector GetRow(int rowIndex)
        {
            CheckRowIndex(rowIndex);
            return new Vector(matrix[rowIndex]);
        }

        /// <summary>
        /// Gets a Vector than contains all the components of the specified row with of this Matrix specified range.
        /// </summary>
        /// <param name="rowIndex">The index of the specified row.</param>
        /// <param name="startColumnIndex">The start column index (inclusive).</param>
        /// <param name="endColumnIndex">The end column index (inclusive).</param>
        /// <returns>A Vector than contains all the components of the specified row with specified range of this Matrix.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the input row index is out of the range of this Matrix.</exception>
        /// <exception cref="ArgumentException">If <paramref name="startColumnIndex" /> or <paramref name="endColumnIndex" /> is out of range or <paramref name="startColumnIndex" /> is not less than <paramref name="endColumnIndex" />.</exception>
        public Vector GetSubRow(int rowIndex, int startColumnIndex, int endColumnIndex)
        {
            CheckRowIndex(rowIndex);

            // Remainging check will be done here in the Vector's constructor.
            return matrix[rowIndex].GetSubVector(startColumnIndex, endColumnIndex);
        }

        /// <summary>
        /// Gets a Vector than contains all the components of the specified column of this Matrix.
        /// </summary>
        /// <param name="columnIndex">The index of the specified column.</param>
        /// <returns>A Vector than contains all the components of the specified column of this Matrix.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the input column index is out of the range of this Matrix.</exception>
        public Vector GetColumn(int columnIndex)
        {
            CheckColumnIndex(columnIndex);

            double[] vector = new double[this.RowCount];
            for (int i = 0; i < vector.Length; i++)
                vector[i] = this[i, columnIndex];

            return new Vector(vector);
        }

        /// <summary>
        /// Gets a Vector than contains all the components of the specified column of this Matrix specified range.
        /// </summary>
        /// <param name="columnIndex">The index of the specified column.</param>
        /// <param name="startRowIndex">The start row index (inclusive).</param>
        /// <param name="endRowIndex">The end row index (inclusive).</param>
        /// <returns>a Vector than contains all the components of the specified column of this Matrix specified range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the input column index is out of the range of this Matrix.</exception>
        /// <exception cref="ArgumentException">If <paramref name="startRowIndex" /> or <paramref name="endRowIndex" /> is out of range or <paramref name="startRowIndex" /> is not less than <paramref name="endRowIndex" />.</exception>
        public Vector GetSubColumn(int columnIndex, int startRowIndex, int endRowIndex)
        {
            CheckColumnIndex(columnIndex);

            double[] vector = new double[endRowIndex - startRowIndex + 1];
            for (int i = 0; i < vector.Length; i++)
                vector[i] = this[i + startRowIndex, columnIndex];

            return new Vector(vector);
        }

        /// <summary>
        /// Returns a new matrix containing the upper triangle of this matrix. The values of components of the other part of the returned matrix are all 0.
        /// </summary>
        /// <returns>A new matrix containing the upper triangle of this matrix. The values of components of the other part of the returned matrix are all 0.</returns>
        /// <exception cref="ArgumentException">If this Matrix is not a square matrix.</exception>
        public Matrix UpperTriangular()
        {
            // Only a square matrix have its upper triangular matrix.
            CheckIsSquareMatrix();

            // Make a deep copy of this Matrix.
            Matrix result = this.Clone();

            // Clear all components in the strict lower triangular region.
            for (int i = 1; i < this.RowCount; i++)
            {
                for (int j = 0; j < i; j++)
                    result[i, j] = 0;
            }

            // Return the upper triangular matrix.
            return result;
        }

        /// <summary>
        /// Returns a new matrix containing the lower triangle of this matrix.
        /// </summary>
        /// <returns>A new matrix containing the lower triangle of this matrix.</returns>
        public Matrix LowerTriangular()
        {
            // Only a square matrix have its lower triangular matrix.
            CheckIsSquareMatrix();

            // Make a deep copy of this Matrix.
            Matrix result = this.Clone();

            // Clear all components in the sttrict upper triangular region.
            for (int j = 1; j < this.ColumnCount; j++)
            {
                for (int i = 0; i < j; i++)
                    result[i, j] = 0;
            }

            // Return the lower triangular matrix.
            return result;
        }

        public double Determinant()
        {
            return 0;
        }

        public Matrix Inverse()
        {
            return null;
        }

        /// <summary>
        /// Do the pre-check for the convolution operation.
        /// </summary>
        /// <remarks>
        /// This implementation correspond to the operation used in convolutional neural networks (CNN).
        /// </remarks>
        /// <param name="matrix1">A Matrix.</param>
        /// <param name="matrix2">The other Matrix.</param>
        /// <param name="padding">Padding for the 1st matrix.</param>
        /// <param name="stride">Stride of the operation.</param>
        /// <exception cref="ArgumentNullException">If one of the input Matrix is null.</exception>
        /// <exception cref="ArgumentException">If the setting is not suitable for the convolution and correlation operation.</exception>
        private static void ConvolutionPreCheck(Matrix matrix1, Matrix matrix2, int padding, int stride)
        {
            if (matrix1 == null)
                throw new ArgumentNullException("matrix1", "matrix1 is null.");
            if (matrix2 == null)
                throw new ArgumentNullException("matrix2", "matrix2 is null.");

            if (padding < 0)
                throw new ArgumentException("Padding of a matrix must be a non-negative integer.");
            if (stride <= 0)
                throw new ArgumentException("Stride of a convolution or correlation operation must be a positive integer.");
            if (!((matrix1.RowCount + 2 * padding - matrix2.RowCount) % stride == 0))
                throw new ArgumentException("The stride is not fit.");
            if (!((matrix1.ColumnCount + 2 * padding - matrix2.ColumnCount) % stride == 0))
                throw new ArgumentException("The stride is not fit.");
        }

        /// <summary>
        /// Computes the convolution of 2 Matrix, with specified padding and stride.
        /// </summary>
        /// <param name="matrix1">A Matrix.</param>
        /// <param name="matrix2">The other Matrix.</param>
        /// <param name="padding">Padding for the convolution operation.</param>
        /// <param name="stride">Stride of the operation.</param>
        /// <returns>The convolution of 2 input Matrix.</returns>
        /// <exception cref="ArgumentNullException">If one of the input Matrix is null.</exception>
        /// <exception cref="ArgumentException">If the setting is not suitable for the convolution and correlation operation.</exception>
        public static Matrix ComputeConvolution(Matrix matrix1, Matrix matrix2, int padding = 0, int stride = 1)
        {
            // Check before computing.
            ConvolutionPreCheck(matrix1, matrix2, padding, stride);

            // Reflect matrix 2.
            double[,] reflectedMatrix2 = new double[matrix2.RowCount, matrix2.ColumnCount];
            for (int i = 0; i < matrix2.RowCount; i++)
            {
                for (int j = 0; j < matrix2.ColumnCount; j++)
                    reflectedMatrix2[i, j] = matrix2[matrix2.RowCount - 1 - i, matrix2.ColumnCount - 1 - j];
            }

            // Pad zeros around matrix 1.
            double[,] extendedMatrix1 = new double[matrix1.RowCount + 2 * padding, matrix1.ColumnCount + 2 * padding];
            for (int i = 0; i < matrix1.RowCount; i++)
            {
                for (int j = 0; j < matrix1.ColumnCount; j++)
                    extendedMatrix1[i + padding, j + padding] = matrix1[i, j];
            }

            // Initialize the result matrix.
            int resultRowCount = 1 + (extendedMatrix1.GetLength(0) - matrix2.RowCount) / stride;
            int resultColumnCount = 1 + (extendedMatrix1.GetLength(1) - matrix2.ColumnCount) / stride;
            Matrix result = new Matrix(resultRowCount, resultColumnCount);

            // Compute the convolution.
            for (int i = 0; i < result.RowCount; i++)
            {
                int startRowIndex = i * stride;

                for (int j = 0; j < result.ColumnCount; j++)
                {
                    int startColumnIndex = j * stride;

                    // Use reflectedMatrix2 here so that CPU doesn't need to read matrix2 to cache to get its row count and column count.
                    // To avoid cache miss.
                    for (int x = 0; x < reflectedMatrix2.GetLength(0); x++)
                    {
                        for (int y = 0; y < reflectedMatrix2.GetLength(1); y++)
                            result[i, j] += extendedMatrix1[startRowIndex + x, startColumnIndex + y] * reflectedMatrix2[x, y];
                    }
                }
            }

            // Return the convolution computed above.
            return result;
        }

        /// <summary>
        /// Computes the correlation of 2 Matrix, with specified padding and stride.
        /// </summary>
        /// <param name="matrix1">A Matrix.</param>
        /// <param name="matrix2">The other Matrix.</param>
        /// <param name="padding">Padding for the correlation operation.</param>
        /// <param name="stride">Stride of the operation.</param>
        /// <returns>The correlation of 2 input Matrix.</returns>
        /// <exception cref="ArgumentNullException">If one of the input Matrix is null.</exception>
        /// <exception cref="ArgumentException">If the setting is not suitable for the convolution and correlation operation.</exception>
        public static Matrix ComputeCorrelation(Matrix matrix1, Matrix matrix2, int padding = 0, int stride = 1)
        {
            // Check before computing.
            ConvolutionPreCheck(matrix1, matrix2, padding, stride);

            // Pad zeros around matrix 1.
            double[,] extendedMatrix1 = new double[matrix1.RowCount + 2 * padding, matrix1.ColumnCount + 2 * padding];
            for (int i = 0; i < matrix1.RowCount; i++)
            {
                for (int j = 0; j < matrix1.ColumnCount; j++)
                    extendedMatrix1[i + padding, j + padding] = matrix1[i, j];
            }

            // Initialize the result matrix.
            int resultRowCount = 1 + (extendedMatrix1.GetLength(0) - matrix2.RowCount) / stride;
            int resultColumnCount = 1 + (extendedMatrix1.GetLength(1) - matrix2.ColumnCount) / stride;
            Matrix result = new Matrix(resultRowCount, resultColumnCount);

            // Compute the convolution.
            for (int i = 0; i < result.RowCount; i++)
            {
                int startRowIndex = i * stride;

                for (int j = 0; j < result.ColumnCount; j++)
                {
                    int startColumnIndex = j * stride;

                    // Use reflectedMatrix2 here so that CPU doesn't need to read matrix2 to cache to get its row count and column count.
                    // To avoid cache miss.
                    for (int x = 0; x < matrix2.RowCount; x++)
                    {
                        for (int y = 0; y < matrix2.ColumnCount; y++)
                            result[i, j] += extendedMatrix1[startRowIndex + x, startColumnIndex + y] * matrix2[x, y];
                    }
                }
            }

            // Return the correlation computed above.
            return result;
        }

        /// <summary>
        /// Returns a Matrix which is the sub-matrix of this Matrix with specified range.
        /// </summary>
        /// <param name="startRowIndex">The start row index of the sub region (inclusive).</param>
        /// <param name="startColumnIndex">The start column index of the sub region (inclusive).</param>
        /// <param name="endRowIndex">The end row index of the sub region (inclusive).</param>
        /// <param name="endColumnIndex">The end column index of the sub region (inclusive).</param>
        /// <returns>A Matrix which is the sub-matrix of this Matrix with specified range.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If one of the indicies is out of range of this Matrix.</exception>
        /// <exception cref="ArgumentException">If the end index is not greater than the start index (both row and column).</exception>
        public Matrix GetSubMatrix(int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            // Check input indicies before extraction.
            CheckIndicies(startRowIndex, startColumnIndex, endRowIndex, endColumnIndex);

            // Get the row count and column count of the sub-matrix.
            int numRows = endRowIndex - startRowIndex + 1;
            int numColumns = endColumnIndex - startColumnIndex + 1;

            // Initialize the sub-matrix.
            Matrix result = new Matrix(numRows, numColumns);

            // Extract contents for the sub-matrix.
            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < result.ColumnCount; j++)
                    result[i, j] = this[i + startRowIndex, j + startColumnIndex];
            }

            // Return the sub-matrix.
            return result;
        }

        /// <summary>
        /// Returns a Vector that contains all the components of this Matrix's diagonal.
        /// </summary>
        /// <returns>A Vector that contains all the components of this Matrix's diagonal.</returns>
        public Vector GetDiagonal()
        {
            // Only a square matrix have its diagonal.
            CheckIsSquareMatrix();

            // Initialize the result Vector.
            Vector result = new Vector(this.RowCount);

            // Assign and return.
            for (int i = 0; i < result.Count; i++)
                result[i] = this[i, i];
            return result;
        }

        /// <summary>
        /// Returns a Matrix that inserts a column to this Matrix at right side.
        /// </summary>
        /// <param name="column">The column Vector to insert.</param>
        /// <returns>A Matrix that inserts a column to this Matrix at right side.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="column" /> is null.</exception>
        /// <exception cref="ArgumentException">If the input column vector doesn't have the same length as the row count of this matrix.</exception>
        public Matrix InsertColumn(Vector column)
        {
            // Check the column Vector before processing.
            CheckColumnVector(column);

            // Initialize the result Matrix.
            Matrix result = new Matrix(this.RowCount, this.ColumnCount + 1);

            // Copy the values of this Matrix to the result Matrix.
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i, j] = this[i, j];
            }

            // Copy the values of the column Vector to the result Matrix.
            for (int i = 0; i < column.Count; i++)
                result[i, this.ColumnCount] = column[i];

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Returns a Matrix that removes the specified column from this Matrix.
        /// </summary>
        /// <param name="columnIndex">The index of the column to remove.</param>
        /// <returns>A Matrix that removes the specified column from this Matrix.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the input column index is out of the range of this Matrix.</exception>
        public Matrix RemoveColumn(int columnIndex)
        {
            // Check the column index before processing.
            CheckColumnIndex(columnIndex);

            // Initialize the result Matrix.
            Matrix result = new Matrix(this.RowCount, this.ColumnCount - 1);

            // Copy the sub-matrix on the left of the specified column.
            for (int j = 0; j < columnIndex; j++)
            {
                for (int i = 0; i < result.RowCount; i++)
                    result[i, j] = this[i, j];
            }

            // Copy the sub-matrix on the right of the specified column.
            for (int j = columnIndex + 1; j < this.ColumnCount; j++)
            {
                for (int i = 0; i < result.RowCount; i++)
                    result[i, j - 1] = this[i, j];
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Copies the values of the given column Vector to the specified column.
        /// </summary>
        /// <param name="column">The column Vector that contains the new values that the specified column should have.</param>
        /// <param name="columnIndex">The index of the column to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the input column index is out of the range of this Matrix.</exception>
        /// <exception cref="ArgumentNullException">If the input column Vector is null.</exception>
        /// <exception cref="ArgumentException">If the input column Vector doesn't have the same length as the row count of this matrix.</exception>
        public void SetColumn(Vector column, int columnIndex)
        {
            // Check the column index and column Vector before processing.
            CheckColumnIndex(columnIndex);
            CheckColumnVector(column);

            // Assignment.
            for (int i = 0; i < this.RowCount; i++)
                this[i, columnIndex] = column[i];
        }

        /// <summary>
        /// Copies the values of the given double array to the specified column.
        /// </summary>
        /// <param name="column">The double array that contains the new values that the sepcified column should have.</param>
        /// <param name="columnIndex">The index of the column to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the input column index is out of the range of this Matrix.</exception>
        /// <exception cref="ArgumentNullException">If the input double array is null.</exception>
        /// <exception cref="ArgumentException">If the input double array doesn't have the same length as the row count of this matrix.</exception>
        public void SetColumn(double[] column, int columnIndex)
        {
            // Check the column index before processing.
            CheckColumnIndex(columnIndex);

            // Throw exception if the given array is not suitable for the assgnment operation.
            if (column == null)
                throw new ArgumentNullException("column", "The column to insert is null.");
            if (column.Length != this.RowCount)
                throw new ArgumentException("The length of the vector is not equal to the row count of this Matrix.");

            // Assignment.
            for (int i = 0; i < this.RowCount; i++)
                this[i, columnIndex] = column[i];
        }

        /// <summary>
        /// Returns a Matrix that inserts a row to this Matrix at the bottom.
        /// </summary>
        /// <param name="row">The row Vector to insert.</param>
        /// <returns>A Matrix that inserts a row to this Matrix at the bottom.</returns>
        /// <exception cref="ArgumentNullException">If the input row vector is null.</exception>
        /// <exception cref="ArgumentException">If the input row vector doesn't have the same length as the row count of this matrix.</exception>
        public Matrix InsertRow(Vector row)
        {
            // Check the row Vector before processing.
            CheckRowVector(row);

            // Initialize the result Matrix.
            Matrix result = new Matrix(this.RowCount + 1, this.ColumnCount);

            // Copy the values of this Matrix to the result Matrix.
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i, j] = this[i, j];
            }

            // Copy the values of the row Vector to the result Matrix.
            for (int j = 0; j < row.Count; j++)
                result[this.RowCount, j] = row[j];

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Returns a Matrix that removes the specified row from this Matrix.
        /// </summary>
        /// <param name="columnIndex">The index of the row to remove.</param>
        /// <returns>A Matrix that removes the specified row from this Matrix.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the input row index is out of the range of this Matrix.</exception>
        public Matrix RemoveRow(int rowIndex)
        {
            // Check the column index before processing.
            CheckRowIndex(rowIndex);

            // Initialize the result Matrix.
            Matrix result = new Matrix(this.RowCount - 1, this.ColumnCount);

            // Copy the sub-matrix on the above the specified column.
            for (int i = 0; i < rowIndex; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i, j] = this[i, j];
            }

            // Copy the sub-matrix on the below the specified column.
            for (int i = rowIndex + 1; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i - 1, j] = this[i, j];
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Copies the values of the given row Vector to the specified row.
        /// </summary>
        /// <param name="row">The row Vector that contains the new values that the specified row should have.</param>
        /// <param name="rowIndex">The index of the row to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the input row index is out of the range of this Matrix.</exception>
        /// <exception cref="ArgumentNullException">If the input row Vector is null.</exception>
        /// <exception cref="ArgumentException">If the input row Vector doesn't have the same length as the row count of this matrix.</exception>
        public void SetRow(Vector row, int rowIndex)
        {
            // Check the column index and column Vector before processing.
            CheckRowIndex(rowIndex);
            CheckRowVector(row);

            // Assignment.
            for (int j = 0; j < this.ColumnCount; j++)
                this[rowIndex, j] = row[j];
        }

        /// <summary>
        /// Copies the values of the given double array to the specified row.
        /// </summary>
        /// <param name="row">The double array that contains the new values that the sepcified row should have.</param>
        /// <param name="rowIndex">The index of the row to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the input row index is out of the range of this Matrix.</exception>
        /// <exception cref="ArgumentNullException">If the input double array is null.</exception>
        /// <exception cref="ArgumentException">If the input double array doesn't have the same length as the column count of this matrix.</exception>
        public void SetRow(double[] row, int rowIndex)
        {
            // Check the row index before processing.
            CheckRowIndex(rowIndex);

            // Throw exception if the given array is not suitable for the assgnment operation.
            if (row == null)
                throw new ArgumentNullException("row", "The row to insert is null.");
            if (row.Length != this.ColumnCount)
                throw new ArgumentException("The length of the vector is not equal to the column count of this Matrix.");

            // Assignment.
            for (int j = 0; j < this.ColumnCount; j++)
                this[rowIndex, j] = row[j];
        }

        /// <summary>
        /// Copies the values of a given Matrix into a region in this matrix.
        /// </summary>
        /// <param name="subMatrix">The Matrix that contains the new values that the sub-region should have.</param>
        /// <param name="startRowIndex">The start row index of the sub-region.</param>
        /// <param name="startColumnIndex">The start column index of the sub-region.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="startRowIndex" /> or <paramref name="startColumnIndex" /> is out of the range of this Matrix.
        /// If the sub-region is smaller than <paramref name="subMatrix" />
        /// </exception>
        public void SetSubMatrix(Matrix subMatrix, int startRowIndex, int startColumnIndex)
        {
            // Check start row index and stasrt column index.
            CheckRowIndex(startColumnIndex);
            CheckColumnIndex(startColumnIndex);

            // Check whether the size of the sub-region is smaller than the given Matrix.
            if (this.RowCount < subMatrix.RowCount + startRowIndex)
                throw new ArgumentOutOfRangeException("subMatrix", "The row count of subMatrix is larger than the remaining capacity of this Matrix.");
            if (this.ColumnCount < subMatrix.ColumnCount + startColumnIndex)
                throw new ArgumentOutOfRangeException("subMatrix", "The column count of subMatrix is larger than the remaining capacity of this Matrix.");

            // Assignment.
            for (int i = 0; i < subMatrix.RowCount; i++)
            {
                for (int j = 0; j < subMatrix.ColumnCount; j++)
                    this[i + startRowIndex, j + startColumnIndex] = subMatrix[i, j];
            }
        }

        /// <summary>
        /// Sets the value of the diagonal of this Matrix using the sepcified Vector.
        /// </summary>
        /// <param name="diagonal">The Vector that contains the new values that the diagonal of this Matrix should have.</param>
        /// <exception cref="ArgumentException">
        /// If this Matrix is not a square matrix.
        /// If The length of the input vector is not equal to the size of the diagonal of this Matrix.
        /// </exception>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public void SetDiagonal(Vector diagonal)
        {
            // Check whether this Matrix is square matrix.
            CheckIsSquareMatrix();

            // Check the diagonal Vector.
            if (diagonal == null)
                throw new ArgumentNullException("diagonal", "The input Vector must not be null.");
            if (this.RowCount != diagonal.Count)
                throw new ArgumentException("The length of the input vector is not equal to the size of the diagonal of this Matrix.");

            // Assignment.
            for (int i = 0; i < diagonal.Count; i++)
                this[i, i] = diagonal[i];
        }

        /// <summary>
        /// Sets the value of the diagonal of this Matrix using the sepcified Vector.
        /// </summary>
        /// <param name="diagonal">The double array that contains the new values that the diagonal of this Matrix should have.</param>
        /// <exception cref="ArgumentException">
        /// If this Matrix is not a square matrix.
        /// If The length of the input double array is not equal to the size of the diagonal of this Matrix.
        /// </exception>
        /// <exception cref="ArgumentNullException">If the input double array is null.</exception>
        public void SetDiagonal(params double[] diagonal)
        {
            // Check whether this Matrix is square matrix.
            CheckIsSquareMatrix();

            // Check the diagonal Vector.
            if (diagonal == null)
                throw new ArgumentNullException("diagonal", "The input input doubl must not be null.");
            if (this.RowCount != diagonal.Length)
                throw new ArgumentException("The length of the input Vector is not equal to the size of the diagonal of this Matrix.");

            // Assignment.
            for (int i = 0; i < diagonal.Length; i++)
                this[i, i] = diagonal[i];
        }

        /// <summary>
        /// Returns the transpose of this matrix.
        /// </summary>
        /// <returns>The transpose of this matrix.</returns>
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

        /// <summary>
        /// Concatenates this matrix with the given matrix.
        /// </summary>
        /// <param name="right">The Matrix to concatenate.</param>
        /// <returns>A Matrix that whose left part is this Matrix, right part is the given Matrix.</returns>
        /// <exception cref="ArgumentNullException">If the Matrix to append is null.</exception>
        /// <exception cref="ArgumentException">If the row count of the input Matrix is not equal to the the row count of this Matrix.</exception>
        public Matrix Append(Matrix right)
        {
            // Check the given Matrix.
            if (right == null)
                throw new ArgumentNullException("right", "The Matrix to append is null.");
            if (this.RowCount != right.RowCount)
                throw new ArgumentException("The row count of the input Matrix is not equal to the the row count of this Matrix.");

            // Initialize the result Matrix with.
            Matrix result = new Matrix(this.RowCount, this.ColumnCount + right.ColumnCount);

            // Copy the left half.
            for (int j = 0; j < this.ColumnCount; j++)
            {
                for (int i = 0; i < this.RowCount; i++)
                    result[i, j] = this[i, j];
            }

            // Copy the right half.
            for (int j = 0; j < right.ColumnCount; j++)
            {
                for (int i = 0; i < right.RowCount; i++)
                    result[i, j + this.ColumnCount] = right[i, j];
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Stacks this matrix on top of the given matrix and places the result into the result matrix.
        /// </summary>
        /// <param name="lower">The matrix to stack this matrix upon.</param>
        /// <exception cref="ArgumentNullException">If the Matrix to stack is null.</exception>
        /// <exception cref="ArgumentException">If the column count of the input Matrix is not equal to the the column count of this Matrix.</exception>
        public Matrix Stack(Matrix lower)
        {
            // Check the given Matrix.
            if (lower == null)
                throw new ArgumentNullException("lower", "The Matrix to stack is null.");
            if (this.ColumnCount != lower.ColumnCount)
                throw new ArgumentException("The column count of the input Matrix is not equal to the column count of this Matrix.");

            // Initialize the result Matrix with.
            Matrix result = new Matrix(this.RowCount + lower.RowCount, this.ColumnCount);

            // Copy the left half.
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    result[i, j] = this[i, j];
            }

            // Copy the right half.
            for (int i = 0; i < lower.RowCount; i++)
            {
                for (int j = 0; j < lower.ColumnCount; j++)
                    result[i + this.RowCount, j] = lower[i, j];
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Evaluates whether this matrix is hermitian (conjugate symmetric).
        /// </summary>
        /// <param name="epsilon">The threshold to determine whether the difference of 2 value is 0.</param>
        /// <returns>True if this Matrix is symmetric, otherwise, false.</returns>
        /// <remarks>
        /// This method doesn't throw exception.
        /// </remarks>
        public bool IsSymmetric(double epsilon = 1e-5)
        {
            // Check whether this Matrix is a square matrix.
            if (!IsSquareMatrix)
                return false;

            // Check if this Matrix is symmetrix.
            for (int i = 1; i < this.RowCount; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Math.Abs(this[i, j] - this[j, i]) >= epsilon)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns this matrix as array of double[,].
        /// The returned array will be independent from this matrix.
        /// A new memory block will be allocated for the arrays.
        /// </summary>
        /// <returns>This matrix as array of double[,].</returns>
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
        /// Returns the matrix's components as an array with the data laid out column by column (column major).
        /// The returned array will be independent from this matrix.
        /// A new memory block will be allocated for the array.
        /// </summary>
        /// <example>
        /// 1, 2, 3
        /// 4, 5, 6  will be returned as  1, 4, 7, 2, 5, 8, 3, 6, 9
        /// 7, 8, 9
        /// </example>
        /// <returns>An array containing the matrix's components.</returns>
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
        /// Returns the matrix's components as an array with the data laid row by row (row major).
        /// The returned array will be independent from this matrix.
        /// A new memory block will be allocated for the array.
        /// </summary>
        /// <example><pre>
        /// 1, 2, 3
        /// 4, 5, 6  will be returned as  1, 2, 3, 4, 5, 6, 7, 8, 9
        /// 7, 8, 9
        /// </pre></example>
        /// <returns>An array containing the matrix's components.</returns>
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
                    result[i][j] = this[i, j];
            }

            return result;
        }

        /// <summary>
        /// Returns this matrix as array of column arrays.
        /// The returned arrays will be independent from this matrix.
        /// A new memory block will be allocated for the arrays.
        /// </summary>
        /// <returns>This matrix as array of double[,].</returns>
        public double[][] ToColumnArrays()
        {
            double[][] result = new double[this.ColumnCount][];
            for (int j = 0; j < this.ColumnCount; j++)
            {
                result[j] = new double[this.RowCount];
                for (int i = 0; i < this.RowCount; i++)
                    result[j][i] = this[i, j];
            }

            return result;
        }

        /// <summary>
        /// Returns an array of Vectors whose elements are correspond to the rows vector of this Matrix.
        /// </summary>
        /// <returns>an array of Vectors whose elements are correspond to the rows vector of this Matrix.</returns>
        public Vector[] ToRowVectors()
        {
            Vector[] rows = new Vector[this.RowCount];
            for (int i = 0; i < this.RowCount; i++)
                rows[i] = new Vector(matrix[i]);

            return rows;
        }

        /// <summary>
        /// Returns an array of Vectors whose elements are correspond to the column vector of this Matrix.
        /// </summary>
        /// <returns>an array of Vectors whose elements are correspond to the column vector of this Matrix.</returns>
        public Vector[] ToColumnVectors()
        {
            // Initialize the columns vector.
            Vector[] columns = new Vector[this.ColumnCount];

            // Copy values and return.
            double[][] columnArrays = ToColumnArrays();
            for (int i = 0; i < columns.Length; i++)
                columns[i] = new Vector(columnArrays[i]);
            return columns;
        }

        /// <summary>
        /// Returns a Matrix whose i-th component equals matrix[i, j] + scalar, where matrix and scalar are input arguments.
        /// </summary>
        /// <param name="matrix">The input Matrix.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Matrix whose i-th component equals matrix[i, j] + scalar, where matrix and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If input Matrix is null.</exception>
        public static Matrix operator +(Matrix matrix, double scalar)
        {
            // Check the input Matrix.
            if (matrix == null)
                throw new ArgumentNullException("matrix", "The input Matrix is null.");

            // Make a deep copy of this Matrix.
            Matrix result = matrix.Clone();

            // Add operation.
            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < result.ColumnCount; j++)
                    result[i, j] += scalar;
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals matrix[i, j] + scalar, where matrix and scalar are input arguments.
        /// </summary>
        /// <param name="matrix">The input Matrix.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Vector whose i-th component equals matrix[i, j] + scalar, where matrix and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If input Matrix is null.</exception>
        public static Matrix operator +(double scalar, Matrix matrix)
        {
            return matrix + scalar;
        }

        /// <summary>
        /// Returns a Matrix whose i-th component equals matrix1[i, j] + matrix2[i, j], where matrix1 and matrix2 are input arguments.
        /// </summary>
        /// <param name="matrix1">A Matrix.</param>
        /// <param name="matrix2">The other Matrix.</param>
        /// <returns>a Matrix whose i-th component equals matrix1[i, j] + matrix2[i, j], where matrix1 and matrix2 are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If one of the input Matrix is null.</exception>
        /// <exception cref="ArgumentException">If input matrices don't have the same shape.</exception>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            // Check 2 input matrices.
            CheckMatrices(matrix1, matrix2);

            // Make a deep copy of this Matrix.
            Matrix result = matrix1.Clone();

            // Add operation.
            for (int i = 0; i < matrix1.RowCount; i++)
            {
                for (int j = 0; j < matrix1.ColumnCount; j++)
                    result[i, j] += matrix2[i, j];
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals matrix[i, j] - scalar, where matrix and scalar are input arguments.
        /// </summary>
        /// <param name="matrix">The input Matrix.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Vector whose i-th component equals matrix[i, j] - scalar, where matrix and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If input Matrix is null.</exception>
        public static Matrix operator -(Matrix matrix, double scalar)
        {
            // Check the input Matrix.
            if (matrix == null)
                throw new ArgumentNullException("matrix", "The input Matrix is null.");

            // Make a deep copy of this Matrix.
            Matrix result = matrix.Clone();

            // Subtraction operation.
            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < result.ColumnCount; j++)
                    result[i, j] -= scalar;
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals scalar - matrix[i, j], where matrix and scalar are input arguments.
        /// </summary>
        /// <param name="matrix">The input Matrix.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Vector whose i-th component equals scalar - matrix[i, j], where matrix and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If input Matrix is null.</exception>
        public static Matrix operator -(double scalar, Matrix matrix)
        {
            // Check the input Matrix.
            if (matrix == null)
                throw new ArgumentNullException("matrix", "The input Matrix is null.");

            // Make a deep copy of this Matrix.
            Matrix result = matrix.Clone();

            // Subtraction operation.
            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < result.ColumnCount; j++)
                    result[i, j] = scalar - result[i, j];
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Returns a Matrix whose i-th component equals matrix1[i, j] + matrix2[i, j], where matrix1 and matrix2 are input arguments.
        /// </summary>
        /// <param name="matrix1">A Matrix.</param>
        /// <param name="matrix2">The other Matrix.</param>
        /// <returns>a Matrix whose i-th component equals matrix1[i, j] + matrix2[i, j], where matrix1 and matrix2 are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If one of the input Matrix is null.</exception>
        /// <exception cref="ArgumentException">If input matrices don't have the same shape.</exception>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            // Check 2 input matrices.
            CheckMatrices(matrix1, matrix2);

            // Make a deep copy of this Matrix.
            Matrix result = matrix1.Clone();

            // Subtraction operation.
            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < matrix2.RowCount; j++)
                    result[i, j] -= matrix2[i, j];
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Returns a Matrix whose i-th component equals matrix[i, j] * scalar, where matrix and scalar are input arguments.
        /// </summary>
        /// <param name="matrix">The input Matrix.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Matrix whose i-th component equals matrix[i, j] * scalar, where matrix and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If input Matrix is null.</exception>
        public static Matrix operator *(Matrix matrix, double scalar)
        {
            // Check the input Matrix.
            if (matrix == null)
                throw new ArgumentNullException("matrix", "The input Matrix is null.");

            // Make a deep copy of this Matrix.
            Matrix result = matrix.Clone();

            // Multiplication operation.
            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < result.ColumnCount; j++)
                    result[i, j] *= scalar;
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Returns a Matrix whose i-th component equals matrix[i, j] * scalar, where matrix and scalar are input arguments.
        /// </summary>
        /// <param name="matrix">The input Matrix.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Matrix whose i-th component equals matrix[i, j] * scalar, where matrix and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If input Matrix is null.</exception>
        public static Matrix operator *(double scalar, Matrix matrix)
        {
            return matrix * scalar;
        }

        /// <summary>
        /// Returns a column Vector that is the product of the input Matrix and the input column Vector (matrix * rowVector).
        /// </summary>
        /// <param name="matrix">The Matrix of this multiplication operation.</param>
        /// <param name="columnVector">The column vector of this multiplication.</param>
        /// <returns>A column Vector that is the product of the input Matrix and the input column Vector (matrix * rowVector).</returns>
        /// <exception cref="ArgumentNullException">If the input Matrix of Vector is null.</exception>
        /// <exception cref="ArgumentException">If the column count or the matrix and the count of the vector are not equal.</exception>
        public static Vector operator *(Matrix matrix, Vector columnVector)
        {
            // Check whether one of them is null.
            if (matrix == null)
                throw new ArgumentNullException("matrix", "The input Matrix is null.");
            if (columnVector == null)
                throw new ArgumentNullException("columnVector", "The input Vector is null.");

            // Check whether the column count of the matrix and the count of the vector must be equal
            if (matrix.ColumnCount != columnVector.Count)
                throw new ArgumentException("The column count of the matrix and the count of the vector must be equal.");

            // Get rows of the given matrix.
            Vector[] rows = matrix.matrix;

            // Initialize the result Vector.
            Vector result = new Vector(matrix.RowCount);

            // Multiplication operation.
            for (int i = 0; i < result.Count; i++)
                result[i] = rows[i] * columnVector;

            // Return the result Vector.
            return result;
        }

        /// <summary>
        /// Returns a row Vector that is the product of the input row vector and the input Matrix (rowVector * matrix).
        /// </summary>
        /// <param name="rowVector">The row vector of this multiplication.</param>
        /// <param name="matrix">The matrix of this multiplication.</param>
        /// <returns>A row Vector that is the product of the input row vector and the input Matrix (rowVector * matrix).</returns>
        /// <exception cref="ArgumentNullException">If the input Matrix or Vector is null.</exception>
        /// <exception cref="ArgumentException">If the row count of the matrix and the count of the vector are not equal.</exception>
        public static Vector operator *(Vector rowVector, Matrix matrix)
        {
            // Check whether one of them is null.
            if (matrix == null)
                throw new ArgumentNullException("matrix", "The input Matrix is null.");
            if (rowVector == null)
                throw new ArgumentNullException("rowVector", "The input Vector is null.");

            // Check whether the column count of the matrix and the count of the vector must be equal
            if (matrix.RowCount != rowVector.Count)
                throw new ArgumentException("The row count of the matrix and the count of the vector must be equal.");

            // Get columns of the given Matrix.
            Vector[] columns = matrix.ToColumnVectors();

            // Initialize the result Vector.
            Vector result = new Vector(matrix.ColumnCount);

            // Multiplication operation.
            for (int i = 0; i < result.Count; i++)
                result[i] = rowVector * columns[i];

            // Return the result Vector.
            return result;
        }

        /// <summary>
        /// Returns a Matrix that is the product of 2 input matrices.
        /// </summary>
        /// <param name="matrixLeft">The Matrix on the left of the *.</param>
        /// <param name="matrixRight">The Matrix on the right of the *.</param>
        /// <returns>A Matrix that is the product of 2 input matrices.</returns>
        /// <exception cref="ArgumentNullException">If one of the input matrices is null.</exception>
        /// <exception cref="ArgumentException">If the column count of left matrix and the row count of right matrix are not equal.</exception>
        public static Matrix operator *(Matrix matrixLeft, Matrix matrixRight)
        {
            // Check whether one of them is null.
            if (matrixLeft == null)
                throw new ArgumentNullException("matrixLeft", "matrixLeft is null.");
            if (matrixRight == null)
                throw new ArgumentNullException("matrixRight", "matrixRight is null.");

            // Check the inner dimension of 2 matrices.
            if (matrixLeft.ColumnCount != matrixRight.RowCount)
                throw new ArgumentException("The column count of left matrix and the row count of right matrix must be equal.");

            // Initialize the result Matrix.
            Matrix result = new Matrix(matrixLeft.RowCount, matrixRight.ColumnCount);

            // Get the row and column vectors.
            Vector[] rows = matrixLeft.matrix;
            Vector[] columns = matrixRight.ToColumnVectors();

            // Matrix multiplication.
            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < result.ColumnCount; j++)
                    result[i, j] = rows[i] * columns[j];
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals matrix[i, j] / scalar, where matrix and scalar are input arguments.
        /// </summary>
        /// <param name="matrix">The input Matrix.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Vector whose i-th component equals matrix[i, j] / scalar, where matrix and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If input Matrix is null.</exception>
        public static Matrix operator /(Matrix matrix, double scalar)
        {
            // Check the input Matrix.
            if (matrix == null)
                throw new ArgumentNullException("matrix", "The input Matrix is null.");

            // Make a deep copy of this Matrix.
            Matrix result = matrix.Clone();

            // Division operation.
            for (int i = 0; i < result.RowCount; i++)
            {
                for (int j = 0; j < result.ColumnCount; j++)
                    result[i, j] /= scalar;
            }

            // Return the result Matrix.
            return result;
        }

        /// <summary>
        /// Throws exception if 2 input matrices don't have the same shape.
        /// </summary>
        /// <param name="matrix1">A Matrix.</param>
        /// <param name="matrix2">The other Matrix.</param>
        /// <exception cref="ArgumentException">If input matrices don't have the same shape.</exception>
        private static void HaveSameShape(Matrix matrix1, Matrix matrix2)
        {
            if ((matrix1.RowCount != matrix2.RowCount) || (matrix1.ColumnCount != matrix2.ColumnCount))
                throw new ArgumentException("Input matrices don't have the same shape.");
        }

        /// <summary>
        /// Throws exception if 2 input matrices don't have the same shape.
        /// </summary>
        /// <param name="matrix1">A Matrix.</param>
        /// <param name="matrix2">The other Matrix.</param>
        /// <exception cref="ArgumentNullException">If one of the input Matrix is null.</exception>
        /// <exception cref="ArgumentException">If input matrices don't have the same shape.</exception>
        private static void CheckMatrices(Matrix matrix1, Matrix matrix2)
        {
            // Check if one of the Matrix is null.
            if (matrix1 == null)
                throw new ArgumentNullException("matrix1", "matrix1 is null.");
            if (matrix2 == null)
                throw new ArgumentNullException("matrix2", "matrix2 is null");

            // Check whether they have the same shape.
            HaveSameShape(matrix1, matrix2);
        }

        /// <summary>
        /// Returns the string representation of this Matrix. Each row of this Matrix will be a line in the string, every 2 adjacent components in the same line will be separated by white space.
        /// </summary>
        /// <returns>The string representation of this Matrix. Each row of this Matrix will be a line in the string, every 2 adjacent components in the same line will be separated by white space.</returns>
        public override string ToString()
        {
            // Use StringBuilder to accelerate.
            StringBuilder s = new StringBuilder();

            // Gether contents for the StringBuilder.
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                    s.Append(string.Format("{0,4} ", this[i, j]));
                s.Append(Environment.NewLine);
            }

            // Remove the line breaks at the end of this StringBuilder.
            s.Remove(s.Length - 1, 1);

            // Return the string representation of this Matrix.
            return s.ToString();
        }
    }
}