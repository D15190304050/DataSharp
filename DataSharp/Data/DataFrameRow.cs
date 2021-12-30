using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharp.Data
{
    /// <summary>
    /// The <see cref="DataFrameRow" /> class represents a row in a <see cref="DataFrame" /> that contains a row of data.
    /// </summary>
    /// <remaars>
    /// This class is named as "DataFrameRow" to distinguish with the <see cref="System.Data.DataRow" /> class.
    /// </remaars>
    public class DataFrameRow
    {
        private List<object> rowData;

        

        public object this[int i]
        {
            get { return rowData[i]; }
            set { rowData[i] = value; }
        }

        public object this[string columnName]
        {
            get { return null; }
            set { }
        }

        public DataFrameRow()
        {
        }


    }
}
