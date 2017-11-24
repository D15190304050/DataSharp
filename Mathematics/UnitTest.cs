using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public static class UnitTest
    {
        /// <summary>
        /// Unit test method for the Vector class.
        /// </summary>
        public static void VectorTest()
        {
            double[] values = { 1.1, 2.2, 3.3, 4.4, 5.5 };

            Vector v1 = new Vector(values);
            Vector v2 = new Vector(v1);
            v2 = v2 - 1;
            Console.WriteLine($"v1 = {v1}");
            Console.WriteLine($"v2 = {v2}");
            Console.WriteLine();

            Console.WriteLine($"The sub-vector with range [1, 3] of v1 is {v1.GetSubVector(1, 3)}\n");

            Console.WriteLine($"v1 + 3 = {v1 + 3}");
            Console.WriteLine($"5 + v1 = {5 + v1}");
            Console.WriteLine($"v1 + v2 = {v1 + v2}");
            Console.WriteLine();

            Console.WriteLine($"v1 - 1 = {v1 - 1}");
            Console.WriteLine($"5 - v1 = {5 - v1}");
            Console.WriteLine($"v1 - v2 = {v1 - v2}");
            Console.WriteLine();

            Console.WriteLine($"v1 * 2 = {v1 * 2}");
            Console.WriteLine($"3 * v1 = {3 * v1}");
            Console.WriteLine($"v1 * v2 = {v1 * v2}");
            Console.WriteLine();

            Console.WriteLine($"v1 / 0.5 = {v1 / 0.5}");
            Console.WriteLine();

            Vector v3 = new Vector(new double[] { 1, 1 });
            Console.WriteLine($"v3 = {v3}");
            Console.WriteLine($"After normalization, v3 = {v3.Normalize()}");
            Console.WriteLine();

            Console.WriteLine($"Sum of v1 = {v1.SumComponents()}");
            Console.WriteLine($"Sum of squared components of v1 = {v1.SumSquaredComponents()}");
        }
    }
}
