using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data.Types
{
    public class DictionaryType : DataType
    {
        public DataType KeyType { get; }
        public DataType ValueType { get; }
        public bool ValueContainsNull { get; }

        public override int DefaultSize => 1 * (this.KeyType.DefaultSize + this.ValueType.DefaultSize);

        public DictionaryType(DataType keyType, DataType valueType, bool valueContainsNull)
        {
            this.KeyType = keyType;
            this.ValueType = valueType;
            this.ValueContainsNull = valueContainsNull;
        }

        public override DataType AsNullable()
        {
            return new DictionaryType(this.KeyType.AsNullable(), this.ValueType.AsNullable(), true);
        }

        public override bool ExistsRecursively(Func<DataType, bool> f)
        {
            return f(this) || this.KeyType.ExistsRecursively(f) || this.ValueType.ExistsRecursively(f);
        }

        public override string ToString()
        {
            return $"Dictionary<${this.KeyType}, ${this.ValueType}>";
        }

        public override bool AcceptsType(DataType other)
        {
            return other is DictionaryType;
        }
    }
}
