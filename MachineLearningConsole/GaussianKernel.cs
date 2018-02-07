using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    public class GaussianKernel : IKernel
    {
        public double Sigma { get; set; }

        public GaussianKernel(double sigma)
        {
            this.Sigma = sigma;
        }

        public double KernelTransform(Vector v1, Vector v2)
        {
            Vector deltaRow = v1 - v2;
            return Math.Exp(-1 * (deltaRow * deltaRow) / (/*2 * */Sigma * Sigma));
        }
    }
}