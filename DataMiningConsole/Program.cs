using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;
using System.Data.SqlClient;

namespace DataMiningConsole
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //FunctionalityTest.StringHashTest();
            //FunctionalityTest.FrequencyCounter();

            string connString = @"Server = DESKTOP-2ARV8QK\DINOSTARK; Integrated Security = True; Database = Startup;";
            SqlConnection conn = new SqlConnection(connString);

            string query = @"Select ShoppingList From Transactions;";

            AprioriSqlServer apriori = new AprioriSqlServer
            {
                Connection = conn,
                CommandText = query,
                MinSupportCount = 2
            };
            apriori.ComputeFrequentItemsets();

            var frequentItemsets = apriori.FrequentItemsets;

            foreach (LinkedList<SortedSet<string>> foi in frequentItemsets)
            {
                foreach (SortedSet<string> set in foi)
                {
                    foreach (string s in set)
                        Console.Write(s + " ");
                    Console.WriteLine();
                }
                Console.WriteLine("\n");
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
    }
}