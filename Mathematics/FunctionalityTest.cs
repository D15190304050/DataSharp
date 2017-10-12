using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Mathematics
{
    /// <summary>
    /// The FunctionalityTest class provides the prototypes of some functions will be used in this project along with
    /// some kind of test examples.
    /// </summary>
    public static class FunctionalityTest
    {
        /// <summary>
        /// Print the elements in the specified array in a line with them separated by space.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerator.</typeparam>
        /// <param name="array">The array to be printed.</param>
        public static void PrintIEnumerable<T>(IEnumerable<T> enumerator)
        {
            foreach (T t in enumerator)
                Console.Write(t + " ");
            Console.WriteLine();
        }

        /// <summary>
        /// Print each element in the specified array in a line.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerator.</typeparam>
        /// <param name="array">The array to be printed.</param>
        public static void PrintIEnumerableLines<T>(IEnumerable<T> enumerator)
        {
            foreach (T t in enumerator)
                Console.WriteLine(t);
        }

        /// <summary>
        /// Test the SetEquals() method defied in the ISet&lt;T> interface. 
        /// </summary>
        public static void SetEqualityTest()
        {
            // Generate test data.
            int[] values = { 1, 2, 3, 4 };
            SortedSet<int> set1 = new SortedSet<int>(values);
            SortedSet<int> set2 = new SortedSet<int>(values);
            HashSet<int> set3 = new HashSet<int>(values);

            // Test for 2 SortedSet<int>.
            Console.WriteLine("Test for 2 SortedSet<int>:");
            Console.Write("set1 : ");
            PrintIEnumerable(set1);
            Console.Write("set2 : ");
            PrintIEnumerable(set2);
            Console.WriteLine("SetEquality(set1, set2) : {0}", set1.SetEquals(set2));

            // Test for a SortedSet<int> and a HashSet<int>.
            Console.WriteLine("Test for a SortedSet<int> and a HashSet<int>:");
            Console.Write("set1 : ");
            PrintIEnumerable(set1);
            Console.Write("set3 : ");
            PrintIEnumerable(set3);
            Console.WriteLine("SetEquality(set1, set3) : {0}", set1.SetEquals(set3));
        }

        /// <summary>
        /// Test the ISet&lt;T>.ExceptWith() method.
        /// </summary>
        public static void SetRemovalTest()
        {
            int[] values1 = { 1, 2, 3, 4, 5 };
            int[] values2 = { 2, 3, 4, 5, 6 };

            SortedSet<int> set1 = new SortedSet<int>(values1);
            SortedSet<int> set2 = new SortedSet<int>(values2);

            set1.ExceptWith(set2);

            Console.Write("After ExceptWith() operation, set1 = ");
            PrintIEnumerable(set1);
            Console.Write("set2 = ");
            PrintIEnumerable(set2);
        }

        /// <summary>
        /// Test the BuildSets algorithm.
        /// </summary>
        public static void BuildSetsDemo()
        {
            // Remarks :
            // The BuildSets algorithm extracts some transactions from the specified table in the specified database,
            // and then divides every transaction into separate items, in the end, build itemsets according to the items
            // in every transactions.

            // The connection string to connect to the SQL Server.
            string connString = @"Server = DESKTOP-2ARV8QK\DINOSTARK; Integrated Security = True; Database = Startup;";
            SqlConnection conn = new SqlConnection(connString);

            // Query command.
            string query = @"Select ShoppingList From Transactions;";
            SqlCommand cmd = new SqlCommand(query, conn);

            // A linked list to store all the transactions.
            LinkedList<string> lists = new LinkedList<string>();

            // Read shopping lists.
            try
            {
                // Open the connection.
                conn.Open();

                // Read transactions and add them to the linked list.
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lists.AddLast(reader[0].ToString());

                // Close the data reader.
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                // Close the connection after the reading operation ends.
                conn.Close();
            }

            // Extract the itemsets.
            // The SortedSet<string> represents a set that stores every item in a single transaction.
            // And the LinkedList<SortedSet<string>> represents a linked list that stores these sets.
            LinkedList<SortedSet<string>> transactions = new LinkedList<SortedSet<string>>();
            foreach (string s in lists)
            {
                // Split every shopping list into separate items that stores in an array of string.
                // And then initialize a SortedSet<string> object using the array.
                SortedSet<string> transaction = new SortedSet<string>(s.Split(new char[] { ',' }));

                // Add the set to the linked list.
                transactions.AddLast(transaction);
            }

            // Print every single itemsets in a single line.
            foreach (SortedSet<string> t in transactions)
                PrintIEnumerable(t);
        }

        /// <summary>
        /// This method tries to extract all (k-1)-subsets from the set with k elements.
        /// </summary>
        public static void GetSubsetsDemo()
        {
            // Initialize a set with 7 elements for testing.
            int[] values = { 1, 2, 3, 4, 5, 6, 7 };
            SortedSet<int> set = new SortedSet<int>(values);

            // Generate all the subsets of the set we initialize above with 6 elements.
            int numSelection = 6;
            var subsets = SetHelper.GetSubsets(set, numSelection);

            // Print the original set so that the developer can check it.
            Console.Write("The original set is: ");
            PrintIEnumerable(set);

            // Print all the subsets, one in each line.
            foreach (ISet<int> s in subsets)
            {
                foreach (int i in s)
                    Console.Write(i + " ");
                Console.WriteLine("is the subset of the original set : {0}, and its type is SortedSet<int> : {1}", s.IsProperSubsetOf(set), s is SortedSet<int>);
            }
        }

        /// <summary>
        /// Functionality test function for the Combinations() method.
        /// </summary>
        public static void CombinationsFunctionalityTest()
        {
            string[] strings = { "A", "B", "C", "D", "E" };
            int numSelection = 3;
            var combinations = CombinationHelper.Combinations(strings, numSelection);
            foreach (string[] s in combinations)
                PrintIEnumerable(s);
        }
    }
}