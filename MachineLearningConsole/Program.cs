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
        public static int Main(string[] args)
        {
            //UnitTest.MachineLearningUtilTest();
            //ClassifyPerson();

            //UnitTest.SvmTest();
            //UnitTest.SvmRbfTest();

            string dataPath = @"TestData\clusterTestSet.txt";
            Vector[] dataMatrix = MachineLearningUtil.FileToMatrix(dataPath);
            KMeans kmeans = new KMeans(dataMatrix, 4, DistanceMetrics.EuclideanDistance);
            kmeans.Cluster();
            Vector[] centroids = kmeans.GetCentroids();
            foreach (Vector centroid in centroids)
                Console.WriteLine(centroid);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}