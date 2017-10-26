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
        public static DataTable GetProducts()
        {
            return ReadDataSet().Tables[0];
        }

        internal static DataSet ReadDataSet()
        {
            DataSet ds = new DataSet();
            ds.ReadXml("store.xml");
            return ds;
        }
    }
}
