using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    /// <summary>
    /// The Vector class represnets a vector in mathematics that provides vector operations.
    /// </summary>
    public class Vector
    {
        /// <summary>
        /// The array that contains every components of this Vector.
        /// </summary>
        private double[] vector;

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
        public int Length { get { return vector.Length; } }

        /// <summary>
        /// Initializes a Vector with specified number of components.
        /// </summary>
        /// <param name="length">The number of components in this Vector.</param>
        public Vector(int length)
        {
            vector = new double[length];
        }

        /// <summary>
        /// Initializes a Vector from a specified double array so that this Vector will have the same values.
        /// </summary>
        /// <remarks>
        /// After initialization, this Vector will have the same value as the input double array.
        /// </remarks>
        /// <param name="vector">The specified double array.</param>
        public Vector(double[] vector)
        {
            this.vector = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
                this.vector[i] = vector[i];
        }

        /// <summary>
        /// Initializes a Vector from another Vector, this operation will make a deep copy of the input Vector.
        /// </summary>
        /// <param name="vector">The other Vector.</param>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public Vector(Vector vector)
        {
            if (this.vector == null)
                throw new ArgumentNullException("The input vector must not be null.");
            this.vector = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
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
            for (int i = 0; i < result.Length; i++)
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

            int length = vector1.Length;
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
            for (int i = 0; i < result.Length; i++)
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
            for (int i = 0; i < result.Length; i++)
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

            int length = vector1.Length;
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
            for (int i = 0; i < result.Length; i++)
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
            for (int i = 0; i < vector1.Length; i++)
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

            Vector result = new Vector(vector);
            for (int i = 0; i < result.Length; i++)
                result[i] /= scalar;

            return result;
        }

        /// <summary>
        /// Returns true if the given Vector equals to this Vector, otherwise, false.
        /// </summary>
        /// <param name="vector">The given Vector.</param>
        /// <param name="epsilon">The minimum positive double value when the program determine whether the difference of 2 scalar is 0 or not.</param>
        /// <returns>true if the given Vector equals to this Vector, otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">If the input Vector is null.</exception>
        public bool Equals(Vector vector, double epsilon = 1e-5)
        {
            if (vector == null)
                return false;

            if (this.Length != vector.Length)
                return false;

            for (int i = 0; i < this.Length; i++)
            {
                if (Math.Abs(this[i] - vector[i]) > epsilon)
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
        /// Returns the sum of each component of this Vector.
        /// </summary>
        /// <returns>The sum of each component of this Vector.</returns>
        public double SumComponents()
        {
            double sum = 0;
            for (int i = 0; i < this.Length; i++)
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

            if (v1.Length != v2.Length)
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