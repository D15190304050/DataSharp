using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DataSharp.Data.Types.Atomic
{
    public class AtomicLong : AtomicBase<long>
    {
        /// <summary>
        /// Creates a new <c>AtomicLong</c> instance with an initial value of <c>0</c>.
        /// </summary>
        public AtomicLong() : this(0) { }

        /// <summary>
        /// Creates a new <c>AtomicLong</c> instance with the initial value provided.
        /// </summary>
        public AtomicLong(long value)
        {
            this.value = value;
        }

        /// <summary>
        /// This method sets the current value atomically.
        /// </summary>
        /// <param name="value">
        /// The new value to set.
        /// </param>
        public override void Set(long value)
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
        public override long GetAndSet(long value)
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
        public override bool CompareAndSet(long expected, long result)
        {
            return Interlocked.CompareExchange(ref value, result, expected) == expected;
        }

        /// <summary>
        /// Atomically adds the given value to the current value.
        /// </summary>
        /// <param name="delta">
        /// The value to add.
        /// </param>
        /// <returns>
        /// The updated value.
        /// </returns>
        public override long AddAndGet(long delta)
        {
            return Interlocked.Add(ref value, delta);
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
        public override long GetAndAdd(long delta)
        {
            for (; ; )
            {
                long current = Get();
                long next = current + delta;
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
        public override long Increment()
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
        public override long Decrement()
        {
            return GetAndAdd(-1);
        }

        /// <summary>
        /// This method increments the value by 1 and returns the new value. This is the atomic version 
        /// of pre-increment.
        /// </summary>
        /// <returns>
        /// The value after incrementing.
        /// </returns>s
        public override long PreIncrement()
        {
            return Interlocked.Increment(ref value);
        }

        /// <summary>
        /// This method decrements the value by 1 and returns the new value. This is the atomic version 
        /// of pre-decrement.
        /// </summary>
        /// <returns>
        /// The value after decrementing.
        /// </returns>
        public override long PreDecrement()
        {
            return Interlocked.Decrement(ref value);
        }

        public static implicit operator long(AtomicLong value)
        {
            return value.Get();
        }
    }
}
