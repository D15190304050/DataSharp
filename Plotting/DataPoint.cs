using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotting
{
    /// <summary>
    /// The DataPoint class represents a point in Cartesian coordinate system.
    /// </summary>
    public class DataPoint
    {
        /// <summary>
        /// Gets or sets the x-coordinate of this point.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y-coordinate of this point.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Initializes a new DataPoint with specified coordinate.
        /// </summary>
        /// <param name="x">The x-coordinate of this DataPoint.</param>
        /// <param name="y">The y-coordinate of this DataPoint.</param>
        public DataPoint(double x = 0, double y = 0)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Returns the string representation of this DataPoint, which contains the coordinate information.
        /// </summary>
        /// <returns>The string representation of this DataPoint, which contains the coordinate information.</returns>
        public override string ToString()
        {
            return $"({this.X},{this.Y})";
        }
    }
}
