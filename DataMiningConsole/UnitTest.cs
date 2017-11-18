using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DataMiningConsole
{
    /// <summary>
    /// The UnitTest class provides some static methods for testing the classes we provid in this project.
    /// </summary>
    public static class UnitTest
    {
        /// <summary>
        /// Unit test method for the AprioriSqlServer class.
        /// </summary>
        /// <param name="conn">An instance of the SqlConnection class will be used, set it null to use the default connection.</param>
        /// <returns>The extracted frequent itemsets.</returns>
        public static LinkedList<Dictionary<SortedSet<string>, int>> AprioriSqlServerUnitTest(SqlConnection conn = null)
        {
            // Initialize a SqlConnection instance that will be used in the Apriori algorithm.
            // You can re-write your connection string here.
            if (conn == null)
            {
                string connString = @"Server = DESKTOP-AQ2402R\SQLEXPRESS; Integrated Security = True; Database = Startup;";
                conn = new SqlConnection(connString);
            }

            // The query command.
            string query = @"Select ShoppingList From Transactions;";

            // Initialize an Apriori solver with specified configuration.
            AprioriSqlServer apriori = new AprioriSqlServer
            {
                Connection = conn,
                CommandText = query,
                MinSupportCount = 2
            };

            // Compute the frequent itemsets.
            apriori.ComputeFrequentItemsets();

            // Get the frequent itemsets computed above.
            var frequentItemsets = apriori.FrequentItemsets;

            // Traverse through every frequent k-itemsets with k = 1, 2, 3, ...
            foreach (Dictionary<SortedSet<string>, int> foi in frequentItemsets)
            {
                // Traverse through every key value pair in the frequent k-itemsets.
                foreach (KeyValuePair<SortedSet<string>, int> itemset in foi)
                {
                    // Get the frequent itemset.
                    SortedSet<string> set = itemset.Key;

                    // Use a StringBuilder here to get the string representation of the itemset and its occurance.
                    StringBuilder sb = new StringBuilder("[ ");

                    // Add every item in the set to the StringBuilder.
                    foreach (string s in set)
                        sb.Append(s + ", ");

                    // Add the occurance to the StringBuilder.
                    sb.Remove(sb.Length - 2, 2);
                    sb.Append("; " + itemset.Value + " ]");

                    // Print the itemset and its occurance.
                    Console.WriteLine(sb.ToString());
                }

                // Print an empty line between k-itemsets and (k+1)-itemsets.
                Console.WriteLine();
            }

            return frequentItemsets;
        }

        /// <summary>
        /// Unit test method for the AssociationRules class.
        /// </summary>
        public static void AssociationRulesUnitTest(DataSourceOption option = DataSourceOption.DefaultTestData, int numRow = 0, int numItem = 0)
        {
            // Declare a collection of frequentItemsets.
            // This collection will be filled after following processing.
            LinkedList<Dictionary<SortedSet<string>, int>> frequentItemsets = null;

            // Clear data in the data table before loading test data.

            // Initialize a SqlConnection instance for following processing.
            string connectionString = @"Server = DESKTOP-2ARV8QK\DINOSTARK; Integrated Security = True; Database = Startup;";
            SqlConnection conn = new SqlConnection(connectionString);

            // Initialize a SqlCommand instance for executing the SQL command.
            // The command text here is used to clear data in the data table.
            string cmdClear = "DELETE FROM Transactions WHERE TransactionID >= 1;";
            SqlCommand cmd = new SqlCommand(cmdClear, conn);

            // Try to clear data in the data table.
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

            // Generate dataset for test.
            if (option == DataSourceOption.DefaultTestData)
            {
                // The default dataset.
                string cmdLoadData = @"INSERT INTO Transactions
                                        VALUES
                                        (1, '1,2,5'),
                                        (2, '2,4'),
                                        (3, '2,3'),
                                        (4, '1,2,4'),
                                        (5, '1,3'),
                                        (6, '2,3'),
                                        (7, '1,3'),
                                        (8, '1,2,3,5'),
                                        (9, '1,2,3'); ";
                cmd.CommandText = cmdLoadData;

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
            else
                DataGenerator.GenerateTransactionsSqlServer(conn, numRow, numItem);

            // Get the frequent itemsets computed by the AprioriSqlServer class.
            frequentItemsets = AprioriSqlServerUnitTest(conn);

            // Generate the association rules from the frequent itemsets we extract before.
            IEnumerable<AssociationRule> associationRules = AssociationRules.GenerateAssociationRules(frequentItemsets);

            // Print the association rules computed before.
            foreach (AssociationRule ar in associationRules)
                Console.WriteLine(ar);
        }

        public static void StrongARUnitTest(double minConfidence, SqlConnection conn = null)
        {
            // Get the frequent itemsets computed by the AprioriSqlServer class.
            LinkedList<Dictionary<SortedSet<string>, int>> frequentItemsets = AprioriSqlServerUnitTest();

            // Generate the association rules from the frequent itemsets we extract before.
            IEnumerable<AssociationRule> associationRules = AssociationRules.GenerateStrongAssociationRules(frequentItemsets, minConfidence);

            // Print the association rules computed before.
            foreach (AssociationRule ar in associationRules)
                Console.WriteLine(ar);
        }

        /// <summary>
        /// Unit test method for the AssociationRules class.
        /// </summary>
        public static void AprioriMySqlUnitTest()
        {
            // Initialize a MySqlConnection instance that will be used in the Apriori algorithm.
            // You can re-write your connection string here.
            string connString = @"Server = localhost; User Id = DinoStark; Password = non-feeling; Database = Startup;";
            MySqlConnection conn = new MySqlConnection(connString);

            // The query command.
            string query = @"Select ShoppingList From Transactions;";

            // Initialize an Apriori solver with specified configuration.
            AprioriMySql apriori = new AprioriMySql
            {
                Connection = conn,
                CommandText = query,
                MinSupportCount = 2
            };

            // Compute the frequent itemsets.
            apriori.ComputeFrequentItemsets();

            // Get the frequent itemsets computed above.
            var frequentItemsets = apriori.FrequentItemsets;

            // Traverse through every frequent k-itemsets with k = 1, 2, 3, ...
            foreach (Dictionary<SortedSet<string>, int> foi in frequentItemsets)
            {
                // Traverse through every key value pair in the frequent k-itemsets.
                foreach (KeyValuePair<SortedSet<string>, int> itemset in foi)
                {
                    // Get the frequent itemset.
                    SortedSet<string> set = itemset.Key;

                    // Use a StringBuilder here to get the string representation of the itemset and its occurance.
                    StringBuilder sb = new StringBuilder("[ ");

                    // Add every item in the set to the StringBuilder.
                    foreach (string s in set)
                        sb.Append(s + ", ");

                    // Add the occurance to the StringBuilder.
                    sb.Remove(sb.Length - 2, 2);
                    sb.Append("; " + itemset.Value + " ]");

                    // Print the itemset and its occurance.
                    Console.WriteLine(sb.ToString());
                }

                // Print an empty line between k-itemsets and (k+1)-itemsets.
                Console.WriteLine();
            }
        }
    }
}