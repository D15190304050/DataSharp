using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    /// <summary>
    /// The KMeansPlusPlus class implements the k-means++ algorithm.
    /// </summary>
    public class KMeansPlusPlus : KMeans
    {
        public KMeansPlusPlus(Vector[] dataMatrix, int k, Func<Vector, Vector, double> distanceMetrix = null)
            : base(dataMatrix, k, distanceMetrix)
        { }

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
