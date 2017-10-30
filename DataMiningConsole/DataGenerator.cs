using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Mathematics;
using MySql.Data.MySqlClient;

namespace DataMiningConsole
{
    /// <summary>
    /// The DataGenerator class provides static methods for generating test data for the algorithm in this project.
    /// </summary>
    public static class DataGenerator
    {
        /// <summary>
        /// Returns a SQL INSERT command that generates transactions for frequent itemsets analysis.
        /// </summary>
        /// <param name="numRow">The number of rows in the table.</param>
        /// <param name="numItem">The number of items in the table.</param>
        /// <returns>A SQL INSERT command that generates transactions for frequent itemsets analysis.</returns>
        private static string GenerateTransactions(int numRow, int numItem, int startTransactionID = 1)
        {
            // Initialize a new LinkedList<string> to store all the transaction records.
            // I use LinkedList<T> here just because it can make use of fragment memory, while the array of string need consecutive memory.
            LinkedList<string> shoppingLists = new LinkedList<string>();

            // Generate the shopping lists for each row.
            for (int i = 0; i < numRow; i++)
            {
                // Use StringBuilder to accelerate the processing.
                StringBuilder items = new StringBuilder();

                // Generate a shopping list.
                for (int j = 1; j <= numItem; j++)
                {
                    // Add it to the end of the StringBuilder if the random number generated here is 1.
                    int state = StdRandom.Uniform(2);
                    if (state == 1)
                        items.Append(j + ",");
                }

                // Add at least 1 item to the shopping list if the random number generator generates all zeros before.
                if (items.Length == 0)
                    items.Append((StdRandom.Uniform(numItem) + 1) + ",");

                // Remove the ',' at the end.
                items.Remove(items.Length - 1, 1);

                // Add the shopping list to the
                shoppingLists.AddLast(items.ToString());
            }

            // Build the SQL command text for inserting data.

            // Use StringBuilder to accelerate the processing.
            StringBuilder textForInsertion = new StringBuilder(@"INSERT INTO Transactions VALUES");

            // TransactionID starts from 1.
            int transactionID = startTransactionID;
            foreach (string s in shoppingLists)
            {
                // Build the transaction record.
                textForInsertion.Append("(" + transactionID + ", '" + s + "'),");

                // Update the transactionID.
                transactionID++;
            }

            // Remove the ',' at the end and then add the ";".
            textForInsertion.Remove(textForInsertion.Length - 1, 1);
            textForInsertion.Append(";");

            return textForInsertion.ToString();
        }

        /// <summary>
        /// Generates the transactions for frequent itemsets analysis based on SQL Server.
        /// </summary>
        /// <param name="conn">The SqlConnection used to connect to the instance of the SQL Server.</param>
        /// <param name="numRow">The number of rows in the table.</param>
        /// <param name="numItem">The number of items in the table.</param>
        public static void GenerateTransactionsSqlServer(SqlConnection conn, int numRow, int numItem)
        {
            // Check the SQL Server connection before processing.
            CheckConnection(conn);

            // Use StringBuilder to accelerate.
            StringBuilder textForInsertion = new StringBuilder();

            if (numRow > 1000)
            {
                int startTransactionID = 1;
                for (startTransactionID = 1; startTransactionID + 1000 < numRow; startTransactionID += 1000)
                    textForInsertion.Append(GenerateTransactions(1000, numItem, startTransactionID));
                textForInsertion.Append(GenerateTransactions(numRow - startTransactionID + 1, numItem, startTransactionID));
            }
            else
                textForInsertion.Append(GenerateTransactions(numRow, numItem));

            // Generate the transactions for test.
            //string textForInsertion = GenerateTransactions(numRow, numItem);

            // Initialize a new instance of the SqlCommand with specified command text and SQL Server connection.
            SqlCommand cmd = new SqlCommand(textForInsertion.ToString(), conn);

            // Try to add data generated in this method to the test data table.
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Generates the transactions for frequent itemsets analysis based on MySQL Server.
        /// </summary>
        /// <param name="conn">The SqlConnection used to connect to the instance of the MySQL Server.</param>
        /// <param name="numRow">The number of rows in the table.</param>
        /// <param name="numItem">The number of items in the table.</param>
        public static void GenerateTransactionsMySql(MySqlConnection conn, int numRow, int numItem)
        {
            // Check the SQL Server connection before processing.
            CheckConnection(conn);

            // Generate the transactions for test.
            string textForInsertion = GenerateTransactions(numRow, numItem);

            // Initialize a new instance of the SqlCommand with specified command text and SQL Server connection.
            MySqlCommand cmd = new MySqlCommand(textForInsertion, conn);

            // Try to add data generated in this method to the test data table.
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Returns true if the SqlConnection instance is configured, false otherwise.
        /// </summary>
        /// <param name="conn">The SqlConnection instance.</param>
        /// <returns>True if the SqlConnection instance is configured, false otherwise.</returns>
        private static bool CheckConnection(SqlConnection conn)
        {
            if (conn == null)
                return false;

            string connString = conn.ConnectionString;
            if ((connString == null) || (connString.Equals("")))
                return false;

            return true;
        }

        /// <summary>
        /// Returns true if the MySqlConnection instance is configured, false otherwise.
        /// </summary>
        /// <param name="conn">The MySqlConnection instance.</param>
        /// <returns>True if the MySqlConnection instance is configured, false otherwise.</returns>
        private static bool CheckConnection(MySqlConnection conn)
        {
            if (conn == null)
                return false;

            string connString = conn.ConnectionString;
            if ((connString == null) || (connString.Equals("")))
                return false;

            return true;
        }
    }
}