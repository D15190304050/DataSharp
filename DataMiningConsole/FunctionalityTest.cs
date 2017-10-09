using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataMiningConsole
{
    internal static class FunctionalityTest
    {
        public static void SetEqualityTest()
        {
            int[] values = { 1, 2, 3, 4 };
            SortedSet<int> set1 = new SortedSet<int>(values);
            SortedSet<int> set2 = new SortedSet<int>(values);
            Console.WriteLine(set1.SetEquals(set2));
        }

        public static void BuildSets()
        {
            string connString = @"Server = DESKTOP-2ARV8QK\DINOSTARK; Integrated Security = True; Database = Startup;";
            SqlConnection conn = new SqlConnection(connString);

            string query = @"Select ShoppingList From Transactions;";
            SqlCommand cmd = new SqlCommand(query, conn);

            LinkedList<string> lists = new LinkedList<string>();

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lists.AddLast(reader[0].ToString());

                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }

            LinkedList<SortedSet<string>> transactions = new LinkedList<SortedSet<string>>();
            foreach (string s in lists)
            {
                SortedSet<string> transaction = new SortedSet<string>(s.Split(new char[] { ',' }));
                transactions.AddLast(transaction);
            }

            foreach (SortedSet<string> t in transactions)
            {
                foreach (string s in t)
                    Console.Write(s + " ");
                Console.WriteLine();
            }
        }

        public static void GetSubsetsDemo()
        {
            int[] values = { 1, 2, 3, 4, 5, 6, 7 };
            SortedSet<int> set = new SortedSet<int>(values);

            values = set.ToArray();

        }

        private static void Combinations<T>(T[] a, T[] selected, int n, LinkedList<T[]> combinations)
        {
            if (selected.Length == n)
            {
                T[] combination = new T[n];
                for (int i = 0; i < n; i++)
                    combination[i] = selected[i];
                combinations.AddLast(combination);
            }
            else
            {

            }
        }

        public static void CombinationTest()
        {
            int[] values = { 1, 2, 3, 4, 5 };
            int total = 5;
            int n = 3;


        }

        
    }
}
