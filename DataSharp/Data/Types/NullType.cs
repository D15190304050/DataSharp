using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data.Types
{
    public class NullType : DataType
    {
        private const string NullTypeString = "null";

        public override int DefaultSize => 1;

        public override string ToString()
        {
            return NullTypeString;
        }

        public override DataType AsNullable() { return this; }
    }
}
