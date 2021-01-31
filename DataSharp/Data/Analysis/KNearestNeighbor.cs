using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mathematics;

namespace MachineLearning
{
    /// <summary>
    /// The KNearestNeighbor class represents the solver of k-Nearest Neighbor algorithm for given data set.
    /// </summary>
    public class KNearestNeighbor
    {
        /// <summary>
        /// Gets or sets the distance metric of this k-NN model.
        /// </summary>
        public Func<Vector, Vector, double> DistanceMetric { get; set; }

        /// <summary>
        /// Known data points of this k-NN model.
        /// </summary>
        private Vector[] dataSet;

        /// <summary>
        /// Correspond labels of this k-NN model for known data points.
        /// </summary>
        private int[] labels;

        /// <summary>
        /// Number of dimensions of given data points.
        /// </summary>
        private readonly int dataSize;

        public KNearestNeighbor(Vector[] dataSet, int[] labels, Func<Vector, Vector, double> distanceMetric)
        {
            if (dataSet == null)
                throw new ArgumentNullException("dataSet", "The input array of Vector is null.");
            if (!Matrix.IsMatrix(dataSet))
                throw new ArgumentException("The input array of Vector doesn't have the shape as a matrix.");
            if (labels == null)
                throw new ArgumentNullException("labels", "The input array of int is null.");
            if (dataSet.Length != labels.Length)
                throw new ArgumentException("The size of the data set isn't equal to the length of labels.");

            this.dataSet = dataSet;
            this.labels = labels;
            this.DistanceMetric = distanceMetric;
            dataSize = dataSet.Length;
        }

        /// <summary>
        /// Returns the index of the label of given sample computed by k-NN algorithm.
        /// </summary>
        /// <param name="sample"></param>
        /// <param name="dataSet"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int Classify(Vector sample, int k)
        {
            int dataSize = dataSet.Length;
            Vector[] differenceMatrix = new Vector[dataSize];
            for (int i = 0; i < dataSize; i++)
                differenceMatrix[i] = sample - dataSet[i];

            // Compute the length of every difference Vector.
            Dictionary<int, double> distances = new Dictionary<int, double>();
            for (int i = 0; i < dataSize; i++)
                distances.Add(i, DistanceMetric(sample, dataSet[i]));
            int[] sortedDistanceIndices =
                (from kvp in distances
                 orderby kvp.Value
                 select kvp.Key).ToArray();

            // Sort and get the correspond indices.
            Dictionary<int, int> classCount = new Dictionary<int, int>();
            for (int i = 0; i < k; i++)
            {
                int vote = labels[sortedDistanceIndices[i]];

                if (!classCount.ContainsKey(vote))
                    classCount[vote] = 0;
                classCount[vote]++;
            }

            // Get the class label.
            int[] sortedClassCount =
                (from kvp in classCount
                 orderby kvp.Value descending
                 select kvp.Key).ToArray();

            return sortedClassCount[0];
        }
    }
}