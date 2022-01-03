using DataSharp.Data.Types.Atomic;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data
{
    [Serializable]
    public class Dataset<T>
    {
        private static AtomicLong currentId;

        static Dataset()
        {
            currentId = new AtomicLong();
        }


    }
}
