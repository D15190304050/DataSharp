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
            string filePath = @"TestData\datingTestSet2.txt";
            double testRatio = 0.1;
            Vector[] datingData = MachineLearningUtil.FileToMatrix(filePath);
            int[] labels =
                (from date in datingData
                 select (int)date[date.Length - 1]).ToArray();
            for (int i = 0; i < datingData.Length; i++)
                datingData[i] = datingData[i].GetSubVector(0, datingData[i].Length - 2);


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