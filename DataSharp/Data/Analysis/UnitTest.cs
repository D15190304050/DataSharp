using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;
using System.IO;

namespace MachineLearning
{
    public static class UnitTest
    {
        /// <summary>
        /// Unit test method for methods in the MachineLearningUtil class.
        /// </summary>
        public static void MachineLearningUtilTest()
        {
            // Test for MachineLearningUtil.Normalize() method.
            Vector[] vectors = new Vector[3];
            vectors[0] = new Vector(1, 2, 3);
            vectors[1] = new Vector(1, 4, 7);
            vectors[2] = new Vector(4, 5, 6);

            var (normalizedVector, min, ranges) = MachineLearningUtil.Normalize(vectors);
            for (int i = 0; i < vectors.Length; i++)
                Console.WriteLine(vectors[i]);
            Console.WriteLine();
            for (int i = 0; i < normalizedVector.Length; i++)
                Console.WriteLine(normalizedVector[i]);
            Console.WriteLine();
            Console.WriteLine(min);
            Console.WriteLine(ranges);
        }

        /// <summary>
        /// Unit test method for the KNearestNeighbor class.
        /// </summary>
        public static void KnnTest()
        {
            /* Classify Person. */

            double testRatio = 0.1;

            // Prepare the data set.
            string filePath = @"TestData\datingTestSet2.txt";
            Vector[] datingData = MachineLearningUtil.FileToMatrix(filePath);
            int[] labels =
                (from date in datingData
                 select (int)date[date.Count - 1]).ToArray();
            Vector[] datingDataMatrix =
                (from date in datingData
                 select date.GetSubVector(0, date.Count - 2)).ToArray();

            var (normedMatrix, ranges, minValues) = MachineLearningUtil.Normalize(datingDataMatrix);
            int numRows = normedMatrix.Length;
            int numTestSamples = (int)(testRatio * numRows);

            Vector[] testSet = new Vector[numTestSamples];
            for (int i = 0; i < testSet.Length; i++)
                testSet[i] = normedMatrix[i];
            Vector[] knownSet = new Vector[numRows - numTestSamples];
            for (int i = 0; i < knownSet.Length; i++)
                knownSet[i] = normedMatrix[i + numTestSamples];

            int[] testLabels = new int[numTestSamples];
            for (int i = 0; i < testLabels.Length; i++)
                testLabels[i] = labels[i];
            int[] knownLabels = new int[numRows - numTestSamples];
            for (int i = 0; i < knownLabels.Length; i++)
                knownLabels[i] = labels[i + numTestSamples];

            KNearestNeighbor knn = new KNearestNeighbor(knownSet, knownLabels, DistanceMetrics.EuclideanDistance);

            double errorCount = 0;
            int k = 3;
            for (int i = 0; i < numTestSamples; i++)
            {
                int classierResult = knn.Classify(testSet[i], k);
                if (classierResult != testLabels[i])
                    errorCount += 1.0;
            }

            Console.WriteLine($"The total error rate is: {errorCount / numTestSamples}");
        }

        /// <summary>
        /// Unit test method for the SupportVectorMachine class.
        /// </summary>
        public static void SvmTest()
        {
            // Test for linear kernel.
            string dataPath = @"TestData\svmTestSet.txt";
            LinearKernel linearKernel = new LinearKernel();
            string[] lines = File.ReadAllLines(dataPath);
            int sampleCount = lines.Length;
            Matrix dataMatrix = new Matrix(sampleCount, 2);
            Vector labels = new Vector(sampleCount);

            for (int i = 0; i < sampleCount; i++)
            {
                string[] line = lines[i].Split('\t');
                dataMatrix[i, 0] = double.Parse(line[0]);
                dataMatrix[i, 1] = double.Parse(line[1]);
                labels[i] = double.Parse(line[2]);
            }

            SupportVectorMachine svm = new SupportVectorMachine(dataMatrix, labels, 0.6, 0.001);
            svm.Kernel = linearKernel;
            svm.Train(40);
            int sample1Index = 1;
            Console.WriteLine(svm.GetHypothesis(dataMatrix.GetRow(sample1Index)) + " " + labels[sample1Index]);
            Console.WriteLine(svm.GetHypothesis(dataMatrix.GetRow(2)) + " " + labels[2]);
        }

