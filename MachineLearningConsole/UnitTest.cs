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
            SvmTest(dataPath, linearKernel);

            // Test for Gaussian kernel.
            dataPath = @"TestData\testSetRBF.txt";
            GaussianKernel gaussianKernel = new GaussianKernel(1.3);
        }

        /// <summary>
        /// A helper function for unit testing SVM.
        /// </summary>
        /// <param name="dataPath">The text file that contains the data set.</param>
        /// <param name="kernel">The given kernel function.</param>
        public static void SvmTest(string dataPath, IKernel kernel)
        {
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
            svm.Kernel = kernel;
            svm.Train(40);
            Console.WriteLine(dataMatrix.GetRow(1));
            Console.WriteLine(dataMatrix.GetRow(2));
            Console.WriteLine(svm.GetHypothesis(dataMatrix.GetRow(1)) + " " + labels[1]);
            Console.WriteLine(svm.GetHypothesis(dataMatrix.GetRow(2)) + " " + labels[2]);
        }

        public static void SvmRbfTest()
        {
            string trainingDataPath = @"TestData\testSetRBF.txt";

        }
    }
}
