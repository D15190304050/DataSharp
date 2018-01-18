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


            double testRatio = 0.1;
        }

        public static int Main(string[] args)
        {
            UnitTest.MachineLearningUtilTest();

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}