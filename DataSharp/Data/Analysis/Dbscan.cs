using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSharp.Mathematics;

namespace DataSharp.Data.Analysis
{
    public class Dbscan
    {
        private Vector[] data;
        private Func<Vector, Vector, double> distanceMetric;

        /// <summary>
        /// The radius of the neigoborhood of a data point.
        /// </summary>
        private double eps;

        /// <summary>
        /// The minimum number of other data points for a data point to be a core point in a cluster.
        /// </summary>
        private int minPoints;

        private bool[] marked;

        public Dbscan(Vector[] data, Func<Vector, Vector, double> distanceMetric, double eps, int minPoints)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "The data vectors cannot be null.");
            foreach (Vector v in data)
            {
                if (v == null)
                    throw new ArgumentNullException(nameof(v), "The data vector cannot be null.");
            }

            if (distanceMetric == null)
                throw new ArgumentNullException(nameof(distanceMetric), "The distance metric cannot be null.");

            if (eps <= 0)
                throw new ArgumentOutOfRangeException(nameof(eps), "The epsilon must be greater than 0.");
            if (minPoints < 1)
                throw new ArgumentOutOfRangeException(nameof(minPoints), "The minPoints must be greater than 1.");

            this.data = data;
            this.distanceMetric = distanceMetric;
            this.eps = eps;
            this.minPoints = minPoints;

            marked = new bool[data.Length];
        }

        
    }
}
