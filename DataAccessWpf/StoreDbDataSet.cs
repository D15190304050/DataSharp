using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessWpf
{
    public class StoreDbDataSet
    {
        /// <summary>
        /// Returns a DataTable object that contains product info in the format of DataTable.
        /// </summary>
        /// <returns></returns>
        public DataTable GetProducts()
        {
            return ReadDataSet().Tables[0];
        }

        /// <summary>
        /// Returns a DataSet object that contains product info.
        /// </summary>
        /// <returns>A DataSet object that contains product info.</returns>
        internal static DataSet ReadDataSet()
        {
            DataSet ds = new DataSet();
            ds.ReadXml("store.xml");
            return ds;
        }
    }
}
