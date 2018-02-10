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

        public Vector KernelTransform(Matrix m, Vector v)
        {
            Vector result = new Vector(m.RowCount);
            for (int i = 0; i < m.RowCount; i++)
            {
                Vector deltaRow = m.GetRow(i) - v;
                result[i] = Math.Exp(-1 * (deltaRow * deltaRow) / (Sigma * Sigma));
            }
            return result;
        }
    }
}