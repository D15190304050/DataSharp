using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DataSharp.Data.Types.Atomic
{
    public class AtomicDouble : AtomicBase<double>
    {
        /// <summary>
        /// Creates a new <c>AtomicDouble</c> instance with an initial value of <c>0</c>.
        /// </summary>
        public AtomicDouble() : this(0) { }

        /// <summary>
        /// Creates a new <c>AtomicDouble</c> instance with the initial value provided.
        /// </summary>
        public AtomicDouble(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// This method sets the current value atomically.
        /// </summary>
        /// <param name="value">
        /// The new value to set.
        /// </param>
        public override void Set(double value)
        {
            Interlocked.Exchange(ref this.value, value);
        }

        /// <summary>
        /// This method atomically sets the value and returns the original value.
        /// </summary>
        /// <param name="value">
        /// The new value.
        /// </param>
        /// <returns>
        /// The value before setting to the new value.
        /// </returns>
        public override double GetAndSet(double value)
        {
            return Interlocked.Exchange(ref this.value, value);
        }

        /// <summary>
        /// Atomically sets the value to the given updated value if the current value <c>==</c> the expected value.
        /// </summary>
        /// <param name="expected">
        /// The value to compare against.
        /// </param>
        /// <param name="result">
        /// The value to set if the value is equal to the <c>expected</c> value.
        /// </param>
        /// <returns>
        /// <c>true</c> if the comparison and set was successful. A <c>false</c> indicates the comparison failed.
        /// </returns>
        public override bool CompareAndSet(double expected, double result)
        {
            return Interlocked.CompareExchange(ref value, result, expected) == expected;
        }

        /// <summary>
        /// This method atomically adds a <c>delta</c> the value and returns the original value.
        /// </summary>
        /// <param name="delta">
        /// The value to add to the existing value.
        /// </param>
        /// <returns>
        /// The value before adding the delta.
        /// </returns>
        public override double GetAndAdd(double delta)
        {
            for (; ; )
            {
                double current = Get();
                double next = current + delta;
                if (CompareAndSet(current, next))
                {
                    return current;
                }
            }
        }

        /// <summary>
        /// This method increments the value by 1 and returns the previous value. This is the atomic 
        /// version of post-increment.
        /// </summary>
        /// <returns>
        /// The value before incrementing.
        /// </returns>
        public override double Increment()
        {
            return GetAndAdd(1);
        }

        /// <summary>
        /// This method decrements the value by 1 and returns the previous value. This is the atomic 
        /// version of post-decrement.
        /// </summary>
        /// <returns>
        /// The value before decrementing.
        /// </returns>
        public override double Decrement()
        {
            return GetAndAdd(-1);
        }

        public static implicit operator double(AtomicDouble value)
        {
            return value.Get();
        }
    }
}
