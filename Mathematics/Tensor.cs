using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public class Tensor<T>
    {
        public int Dimensions { get; }
        //public

        public void Reshape(params int[] dimensions)
        {
        }

        public Tensor<T> SubTensorRef()
        {
            return null;
        }

        public Tensor<T> SubTensorCopy()
        {
            return null;
        }

        public Tensor<T> Concatenate(int axis, params Tensor<T>[] tensors)
        {
            return null;
        }
    }
}
