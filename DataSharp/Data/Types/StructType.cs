using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data.Types
{
    public class StructType : DataType
    {
        public override int DefaultSize => throw new NotImplementedException();

        public override DataType AsNullable()
        {
            throw new NotImplementedException();
        }

        public override bool ExistsRecursively(Func<DataType, bool> f)
        {
            throw new NotImplementedException();
        }
    }
}
