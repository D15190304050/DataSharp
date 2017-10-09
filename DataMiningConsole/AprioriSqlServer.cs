using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataMiningConsole
{
    public class AprioriSqlServer
    {
        private SqlConnection connSql;

        public SqlConnection Connection
        {
            get { return connSql; }
            set { connSql = value; }
        }

        private LinkedList<LinkedList<SortedSet<string>>> frequentItemsets;

        public LinkedList<SortedSet<string>>[] FrequentItemsets { get { return frequentItemsets.ToArray(); } }

        public AprioriSqlServer(SqlConnection conn)
        {

        }
    }
}