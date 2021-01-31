using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSharp.Mathematics;

namespace DataSharp.Data.Analysis
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
