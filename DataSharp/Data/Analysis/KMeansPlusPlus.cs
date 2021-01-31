using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSharp.Mathematics;

namespace DataSharp.Data.Analysis
{
    /// <summary>
    /// The KMeansPlusPlus class implements the k-means++ algorithm.
    /// </summary>
    public class KMeansPlusPlus : KMeans
    {
        /// <summary>
        /// Initializes a new instance of the KMeansPlusPlus class.
        /// </summary>
        /// <param name="dataMatrix">The Vector array that stores all the samples.</param>
        /// <param name="k">Number of clusters.</param>
        /// <param name="distanceMetric">The distance metric of 2 samples.</param>
        /// <remarks>
        /// This constructor will make a shallow copy of dataMatrix, instead of a deep copy.
        /// </remarks>
        /// <exception cref="ArgumentNullException">If <param name="dataMatrix" /> is null.</exception>
        /// <exception cref="ArgumentException">If <param name="dataMatrix" /> doesn't have the shape as a matrix.</exception>
        /// <exception cref="ArgumentException">If <param name="k" /> less than 0.</exception>
        public KMeansPlusPlus(Vector[] dataMatrix, int k, Func<Vector, Vector, double> distanceMetric = null)
            : base(dataMatrix, k, distanceMetric)
        { }

        /// <summary>
        /// Generate random initial centroids using roulette selection.
        /// </summary>
        /// <returns>Random initial centroids selected by roulette selection.</returns>
        protected override Vector[] GenerateRandomCentroids()
        {
            Vector[] randomCentroids = new Vector[base.K];
            int numSamples = base.dataMatrix.Length;

            LinkedList<int> centroidIndices = new LinkedList<int>();
            int nextIndex = StdRandom.Uniform(0, numSamples);
            randomCentroids[0] = base.dataMatrix[nextIndex];
            centroidIndices.AddLast(nextIndex);

            Vector lastCentroid = randomCentroids[0];
            Vector distanceSquares = new Vector(numSamples);
            double[] probabilities = new double[numSamples];
            while (centroidIndices.Count < base.K)
            {
                // Get the squares of distances between each sample and last selected centroid.
                for (int i = 0; i < numSamples; i++)
                {
                    distanceSquares[i] = base.DistanceMetric(dataMatrix[i], lastCentroid);
                    distanceSquares[i] = distanceSquares[i] * distanceSquares[i];
                }

                // Calculate the probability of each sample.
                double distanceSquareSum = distanceSquares.SumComponents();
                for (int i = 0; i < numSamples; i++)
                    probabilities[i] = distanceSquares[i] / distanceSquareSum;

                // Loop until find next centroid.
                for (; ; )
                {
                    // Get the index of next potential centroid using roulette selection.
                    int selectedIndex = MathUtil.RouletteSelection(probabilities);

                    // Continue if current selected index has been seleceted before.
                    // Otherwise, store the selected index and break this loop to find next one.
                    if (centroidIndices.Contains(selectedIndex))
                        continue;
                    else
                    {
                        centroidIndices.AddLast(selectedIndex);
                        lastCentroid = dataMatrix[selectedIndex];
                        break;
                    }
                }
            }

            Vector[] initialCentroids = new Vector[base.K];
            int j = 0;
            foreach (int selectedIndex in centroidIndices)
                initialCentroids[j++] = dataMatrix[selectedIndex];
            return initialCentroids;
        }
    }
}
