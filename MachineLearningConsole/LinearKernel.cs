using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    public class LinearKernel : IKernel
    {
        public LinearKernel() { }

        public double KernelTransform(Vector v1, Vector v2)
        {
            return v1 * v2;
        }
    }
}
