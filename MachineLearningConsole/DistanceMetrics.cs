using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    public static class DistanceMetrics
    {
        public static double EuclideanDistance(Vector vector1, Vector vector2)
        {
            return (vector1 - vector2).GetLength();
        }
    }
}
