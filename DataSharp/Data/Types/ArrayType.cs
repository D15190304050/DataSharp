using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data.Types
{
    public class ArrayType : DataType
    {
        private readonly DataType DefaultConcreteType = new ArrayType(new NullType(), true);

        public override int DefaultSize => 1 * this.ElementType.DefaultSize;

        public override DataType DefaultConcreteDataType => DefaultConcreteType;

        public DataType ElementType { get; }

        public bool ContainsNull { get; }

        public ArrayType(DataType elementType, bool containsNull)
        {
            this.ElementType = elementType;
            this.ContainsNull = containsNull;
        }

        public override string ToString()
        {
            return $"array<${this.ElementType}>";
        }

        public override DataType AsNullable()
        {
            return new ArrayType(ElementType.AsNullable(), true);
        }

        public override bool ExistsRecursively(Func<DataType, bool> f)
        {
            return f(this) || ElementType.ExistsRecursively(f);
        }

        public override bool AcceptsType(DataType other)
        {
            return other is ArrayType;
        }
    }
}
