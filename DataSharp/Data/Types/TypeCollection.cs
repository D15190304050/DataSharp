using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data.Types
{
    public class TypeCollection : AbstractDataType
    {
        private const string TypesSuffixString = ", or";

        private IEnumerable<AbstractDataType> types;

        public override DataType DefaultConcreteDataType
        {
            get
            {
                return types.GetEnumerator().Current.DefaultConcreteDataType;
            }
        }

        public TypeCollection NumericAndInterval
        {
            get
            {
                return null;
                //return TypeCollection();
            }
        }

        public TypeCollection(IEnumerable<AbstractDataType> types)
        {
            this.types = types;
        }

        public override bool AcceptsType(DataType other)
        {
            foreach (AbstractDataType dataType in types)
            {
                if (dataType.AcceptsType(other))
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder typesString = new StringBuilder("(");
            foreach (AbstractDataType dataType in types)
                typesString.Append(dataType.ToString()).Append(TypesSuffixString);

            typesString.Remove(typesString.Length - TypesSuffixString.Length, TypesSuffixString.Length);
            typesString.Append(")");

            return typesString.ToString();
        }
    }
}
