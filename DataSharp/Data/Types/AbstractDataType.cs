using System;
using System.Collections.Generic;
using System.Text;

namespace DataSharp.Data.Types
{
    public abstract class AbstractDataType
    {
        /// <summary>
        /// The default concrete type to use if we want to cast a null literal into this type.
        /// </summary>
        public abstract DataType DefaultConcreteDataType { get; }

        /// <summary>
        /// Returns true if `<paramref name="other"/>` is an acceptable input type for a function that expects this, possibly abstract DataType.
        /// 
        /// <para>
        /// This should return true
        /// <c>DecimalType.acceptsType(DecimalType(10, 2))</c>
        /// </para>
        /// 
        /// This should return true as well
        /// <c>NumericType.acceptsType(DecimalType(10, 2))</c>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract bool AcceptsType(DataType other);
    }
}
