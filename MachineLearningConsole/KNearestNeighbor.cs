using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mathematics;

namespace MachineLearning
{
    public class KNearestNeighbor
    {
        /// <summary>
        /// Gets or sets the DistanceMetric of this k-NN model.
        /// </summary>
        public Func<Vector, double> DistanceMetric { get; set; }

        /// <summary>
        /// Returns the index of the label of given sample computed by k-NN algorithm.
        /// </summary>
        /// <param name="sample"></param>
        /// <param name="dataSet"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int Classify(Vector sample, Vector[] dataSet, int[] labels, int k)
        {
            if (dataSet == null)
                throw new ArgumentNullException("dataSet", "The input array of Vector is null.");
            if (!Matrix.IsMatrix(dataSet))
                throw new ArgumentException("The input array of Vector doesn't have the shape as a matrix.");

            int dataSize = dataSet.Length;
            Vector[] differenceMatrix = new Vector[dataSize];
            for (int i = 0; i < dataSize; i++)
                differenceMatrix[i] = sample - dataSet[i];

            Dictionary<int, double> distances = new Dictionary<int, double>();
            for (int i = 0; i < dataSize; i++)
                distances.Add(i, differenceMatrix[i].GetLength());
            int[] sortedDistanceIndices =
                (from kvp in distances
                 orderby kvp.Value
                 select kvp.Key).ToArray();

            Dictionary<int, int> classCount = new Dictionary<int, int>();
            for (int i = 0; i < k; i++)
            {
                int vote = labels[sortedDistanceIndices[i]];

                if (!classCount.ContainsKey(vote))
                    classCount[vote] = 0;
                classCount[vote]++;
            }

            int[] sortedClassCount =
                (from kvp in classCount
                 orderby kvp.Value descending
                 select kvp.Key).ToArray();

            return sortedClassCount[0];
        }
    }
}