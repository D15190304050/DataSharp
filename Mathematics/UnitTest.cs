using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public static class UnitTest
    {
        /// <summary>
        /// Unit test method for the Vector class.
        /// </summary>
        public static void VectorTest()
        {
            double[] values = { 1.1, 2.2, 3.3, 4.4, 5.5 };

            Vector v1 = new Vector(values);
            Vector v2 = new Vector(v1);
            v2 = v2 - 1;
            Console.WriteLine($"v1 = {v1}");
            Console.WriteLine($"v2 = {v2}");
            Console.WriteLine();

            Console.WriteLine($"The sub-vector with range [1, 3] of v1 is {v1.GetSubVector(1, 3)}\n");

            Console.WriteLine($"v1 + 3 = {v1 + 3}");
            Console.WriteLine($"5 + v1 = {5 + v1}");
            Console.WriteLine($"v1 + v2 = {v1 + v2}");
            Console.WriteLine();

            Console.WriteLine($"v1 - 1 = {v1 - 1}");
            Console.WriteLine($"5 - v1 = {5 - v1}");
            Console.WriteLine($"v1 - v2 = {v1 - v2}");
            Console.WriteLine();

            Console.WriteLine($"v1 * 2 = {v1 * 2}");
            Console.WriteLine($"3 * v1 = {3 * v1}");
            Console.WriteLine($"v1 * v2 = {v1 * v2}");
            Console.WriteLine();

            Console.WriteLine($"v1 / 0.5 = {v1 / 0.5}");
            Console.WriteLine();

            Vector v3 = new Vector(new double[] { 1, 1 });
            Console.WriteLine($"v3 = {v3}");
            Console.WriteLine($"After normalization, v3 = {v3.Normalize()}");
            Console.WriteLine();

            Console.WriteLine($"Sum of v1 = {v1.SumComponents()}");
            Console.WriteLine($"Sum of squared components of v1 = {v1.SumSquaredComponents()}");

            Vector v4 = new Vector(1, 2, 3, 4, 5);
            Vector v5 = new Vector(2, 7, 5);
            Console.WriteLine($"Vector v4 = {v4}");
            Console.WriteLine($"Vector v5 = {v5}");
            Console.WriteLine($"The convolution of v4 * V5 = {Vector.ComputeConvolution(v4, v5)}");
            Console.WriteLine($"The correlation of v4 * v5 = {Vector.ComputeCorrelation(v4, v5)}");

            Console.WriteLine($"The element-wise product of v1 .* v4 = {Vector.ElementWiseMultiplication(v1,v4)}");

            Console.WriteLine($"Tile v5 to a 3 * 4 matrix where v5 is a row vector:\n{v5.Tile(4, 1)}");
            Console.WriteLine($"Tile v5 to a 6 * 2 matrix where v5 is a column vector:\n{v5.Tile(2, 2, false)}");
        }

        /// <summary>
        /// Unit test method for the Matrix class.
        /// </summary>
        public static void MatrixTest()
        {
            // Initialize 2 matrices by static method.
            Matrix m1 = Matrix.Ones(5, 5);
            Matrix m2 = Matrix.Zeros(3, 4);
            Console.WriteLine($"A 5*5 matrix whose values are all 1.\n{m1}");
            Console.WriteLine($"A 3*4 matrix whose values are all 0.\n{m2}");

            Console.WriteLine("Clone a Matrix.");
            Matrix m3 = m1.Clone();
            Console.WriteLine($"A colne of m1.\n{m3}");

            Console.WriteLine("Initialize a Matrix by a 2-D double array.");
            double[][] valuesForMatrix4 = new double[5][];
            valuesForMatrix4[0] = new double[] { 1, 2, 3, 4, 5 };
            valuesForMatrix4[1] = new double[] { 6, 7, 8, 9, 10 };
            valuesForMatrix4[2] = new double[] { 11, 12, 13, 14, 15 };
            valuesForMatrix4[3] = new double[] { 16, 17, 18, 19, 20 };
            valuesForMatrix4[4] = new double[] { 21, 22, 23, 24, 25 };
            Matrix m4 = new Matrix(valuesForMatrix4);
            Console.WriteLine($"Matrix m4 =\n{m4}");

            Matrix m5 = m4.GetSubMatrix(0, 0, 4, 3);
            Console.WriteLine($"m5 is sub matrix of m4 with range [0, 0, 4, 3] =\n{m5}");

            Matrix m6 = m1.GetSubMatrix(0, 0, 3, 4);
            Console.WriteLine($"m5 is sub matrix of m1 with range [0, 0, 3, 4] =\n{m6}");

            Console.WriteLine("Test for operator +");
            Console.WriteLine($"m1 + 5 =\n{m1 + 5}");
            Console.WriteLine($"7 + m1 =\n{7 + m1}");
            Console.WriteLine($"m1 + m4 =\n{m1 + m4}");
            Console.WriteLine();

            Console.WriteLine("Test for operator -");
            Console.WriteLine($"m4 - 4 =\n{m4 - 4}");
            Console.WriteLine($"30 - m4 =\n{30 - m4}");
            Console.WriteLine($"m4 - m1 =\n{m4 - m1}");
            Console.WriteLine();

            Console.WriteLine("Test for operator /");
            Console.WriteLine($"m4 / 0.5 =\n{m4 / 0.5}");
            Console.WriteLine();

            Console.WriteLine("Test for operator *");
            Vector v1 = new Vector(5);
            for (int i = 0; i < v1.Count; i++)
                v1[i] = 1;
            Console.WriteLine($"A vector will be used here : v1 = {v1}.");
            Console.WriteLine($"m1 * 2 =\n{m1 * 2}");
            Console.WriteLine($"3 * m1 =\n{3 * m1}");
            Console.WriteLine($"m1 * m5 =\n{m1 * m5}");
            Console.WriteLine($"m6 * m5 =\n{m6 * m5}");
            Console.WriteLine($"m4 * v1 = {m4 * v1}");

            Console.WriteLine("Test for concatenation operation.");
            Console.WriteLine($"m1.Append(m4) =\n{m1.Append(m4)}");
            Console.WriteLine($"m1.Stack(m4) =\n{m1.Stack(m4)}");

            m5.ClearColumn(1);
            Console.WriteLine($"After clearing column 1, m5 =\n{m5}");
            m5.ClearRow(3);
            Console.WriteLine($"After clearing column 3, m5 =\n{m5}");

            m1.ClearRows(1, 2);
            Console.WriteLine($"After clearing row 1 and 2, m1 =\n{m1}");
            m1.ClearColumns(3, 4);
            Console.WriteLine($"After clearing column 3 and 4, m1 =\n{m1}");

            m1.Clear();
            Console.WriteLine($"Clear m1, we get m1 =\n{m1}");

            m3.ClearSubMatrix(1, 1, 2, 2);
            Console.WriteLine($"After clearing range [1, 1, 2, 2], m3 =\n{m3}");

            m4.CoerceZero(5);
            Console.WriteLine($"After coercing all values less than 5 to be 0, m4 =\n{m4}");
            m4.CoerceZero(x => x > 20);
            Console.WriteLine($"After coercing all values greater than 20 to be 0, m4 =\n{m4}");

            Console.WriteLine($"Row 1 of m4 = {m4.GetRow(1)}");
            Console.WriteLine($"Column 2 of m4 = {m4.GetColumn(2)}");
            Console.WriteLine($"Row 1 of m4 with range [1, 3] = {m4.GetSubRow(1, 1, 3)}");
            Console.WriteLine($"Column 2 of m4 with range [1, 3] = {m4.GetSubColumn(2, 1, 3)}");
            Console.WriteLine($"Diagonal of m4 = {m4.GetDiagonal()}");

            Console.WriteLine($"Upper triangular matrix of m4 =\n{m4.UpperTriangular()}");
            Console.WriteLine($"Lower triangular matrix of m4 =\n{m4.LowerTriangular()}");
            Console.WriteLine($"Transpose of m4 =\n{m4.Transpose()}");

            Console.WriteLine($"m5 now =\n{m5}");
            Console.WriteLine($"The matrix that removes column2 from m5 =\n{m5.RemoveColumn(2)}");
            Console.WriteLine($"The matrix that removes column2 from m5 =\n{m5.RemoveRow(2)}");
            Console.WriteLine($"The matrix that insert v1 as a column to m5 =\n{m5 = m5.InsertColumn(v1)}");
            Console.WriteLine($"m6 now =\n{m6}");
            Console.WriteLine($"The matrix that insert v1 as a row to m6 is =\n{m6.InsertRow(v1)}");

            m5.SetColumn(v1, 1);
            Console.WriteLine($"After setting column 1 of m5 using v1, we get m5 =\n{m5}");
            m1.SetRow(v1, 1);
            Console.WriteLine($"After setting row 1 of m5 using v1, we get m1 =\n{m1}");

            Console.WriteLine($"m1 is symmetric = {m1.IsSymmetric()}");
            Matrix m7 = Matrix.Ones(3, 3);
            Console.WriteLine($"m7 =\n{m7}");
            Console.WriteLine($"m7 is symmetric = {m7.IsSymmetric()}");
            m7.SetDiagonal(2, 3, 4);
            Console.WriteLine($"After setting diagonal, m7 =\n{m7}\n m7 is symmetric = {m7.IsSymmetric()}");

            m7.SetColumn(new Vector(7, 8, 9), 0);
            Console.WriteLine($"m7 now is: \n{m7}");
            Console.WriteLine("The rows of m7 is:");
            PrintArray(m7.ToRowArrays());
            Console.WriteLine("The columns of m7 is:");
            PrintArray(m7.ToColumnArrays());
            Console.WriteLine("The row major array of m7 is:");
            PrintArray(m7.ToRowMajorArray());
            Console.WriteLine("The column major array of m7 is:");
            PrintArray(m7.ToColumnMajorArray());
            Console.WriteLine("The array format of m7 is:");
            PrintArray(m7.ToArray());

            double[] row0 = new double[] { 10, 5, 20, 20 };
            double[] row1 = new double[] { 10, 5, 20, 20 };
            double[] row2 = new double[] { 10, 5, 20, 20 };
            double[] row3 = new double[] { 10, 5, 20, 20 };
            double[][] rows = new double[4][] { row0, row1, row2, row3 };
            Matrix m8 = new Matrix(rows);
            row0 = new double[] { -1, 1 };
            row1 = new double[] { 0, 1 };
            rows = new double[2][] { row0, row1 };
            Matrix m9 = new Matrix(rows);
            Console.WriteLine($"Matrix m8 =\n{m8}");
            Console.WriteLine($"Matrix m9 =\n{m9}");
            Console.WriteLine($"Convolution of m8 * m9 =\n{Matrix.ComputeConvolution(m8, m9, 1)}");

            row0 = new double[] { 1, 2, 3, 4, 5 };
            row1 = new double[] { 1, 2, 3, 4, 5 };
            row2 = new double[] { 1, 2, 3, 4, 5 };
            row3 = new double[] { 1, 2, 3, 4, 5 };

            rows = new double[][] { row0, row1, row2, row3 };
            m8 = new Matrix(rows);
            row0 = new double[] { 2, 7 };
            row1 = new double[] { 5, 8 };
            rows = new double[][] { row0, row1 };
            m9 = new Matrix(rows);
            Console.WriteLine($"Matrix m8 =\n{m8}");
            Console.WriteLine($"Matrix m9 =\n{m9}");
            Console.WriteLine($"Correlation of m8 * m9 =\n{Matrix.ComputeCorrelation(m8, m9, 1)}");

            Vector v2 = new Vector(1, 2, 3);
            Vector v3 = new Vector(4, 5, 6);
            Vector v4 = new Vector(7, 8, 9);
            Vector[] vectors = new Vector[] { v2, v3, v4 };
            Matrix m10 = new Matrix(vectors);
            Console.WriteLine($"Matrix constructed from row vectors:\n{m10}");
            m10 = new Matrix(vectors, false);
            Console.WriteLine($"Matrix constructed from column vectors:\n{m10}");

            Vector v5 = new Vector(1, 1, 1, 1);
            Console.WriteLine($"v5 = {v5}");
            Console.WriteLine($"v5 * m9 =\n{v5 * m8}");
        }

        private static void PrintArray(double[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.Write(array[i] + " ");
            Console.WriteLine();
        }

        private static void PrintArray(double[][] array)
        {
            for (int i = 0; i < array.Length; i++)
                PrintArray(array[i]);
        }

        private static void PrintArray(double[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                    Console.Write(array[i, j] + " ");
                Console.WriteLine();
            }
        }
    }
}
