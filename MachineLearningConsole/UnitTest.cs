using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

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
    }
}
