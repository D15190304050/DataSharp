using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    public class FuzzyCMeans
    {
        /// <summary>
        /// The Vector array that stores all the samples.
        /// </summary>
        /// <remarks>
        /// Each Vector represents one sample. All Vectors has the same number of components.
        /// </remarks>
        protected Vector[] dataMatrix;

        /// <summary>
        /// The array that contains the cluster index of each sample.
        /// </summary>
        private int[] centroidIndices;

        /// <summary>
        /// Cluster centroids of given samples.
        /// </summary>
        private Vector[] centroids;

        /// <summary>
        /// The distance metric of 2 samples.
        /// </summary>
        public Func<Vector, Vector, double> DistanceMetric { get; set; }

        /// <summary>
        /// Number of clusters.
        /// </summary>
        public int K { get; set; }

        /// <summary>
        /// The index of the probability of belonging to some cluster.
        /// </summary>
        public double ProbabilityIndex { get; set; }

        /// <summary>
        /// Initializes a new instance of the FuzzyCMeans class with given parameters.
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
        /// <exception cref="ArgumentException">If <param name="probabilityIndex" /> less than 1.</exception>
        public FuzzyCMeans(Vector[] dataMatrix, int k, double probabilityIndex, Func<Vector, Vector, double> distanceMetric = null)
        {
            // Check given dataMatrix.
            if (dataMatrix == null)
                throw new ArgumentNullException("dataMatrix");
            if (!Matrix.IsMatrix(dataMatrix))
                throw new ArgumentException("The input array of Vector doesn't have the shape as a matrix.");

            // Check the number of clusters.
            if (k <= 0)
                throw new ArgumentException("Number of clusters must be a positive integer.", "k");

            // Check probability index.
            if (probabilityIndex <= 1)
                throw new ArgumentException("Value of probabilityIndex must be greater than 1.", "probabilityIndex");

            // Initialize internal data structures.
            this.dataMatrix = dataMatrix;
            this.K = k;
            this.ProbabilityIndex = probabilityIndex;
            this.DistanceMetric = distanceMetric;
            centroidIndices = new int[dataMatrix.Length];
        }

        /// <summary>
        /// Generate random initial centroids using roulette selection.
        /// </summary>
        /// <returns>Random initial centroids selected by roulette selection.</returns>
        protected Vector[] GenerateRandomCentroids()
        {
            Vector[] randomCentroids = new Vector[this.K];
            int numSamples = dataMatrix.Length;

            LinkedList<int> centroidIndices = new LinkedList<int>();
            int nextIndex = StdRandom.Uniform(0, numSamples);
            randomCentroids[0] = dataMatrix[nextIndex];
            centroidIndices.AddLast(nextIndex);

            Vector lastCentroid = randomCentroids[0];
            Vector distanceSquares = new Vector(numSamples);
            double[] probabilities = new double[numSamples];
            while (centroidIndices.Count < this.K)
            {
                // Get the squares of distances between each sample and last selected centroid.
                for (int i = 0; i < numSamples; i++)
                {
                    distanceSquares[i] = DistanceMetric(dataMatrix[i], lastCentroid);
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

            Vector[] initialCentroids = new Vector[this.K];
            int j = 0;
            foreach (int selectedIndex in centroidIndices)
                initialCentroids[j++] = dataMatrix[selectedIndex];
            return initialCentroids;
        }

        /// <summary>
        /// Find clusters and centroids using FCM algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">If <param name="DistanceMetric" /> is null.</exception>
        public void Cluster(double epsilon = 1e-12)
        {
            // Check if there is a distance metric.
            if (DistanceMetric == null)
                throw new ArgumentNullException("DistanceMetric");

            // Get the number of samples.
            int numSamples = dataMatrix.Length;

            // A double array stores the cluster infomation of each sample.
            // memberOf[i, j]  = the probability of j-th sample belonging to cluster i.
            double[,] memberOf = new double[this.K, numSamples];

            // Get initial centroids.
            centroids = GenerateRandomCentroids();

            // distanceMatrix[i, j] = distance between i-th centroid and j-th sample.
            double[,] distanceMatrix = new double[this.K, numSamples];

            // A boolean flag that inidicates if clusters changed.
            bool clusterChanged = true;

            // Loop unitl converge.
            while (clusterChanged)
            {
                // Clear the flag.
                clusterChanged = false;

                // Update distance matrix.
                for (int i = 0; i < this.K; i++)
                {
                    for (int j = 0; j < numSamples; j++)
                        distanceMatrix[i, j] = DistanceMetric(centroids[i], dataMatrix[j]);
                }

                // Update member infomation.
                for (int i = 0; i < this.K; i++)
                {
                    for (int j = 0; j < numSamples; j++)
                    {
                        // Calculate u[i, j] (i.e. memberOf[i, j]) using  formula.
                        double denominator = 0;
                        for (int k = 0; k < this.K; k++)
                        {
                            double quotientOfDistance = distanceMatrix[i, j] / distanceMatrix[k, j];
                            denominator += Math.Pow(quotientOfDistance, 2 / (this.ProbabilityIndex - 1));
                        }

                        double updatedMemberOfIJ = 1 / denominator;
                        if (Math.Abs(memberOf[i, j] - updatedMemberOfIJ) > epsilon)
                        {
                            clusterChanged = true;
                            memberOf[i, j] = updatedMemberOfIJ;
                        }
                    }
                }

                // Update centroids.
                for (int i = 0; i < this.K; i++)
                {
                    centroids[i].Clear();
                    double denominator = 0;
                    for (int j = 0; j < numSamples; j++)
                    {
                        double memberOfFactor = Math.Pow(memberOf[i, j], this.ProbabilityIndex);
                        centroids[i] += memberOfFactor * dataMatrix[j];
                        denominator += memberOfFactor;
                    }
                    centroids[i] /= denominator;
                }
            }

            // Store the indices of each sample.
            for (int j = 0; j < numSamples; j++)
            {
                int indexOfMax = 0;
                for (int i = 1; i < this.K; i++)
                {
                    if (memberOf[i, j] > memberOf[indexOfMax, j])
                        indexOfMax = i;
                }
                centroidIndices[j] = indexOfMax;
            }
        }

        /// <summary>
        /// Returns the centroids calculated by k-means algorithm.
        /// </summary>
        /// <returns>The centroids calculated by k-means algorithm.</returns>
        public Vector[] GetCentroids()
        {
            return centroids;
        }
    }
}
