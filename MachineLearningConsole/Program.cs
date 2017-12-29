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
            double[] v1 = { 1, 2 };
            double[] v2 = { 5, 5 };
            Console.WriteLine(DistanceMetrics.EuclideanDistance(new Vector(v1), new Vector(v2)));

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}
