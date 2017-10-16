using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

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
        public static void AprioriSqlServerUnitTest()
        {
            // Initialize a SqlConnection instance that will be used in the Apriori algorithm.
            // You can re-write your connection string here.
            string connString = @"Server = DESKTOP-2ARV8QK\DINOSTARK; Integrated Security = True; Database = Startup;";
            SqlConnection conn = new SqlConnection(connString);

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
        }

        /// <summary>
        /// Unit test method for the AssociationRules class.
        /// </summary>
        public static void AssociationRulesUnitTest()
        {
            // Initialize a SqlConnection instance that will be used in the Apriori algorithm.
            // You can re-write your connection string here.
            string connString = @"Server = DESKTOP-2ARV8QK\DINOSTARK; Integrated Security = True; Database = Startup;";
            SqlConnection conn = new SqlConnection(connString);

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

            // Generate the association rules from the frequent itemsets we extract before.
            IEnumerable<AssociationRule> associationRules = AssociationRules.GenerateAssociationRules(frequentItemsets);

            foreach (AssociationRule ar in associationRules)
                Console.WriteLine(ar);
        }
    }
}