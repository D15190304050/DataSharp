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

        public Vector KernelTransform(Matrix m, Vector v)
        {
            return m * v;
        }
    }
}
