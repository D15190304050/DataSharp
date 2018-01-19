using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    /// <summary>
    /// The Matrix class represents a matrix in mathematics. This class provides some corresponding operation.
    /// </summary>
    [Serializable]
    public class VectorBasedMatrix
    {
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

            int columnCount = vectors[0].Length;
            for (int i = 1; i < vectors.Length; i++)
            {
                Vector v = vectors[i];
                if (v == null)
                    return false;
                if (v.Length != columnCount)
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
        public VectorBasedMatrix(double[][] matrix)
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
                this.matrix[i] = new Vector(this.ColumnCount);
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
        public VectorBasedMatrix(double[,] matrix)
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


    }
}
