using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DataSharp.Data.Types.Atomic
{
    public abstract class AtomicBase<T>
    {
        private T value;

        /// <summary>
        /// This method returns the current value.
        /// </summary>
        /// <returns>
        /// The value of the <c>value</c> accessed atomically.
        /// </returns>
        public T Get()
        {
            return value;
        }

        /// <summary>
        /// This method sets the current value atomically.
        /// </summary>
        /// <param name="value">
        /// The new value to set.
        /// </param>
        public abstract void Set(T value);

        /// <summary>
        /// This method atomically sets the value and returns the original value.
        /// </summary>
        /// <param name="value">
        /// The new value.
        /// </param>
        /// <returns>
        /// The value before setting to the new value.
        /// </returns>
        public abstract T GetAndSet(T value);
    }
}