        /// <summary>
        /// Unit test method for the SupportVectorMachine with RBF kernel.
        /// </summary>
        public static void SvmRbfTest()
        {
            string trainingDataPath = @"TestData\testSetRBF.txt";
            string[] lines = File.ReadAllLines(trainingDataPath);
            int sampleCount = lines.Length;
            Matrix dataMatrix = new Matrix(sampleCount, 2);
            Vector labels = new Vector(sampleCount);

            for (int i = 0; i < sampleCount; i++)
            {
                string[] line = lines[i].Split('\t');
                dataMatrix[i, 0] = double.Parse(line[0]);
                dataMatrix[i, 1] = double.Parse(line[1]);
                labels[i] = double.Parse(line[2]);
            }

            SupportVectorMachine svm = new SupportVectorMachine(dataMatrix, labels, 200, 0.0001);
            GaussianKernel gaussianKernel = new GaussianKernel(1.3);
            svm.Kernel = gaussianKernel;
            svm.Train(1000);
            int sample1Index = 3;
            Console.WriteLine(svm.GetHypothesis(dataMatrix.GetRow(sample1Index)) + " " + labels[sample1Index]);
            Console.WriteLine(svm.GetHypothesis(dataMatrix.GetRow(2)) + " " + labels[2]);
        }

        /// <summary>
        /// Unit test method for the KMeans class.
        /// </summary>
        public static void KmeansTest()
        {
            string dataPath = @"TestData\clusterTestSet.txt";
            Vector[] dataMatrix = MachineLearningUtil.FileToMatrix(dataPath);
            KMeans kmeans = new KMeans(dataMatrix, 4, DistanceMetrics.EuclideanDistance);
            kmeans.Cluster();
            Vector[] centroids = kmeans.GetCentroids();
            foreach (Vector centroid in centroids)
                Console.WriteLine(centroid);
        }

        /// <summary>
        /// Unit test method for the KMeansPlusPlus class.
        /// </summary>
        public static void KmeansppTest()
        {
            string dataPath = @"TestData\clusterTestSet.txt";
            Vector[] dataMatrix = MachineLearningUtil.FileToMatrix(dataPath);
            KMeansPlusPlus kmeanspp = new KMeansPlusPlus(dataMatrix, 4, DistanceMetrics.EuclideanDistance);
            kmeanspp.Cluster();
            Vector[] centroids = kmeanspp.GetCentroids();
            foreach (Vector centroid in centroids)
                Console.WriteLine(centroid);
        }

        /// <summary>
        /// Unit test method for the FuzzyCMeans class.
        /// </summary>
        public static void FcmTest()
        {
            string dataPath = @"TestData\clusterTestSet.txt";
            Vector[] dataMatrix = MachineLearningUtil.FileToMatrix(dataPath);
            FuzzyCMeans fcm = new FuzzyCMeans(dataMatrix, 4, 2, DistanceMetrics.EuclideanDistance);
            fcm.Cluster();
            Vector[] centroids = fcm.GetCentroids();
            foreach (Vector centroid in centroids)
                Console.WriteLine(centroid);
        }

        /// <summary>
        /// Unit test method for the ArtificialBeeColony class.
        /// </summary>
        public static void AbcTest()
        {
            double[] lowerBounds = { -100, -100 };
            double[] upperBounds = { 100, 100 };
            ArtificialBeeColony abc = new ArtificialBeeColony(40, 20, 1000, 2, lowerBounds, upperBounds);
            abc.ObjectiveFunction = AbcTestObjective;

            try
            {
                double value = abc.Solve(out Vector solution);
                Console.WriteLine(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// Objective function for the AbcTest() method().
        /// </summary>
        /// <returns></returns>
        private static double AbcTestObjective(Vector v)
        {
            if (v == null)
                throw new ArgumentNullException("v");
            if (v.Count != 2)
                throw new ArgumentException("Number of components in the given vector must be 2.");

            double x = v[0] * v[0] + v[1] * v[1];
            double sin = Math.Sin(Math.Sqrt(x));
            double numerator = sin * sin - 0.5;
            double temp = 1 + 0.001 * x;
            double denomenator = temp * temp;
            return 0.5 + numerator / denomenator;
        }
    }
}
