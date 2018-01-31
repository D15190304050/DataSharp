using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    public class Program
    {
        public static void ClassifyPerson()
        {
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

        public static int Main(string[] args)
        {
            //UnitTest.MachineLearningUtilTest();
            //ClassifyPerson();

            string dataPath = @"TestData\testSet.txt";
            string[] lines = System.IO.File.ReadAllLines(dataPath);
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

            for (int i = 0; i < 10000; i++)
            {
                SupportVectorMachine svm = new SupportVectorMachine(dataMatrix, labels, 0.6, 0.001);
                svm.SequentialMinOptimization(40);
            }
            Console.WriteLine("Done");
            //Console.WriteLine(dataMatrix.GetRow(1));
            //Console.WriteLine(dataMatrix.GetRow(2));
            //Console.WriteLine(svm.GetHypothesis(dataMatrix.GetRow(1)));
            //Console.WriteLine(svm.GetHypothesis(dataMatrix.GetRow(2)));

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}