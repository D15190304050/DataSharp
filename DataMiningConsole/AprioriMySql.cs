using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataMiningConsole
{
    /// <summary>
    /// The AprioriSqlServer class represents a solver of the Apriori algorith applied to MySQL Server.
    /// </summary>
    /// <remarks>
    /// This class provides member methods for computing the frequent itemsets using the Apriori algorithm.
    /// </remarks>
    public class AprioriMySql : Apriori
    {
        /// <summary>
        /// The MySQL Server connection.
        /// </summary>
        private MySqlConnection connection;

        /// <summary>
        /// /// <summary>
        /// Gets or sets the MySQL Server connection.
        /// </summary>
        /// </summary>
        public MySqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        /// <summary>
        /// The MySqlCommand that will be used to extract shoppling lists.
        /// </summary>
        private MySqlCommand cmd;

        /// <summary>
        /// Initialize a new Apriori solver for the database of MySQL Server.
        /// </summary>
        public AprioriMySql()
        {
            frequentItemsets = new LinkedList<Dictionary<SortedSet<string>, int>>();
            transactions = new LinkedList<SortedSet<string>>();
            cmd = new MySqlCommand();
        }

        /// <summary>
        /// Initialize a new Apriori solver for the database of MySQL Server.
        /// </summary>
        /// <param name="conn">The MySQL Server connection will be used by this instance.</param>
        public AprioriMySql(MySqlConnection conn) : this()
        {
            connection = conn;
        }

        /// <summary>
        /// Initialize a new Apriori solver for the database of MySQL Server.
        /// </summary>
        /// <param name="conn">The MySQL Server connection will be used by this instance.</param>
        /// <param name="cmdText">The SQL command text that will be used to extract shopping lists.</param>
        public AprioriMySql(MySqlConnection conn, string cmdText) : this()
        {
            connection = conn;
            this.cmdText = cmdText;
        }

        /// <summary>
        /// Build the transacton sets using the connection and query command configured by developer.
        /// </summary>
        protected override void BuildTransactionSets()
        {
            // Throw an exception if the SqlConnection and CommandText are not configured correcetly.
            if (!CanQuery())
                throw new AggregateException("You must configure the SqlConnection and CommandText correctly.");

            // Configure the SqlCommand instance before execute the query command.
            cmd.CommandText = cmdText;
            cmd.Connection = connection;

            // A linked list to store all the transactions.
            LinkedList<string> lists = new LinkedList<string>();

            // Read shopping lists.
            try
            {
                // Open the connection.
                connection.Open();

                // Read transactions and add them to the linked list.
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lists.AddLast(reader[0].ToString());

                // Close the data reader.
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                // Close the connection after the reading operation ends.
                connection.Close();
            }

            // Extract the itemsets.
            // The SortedSet<string> represents a set that stores every item in a single transaction.
            // And the LinkedList<SortedSet<string>> represents a linked list that stores these sets.
            transactions.Clear();
            foreach (string s in lists)
            {
                // Split every shopping list into separate items that stores in an array of string.
                // And then initialize a SortedSet<string> object using the array.
                SortedSet<string> transaction = new SortedSet<string>(s.Split(new char[] { ',' }));

                // Add the set to the linked list.
                transactions.AddLast(transaction);
            }
        }

        /// <summary>
        /// Returns true if the current configuration of the SqlConnction and the CommandText is executable, false otherwise.
        /// </summary>
        /// <returns>True if the current configuration of the SqlConnction and the CommandText is executable, false otherwise.</returns>
        protected override bool CanQuery()
        {
            // Return false if the connection is null.
            if (connection == null)
                return false;

            // Returns false if the connection string is null or empty.
            string connString = connection.ConnectionString;
            if ((connString == null) || (connString.Equals("")))
                return false;

            // Returns false if the command text is null or empty.
            if ((cmdText == null) || (cmdText.Equals("")))
                return false;

            // Return true if the MySQL Server connection and the command text are configured.
            return true;
        }
    }
}