using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data.Types
{
    public class NumericType : AbstractDataType
    {
        public override DataType DefaultConcreteDataType => throw new NotImplementedException();

        public override bool AcceptsType(DataType other)
        {
            throw new NotImplementedException();
        }
    }
}
