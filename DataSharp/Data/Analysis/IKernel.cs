using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSharp.Mathematics;

namespace DataSharp.Data.Analysis
{
    /// <summary>
    /// The IKernel interface provides a member method for applying kernel function on 2 vectors.
    /// </summary>
    public interface IKernel
    {
        /// <summary>
        /// The kernel function.
        /// </summary>
        /// <param name="v1">A Vector.</param>
        /// <param name="v2">The other Vector.</param>
        /// <returns>The kernel trnasform of 2 Vector.</returns>
        Vector KernelTransform(Matrix m, Vector v);
    }
}
