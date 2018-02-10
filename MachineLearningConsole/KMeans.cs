using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    public class KMeans
    {
        private Vector[] dataMatrix;

        public Func<Vector, Vector, double> DistanceMetric { get; set; }

        public int K { get; set; }

        public KMeans(Vector[] dataMatrix, int k, Func<Vector, Vector, double> distanceMetric = null)
        {
            if (dataMatrix == null)
                throw new ArgumentNullException("dataMatrix");
            if (!Matrix.IsMatrix(dataMatrix))
                throw new ArgumentException("The input array of Vector doesn't have the shape as a matrix.");

            this.dataMatrix = dataMatrix;
            this.K = k;
            this.DistanceMetric = distanceMetric;
        }

        private Vector[] GetRandomCentroids()
        {
            Vector[] randomCentroids = new Vector[this.K];
            StdRandom.Shuffle(dataMatrix);
            for (int i = 0; i < this.K; i++)
                randomCentroids[i] = dataMatrix[i];
            return randomCentroids;
        }

        public void Cluster()
        {
            int numSamples = dataMatrix.Length;
            int[,] clusterInfo = new int[numSamples, 2];

        }
    }
}
