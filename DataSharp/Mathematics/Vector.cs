using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharp.Mathematics
{
    /// <summary>
    /// The Vector class represnets a vector in mathematics that provides vector operations.
    /// </summary>
    [Serializable]
    public class Vector
    {
        // Unsolved:
        // * Slice.

        /// <summary>
        /// The array that contains every components of this Vector.
        /// </summary>
        private readonly double[] vector;

        /// <summary>
        /// Gets or sets the value of the components with index i in the Vector.
        /// </summary>
        /// <param name="i">The index of the specified components.</param>
        /// <returns>The value of the components with index i in the Vector.</returns>
        public double this[int i]
        {
            get { return vector[i]; }
            set { vector[i] = value; }
        }

        /// <summary>
        /// Gets the number of components of this Vector.
        /// </summary>
        public int Count { get { return vector.Length; } }

        /// <summary>
        /// Initializes a Vector with specified number of components.
        /// </summary>
        /// <param name="count">The number of components in this Vector.</param>
        public Vector(int count)
        {
            if (count <= 0)
                throw new ArgumentException("The length of a Vector must be a positive integer.");

            vector = new double[count];
        }

        /// <summary>
        /// Initializes a Vector from a specified double array so that this Vector will have the same values.
        /// </summary>
        /// <remarks>
        /// After initialization, this Vector will have the same value as the input double array.
        /// </remarks>
        /// <param name="vector">The specified double array.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="vector" /> is null.</exception>
        public Vector(params double[] vector)
        {
            if (vector == null)
                throw new ArgumentNullException("The input double array must not be null.");
            this.vector = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
                this.vector[i] = vector[i];
        }

        /// <summary>
        /// Initializes a Vector from a specified double array with specified range [<paramref name="startIndex" />, <paramref name="endIndex" />] so that this Vector will have the same values.
        /// </summary>
        /// <remarks>
        /// After initialization, this Vector will have the same value as the input double array.
        /// </remarks>
        /// <param name="vector">The specified double array.</param>
        /// <param name="startIndex">The start index (inclusive).</param>
        /// <param name="endIndex">The end index (inclusive).</param>
        /// <exception cref="ArgumentException">If <paramref name="startIndex" /> or <paramref name="endIndex" /> is out of range or <paramref name="startIndex" /> is not less than <paramref name="endIndex" />.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="vector" /> is null.</exception>
        public Vector(double[] vector, int startIndex, int endIndex)
        {
            if (vector == null)
                throw new ArgumentNullException("The input double array must not be null.");

            if ((startIndex < 0) || (startIndex >= vector.Length))
                throw new ArgumentException("startIndex is out of range.");
            if ((endIndex < 0) || (startIndex >= vector.Length))
                throw new ArgumentException("endIndex is out of range.");

            if (endIndex <= startIndex)
                throw new ArgumentException("startIndex must be less than endIndex.");

            this.vector = new double[endIndex - startIndex + 1];
            for (int i = 0; i < this.vector.Length; i++)
                this.vector[i] = vector[i + startIndex];
        }

        /// <summary>
        /// Initializes a Vector from another Vector, this operation will make a deep copy of the input Vector.
        /// </summary>
        /// <param name="vector">The other Vector.</param>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public Vector(Vector vector)
        {
            if (vector == null)
                throw new ArgumentNullException(nameof(vector), "The input vector must not be null.");
            this.vector = new double[vector.Count];
            for (int i = 0; i < vector.Count; i++)
                this.vector[i] = vector[i];
        }

        /// <summary>
        /// Get the length of this Vector.
        /// </summary>
        /// <remarks>
        /// Note that the length here is the distance between the origin and the point that can represent this Vector in a vector space.
        /// </remarks>
        /// <returns>The length of this Vector.</returns>
        public double GetLength()
        {
            return Math.Sqrt(this * this);
        }

        /// <summary>
        /// Clear every entry of this Vector.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < this.Count; i++)
                vector[i] = 0;
        }

        /// <summary>
        /// Returns the sub-vector of this Vector with specified range [<paramref name="startIndex" />, <paramref name="endIndex" />].
        /// </summary>
        /// <param name="startIndex">The start index (inclusive).</param>
        /// <param name="endIndex">The end index (inclusive).</param>
        /// <returns>The sub-vector of this Vector with specified range [<paramref name="startIndex" />, <paramref name="endIndex" />].</returns>
        public Vector GetSubVector(int startIndex, int endIndex)
        {
            return new Vector(this.vector, startIndex, endIndex);
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals vector[i] + scalar, where vector and scalar are input arguments.
        /// </summary>
        /// <param name="vector">The input vector.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Vector whose i-th component equals vector[i] + scalar, where vector and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static Vector operator +(Vector vector, double scalar)
        {
            if (vector == null)
                throw new ArgumentNullException("The input vector must not be null.");

            Vector result = new Vector(vector);
            for (int i = 0; i < result.Count; i++)
                result[i] += scalar;

            return result;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals vector[i] - scalar, where vector and scalar are input arguments.
        /// </summary>
        /// <param name="vector">The input vector.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Vector whose i-th component equals vector[i] + scalar, where vector and scalar are input arguments</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static Vector operator +(double scalar, Vector vector)
        {
            return vector + scalar;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals vector1[i] + vector2[i], where vector1 and vector2 are input arguments.
        /// </summary>
        /// <param name="vector1">An input Vector.</param>
        /// <param name="vector2">The other input Vector.</param>
        /// <returns>A Vector whose i-th component equals vector1[i] + vector2[i], where vector1 and vector2 are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            LengthCheck(vector1, vector2);

            int length = vector1.Count;
            Vector result = new Vector(length);
            for (int i = 0; i < length; i++)
                result[i] = vector1[i] + vector2[i];
            return result;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals vector[i] - scalar, where vector and scalar are input arguments.
        /// </summary>
        /// <param name="vector">The input vector.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Vector whose i-th component equals vector[i] - scalar, where vector and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static Vector operator -(Vector vector, double scalar)
        {
            if (vector == null)
                throw new ArgumentNullException("The input vector must not be null.");

            Vector result = new Vector(vector);
            for (int i = 0; i < result.Count; i++)
                result[i] -= scalar;

            return result;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals scalar - vector[i], where vector and scalar are input arguments.
        /// </summary>
        /// <param name="scalar">The input scalar.</param>
        /// <param name="vector">The input vector.</param>
        /// <returns>A Vector whose i-th component equals scalar - vector[i], where vector and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static Vector operator -(double scalar, Vector vector)
        {
            if (vector == null)
                throw new ArgumentNullException("The input vector must not be null.");

            Vector result = new Vector(vector);
            for (int i = 0; i < result.Count; i++)
                result[i] = scalar - vector[i];

            return result;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals vector1[i] - vector2[i], where vector1 and vector2 are input arguments.
        /// </summary>
        /// <param name="vector1">An input Vector.</param>
        /// <param name="vector2">The other input Vector.</param>
        /// <returns>A Vector whose i-th component equals vector1[i] - vector2[i], where vector1 and vector2 are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            LengthCheck(vector1, vector2);

            int length = vector1.Count;
            Vector result = new Vector(length);
            for (int i = 0; i < length; i++)
                result[i] = vector1[i] - vector2[i];
            return result;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals vector[i] * scalar, where vector and scalar are input arguments.
        /// </summary>
        /// <param name="vector">The input vector.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Vector whose i-th component equals vector[i] * scalar, where vector and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static Vector operator *(Vector vector, double scalar)
        {
            if (vector == null)
                throw new ArgumentNullException("The input vector must not be null.");

            Vector result = new Vector(vector);
            for (int i = 0; i < result.Count; i++)
                result[i] *= scalar;

            return result;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals vector[i] * scalar, where vector and scalar are input arguments.
        /// </summary>
        /// <param name="scalar">The input scalar.</param>
        /// <param name="vector">The input vector.</param>
        /// <returns>A Vector whose i-th component equals vector[i] * scalar, where vector and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static Vector operator *(double scalar, Vector vector)
        {
            return vector * scalar;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals vector1[i] * vector2[i], where vector1 and vector2 are input arguments.
        /// </summary>
        /// <param name="vector1">An input Vector.</param>
        /// <param name="vector2">The other input Vector.</param>
        /// <returns>A Vector whose i-th component equals vector1[i] * vector2[i], where vector1 and vector2 are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static double operator *(Vector vector1, Vector vector2)
        {
            LengthCheck(vector1, vector2);

            double sum = 0;
            for (int i = 0; i < vector1.Count; i++)
                sum += vector1[i] * vector2[i];

            return sum;
        }

        /// <summary>
        /// Returns a Vector whose i-th component equals vector[i] / scalar, where vector and scalar are input arguments.
        /// </summary>
        /// <param name="vector">The input vector.</param>
        /// <param name="scalar">The input scalar.</param>
        /// <returns>A Vector whose i-th component equals vector[i] / scalar, where vector and scalar are input arguments.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static Vector operator /(Vector vector, double scalar)
        {
            if (vector == null)
                throw new ArgumentNullException("The input vector must not be null.");
            if (Math.Abs(scalar) < double.Epsilon)
                throw new DivideByZeroException("The scalar must be a non-zero value.");

            Vector result = new Vector(vector);
            for (int i = 0; i < result.Count; i++)
                result[i] /= scalar;

            return result;
        }

        /// <summary>
        /// Returns a Vector whose values are the element-wise product of <paramref name="vector1" /> and <paramref name="vector2" />.
        /// </summary>
        /// <param name="vector1">An input Vector.</param>
        /// <param name="vector2">The other input Vector.</param>
        /// <returns>A Vector whose values are the element-wise product of <paramref name="vector1" /> and <paramref name="vector2" />.</returns>
        /// <exception cref="ArgumentNullException">If one of the input Vector is null.</exception>
        /// <exception cref="ArgumentException">If 2 input Vector don't have the same length.</exception>
        public static Vector ElementWiseMultiplication(Vector vector1, Vector vector2)
        {
            // Check 2 input vectors before processing.
            LengthCheck(vector1, vector2);

            // Initialize the result vector.
            int length = vector1.Count;
            Vector result = new Vector(length);

            // Compute the result and return it.
            for (int i = 0; i < length; i++)
                result[i] = vector1[i] * vector2[i];
            return result;
        }

        /// <summary>
        /// Returns a Vector that represents the correlation of 2 Vector.
        /// </summary>
        /// <param name="vector1">A Vector.</param>
        /// <param name="vector2">The other Vector.</param>
        /// <returns>A Vector that represents the correlation of 2 Vector.</returns>
        public static Vector ComputeCorrelation(Vector vector1, Vector vector2)
        {
            // Throw ArgumentNullException if one of the input Vector is null.
            if (vector1 == null)
                throw new ArgumentNullException("vector1", "The input Vector must not be null.");
            if (vector2 == null)
                throw new ArgumentNullException("vector2", "The input Vector must not be null.");

            // Initialize the result vector with specified length.
            Vector correlation = new Vector(vector1.Count + vector2.Count - 1);

            // Pad zeros for vector 1.
            double[] extendedVector1 = new double[2 * (vector2.Count - 1) + vector1.Count];
            for (int i = 0; i < vector1.Count; i++)
                extendedVector1[i + vector2.Count - 1] = vector1[i];

            // Compute and return the result of the correlation operation.
            for (int i = 0; i < correlation.Count; i++)
            {
                for (int j = 0; j < vector2.Count; j++)
                    correlation[i] += extendedVector1[i + j] * vector2[j];
            }
            return correlation;
        }

        /// <summary>
        /// Returns a Vector that represents the convlution of 2 Vector.
        /// </summary>
        /// <param name="vector1">A Vector.</param>
        /// <param name="vector2">The other Vector.</param>
        /// <returns>A Vector that represents the convlution of 2 Vector.</returns>
        public static Vector ComputeConvolution(Vector vector1, Vector vector2)
        {
            // Throw ArgumentNullException if one of the input Vector is null.
            if (vector1 == null)
                throw new ArgumentNullException("vector1", "The input Vector must not be null.");
            if (vector2 == null)
                throw new ArgumentNullException("vector2", "The input Vector must not be null.");

            // Initialize the result vector with specified length.
            Vector result = new Vector(vector1.Count + vector2.Count - 1);

            // Pad zeros for vector 1.
            double[] extendedVector1 = new double[2 * (vector2.Count - 1) + vector1.Count];
            for (int i = 0; i < vector1.Count; i++)
                extendedVector1[i + vector2.Count - 1] = vector1[i];

            // Compute and return the result of the convlution operation.
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < vector2.Count; j++)
                    result[i] += extendedVector1[i + j] * vector2[vector2.Count - 1 - j];
            }
            return result;
        }

        /// <summary>
        /// Returns true if the given Vector equals to this Vector, otherwise, false.
        /// </summary>
        /// <param name="that">The given Vector.</param>
        /// <param name="epsilon">The minimum positive double value when the program determine whether the difference of 2 scalar is 0 or not.</param>
        /// <returns>true if the given Vector equals to this Vector, otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public bool Equals(Vector that, double epsilon = 1e-5)
        {
            if (that == null)
                return false;

            if (this.Count != that.Count)
                return false;

            for (int i = 0; i < this.Count; i++)
            {
                if (Math.Abs(this[i] - that[i]) > epsilon)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a Vector whose length is 1 that has the same direction with the input Vector.
        /// </summary>
        /// <param name="vector">The input Vector that provides the direction information.</param>
        /// <returns>A Vector whose length is 1 that has the same direction with the input Vector.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public static Vector Normalize(Vector vector)
        {
            double length = vector.GetLength();
            Vector result = new Vector(vector);

            return result / length;
        }

        /// <summary>
        /// Returns a Vector whose length is 1 that has the same direction with this Vector.
        /// </summary>
        /// <returns>A Vector whose length is 1 that has the same direction with this Vector.</returns>
        public Vector Normalize()
        {
            return Normalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowCopyCount"></param>
        /// <param name="columnCopyCount"></param>
        /// <param name="isRowVector"></param>
        /// <returns></returns>
        public Matrix Tile(int rowCopyCount, int columnCopyCount, bool isRowVector = true)
        {
            int baseRowCount = 1;
            int baseColumnCount = 1;

            if (isRowVector)
            {
                baseColumnCount = this.Count;
                Matrix result = new Matrix(rowCopyCount * baseRowCount, columnCopyCount * baseColumnCount);

                for (int i = 0; i < result.RowCount; i++)
                {
                    for (int j = 0; j < result.ColumnCount; j++)
                        result[i, j] = this[j % this.Count];
                }
                return result;
            }
            else
            {
                baseRowCount = this.Count;
                Matrix result = new Matrix(rowCopyCount * baseRowCount, columnCopyCount * baseColumnCount);

                for (int i = 0; i < result.RowCount; i++)
                {
                    for (int j = 0; j < result.ColumnCount; j++)
                        result[i, j] = this[i % this.Count];
                }
                return result;
            }
        }

        /// <summary>
        /// Returns the sum of each component of this Vector.
        /// </summary>
        /// <returns>The sum of each component of this Vector.</returns>
        public double SumComponents()
        {
            double sum = 0;
            for (int i = 0; i < this.Count; i++)
                sum += this[i];
            return sum;
        }

        /// <summary>
        /// Returns the some of the square of each component of this Vector.
        /// </summary>
        /// <returns>The some of the square of each component of this Vector.</returns>
        public double SumSquaredComponents()
        {
            return this * this;
        }

        /// <summary>
        /// Throws an exception if 2 input Vector don't have the same length or one of them is null.
        /// </summary>
        /// <param name="v1">An input Vector.</param>
        /// <param name="v2">The other input Vector.</param>
        /// <exception cref="ArgumentNullException">If one of the input Vector is null.</exception>
        /// <exception cref="ArgumentException">If 2 input Vector don't have the same length.</exception>
        public static void LengthCheck(Vector v1, Vector v2)
        {
            if ((v1 == null) || (v2 == null))
                throw new ArgumentNullException("The input vector must not be null.");

            if (v1.Count != v2.Count)
                throw new ArgumentException("Input vectors don't have the same length.");
        }

        /// <summary>
        /// Returns a string representation of this Vector, which contains each component of this Vector.
        /// </summary>
        /// <returns>A string representation of this Vector, which contains each component of this Vector.</returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder("[");
            foreach (double v in vector)
                s.Append(v + ", ");
            s.Remove(s.Length - 2, 2);
            s.Append("]");

            return s.ToString();
        }
    }
}