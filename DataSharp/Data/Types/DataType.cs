﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data.Types
{
    public abstract class DataType : AbstractDataType
    {
        public override DataType DefaultConcreteDataType => throw new NotImplementedException();

        public override bool AcceptsType(DataType other)
        {
            throw new NotImplementedException();
        }

        private static bool EqualsIgnoreNullability(DataType left, DataType right)
        {
            return false;
        }
    }
}
