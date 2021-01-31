using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    /// <summary>
    /// The KMeans class impelemts the k-means algorithm.
    /// </summary>
    public class KMeans
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
        private readonly int[] centroidIndices;

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
        /// Initializes a new instance of the KMeans class with given parameters.
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
        public KMeans(Vector[] dataMatrix, int k, Func<Vector, Vector, double> distanceMetric = null)
        {
            // Check given dataMatrix.
            if (dataMatrix == null)
                throw new ArgumentNullException("dataMatrix");
            if (!Matrix.IsMatrix(dataMatrix))
                throw new ArgumentException("The input array of Vector doesn't have the shape as a matrix.");

            // Check the number of clusters.
            if (k <= 0)
                throw new ArgumentException("Number of clusters must be a positive integer.", "k");

            // Initialize internal data structures.
            this.dataMatrix = dataMatrix;
            this.K = k;
            this.DistanceMetric = distanceMetric;
            centroidIndices = new int[dataMatrix.Length];
        }

        /// <summary>
        /// Generates K random centroids for clustering.
        /// </summary>
        /// <returns>K random centroids.</returns>
        protected virtual Vector[] GenerateRandomCentroids()
        {
            Vector[] randomCentroids = new Vector[this.K];

            // Shuffle all the samples and randomly pick first K samples as initial centroids.
            StdRandom.Shuffle(dataMatrix);
            for (int i = 0; i < this.K; i++)
                randomCentroids[i] = dataMatrix[i];

            return randomCentroids;
        }

        /// <summary>
        /// Find clusters and centroids using k-means algorithm.
        /// </summary>
        /// <exception cref="ArgumentNullException">If <param name="DistanceMetric" /> is null.</exception>
        public void Cluster()
        {
            // Check if there is a distance metric.
            if (this.DistanceMetric == null)
                throw new ArgumentNullException("DistanceMetric");

            // Get the number of samples.
            int numSamples = dataMatrix.Length;

            // A double array stores the cluster infomation of each sample.
            // clusterInfo[i, 0] indicates the cluster index of i-th sample.
            // clusterInfo[i, 1] indicates the distance between the cluster centroid and i-th sample, measured by given distance metric.
            double[,] clusterInfo = new double[numSamples, 2];

            // Get initial centroids.
            centroids = GenerateRandomCentroids();

            // A boolean flag that inidicates if clusters changed.
            bool clusterChanged = true;

            // Loop unitl converge.
            while (clusterChanged)
            {
                // Clear the flag.
                clusterChanged = false;

                // Update clusters.
                for (int sample = 0; sample < numSamples; sample++)
                {
                    double minDistance = double.PositiveInfinity;
                    int clusterIndex = -1;

                    for (int centroid = 0; centroid < this.K; centroid++)
                    {
                        double distanceFromCentroid = DistanceMetric(centroids[centroid], dataMatrix[sample]);
                        if (distanceFromCentroid < minDistance)
                        {
                            minDistance = distanceFromCentroid;
                            clusterIndex = centroid;
                        }
                    }

                    if (clusterInfo[sample, 0] != clusterIndex)
                        clusterChanged = true;

                    clusterInfo[sample, 0] = clusterIndex;
                    clusterInfo[sample, 1] = minDistance;
                }

                // Update centroids.
                for (int centroid = 0; centroid < this.K; centroid++)
                {
                    // Get all the samples in the same cluster.
                    LinkedList<Vector> pointsInCluster = new LinkedList<Vector>();
                    for (int sample = 0; sample < numSamples; sample++)
                    {
                        if (clusterInfo[sample, 0] == centroid)
                            pointsInCluster.AddLast(dataMatrix[sample]);
                    }

                    // Update the centroid.
                    if (pointsInCluster.Count != 0)
                    {
                        Matrix points = new Matrix(pointsInCluster.ToArray());
                        centroids[centroid] = points.Mean(0);
                    }
                }
            }

            // Store the indices of each sample.
            for (int i = 0; i < numSamples; i++)
                centroidIndices[i] = (int)clusterInfo[i, 0];
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