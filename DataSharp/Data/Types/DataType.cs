using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data.Types
{
    public abstract class DataType : AbstractDataType
    {
        public override DataType DefaultConcreteDataType => throw new NotImplementedException();

        public abstract int DefaultSize { get; }

        public override bool AcceptsType(DataType other)
        {
            throw new NotImplementedException();
        }

        private static bool EqualsIgnoreNullability(DataType left, DataType right)
        {
            return false;
        }

        /// <summary>
        /// Returns the same data type but set all nullability fields are true.
        /// (`StructField.nullable`, `ArrayType.containsNull`, and `MapType.valueContainsNull`).
        /// </summary>
        /// <returns></returns>
        public abstract DataType AsNullable();

        /// <summary>
        /// Returns true if any `DataType` of this DataType tree satisfies the given function `f`.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public abstract bool ExistsRecursively(Func<DataType, bool> f);
    }
}
