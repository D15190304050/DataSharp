﻿using System;
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

        /// <summary>
        /// Returns a boolean mask where the entry equals 1 or 0.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Tensor<T> operator >(Tensor<T> left, Tensor<T> right)
        {
            return null;
        }

        /// <summary>
        /// Returns a boolean mask where the entry equals 1 or 0.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Tensor<T> operator <(Tensor<T> left, Tensor<T> right)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis">Sort along which axis.</param>
        /// <param name="inPlace">Sorting in-place or not.</param>
        /// <returns></returns>
        public Tensor<T> Sort(int axis = 0, bool inPlace = true)
        {
            return null;
        }
    }
}
