using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataMiningConsole
{
    /// <summary>
    /// The FunctionalityTest class provides the prototypes of some functions will be used in this project along with some kind of test examples.
    /// </summary>
    internal static class FunctionalityTest
    {
        /// <summary>
        /// Print the elements in the specified array in a line with them separated by space.
        /// </summary>
        /// <typeparam name="T">A generic type parameter with no constrain.</typeparam>
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
        /// <typeparam name="T">A generic type parameter with no constrain.</typeparam>
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
            int[] values = { 1, 2, 3, 4 };
            SortedSet<int> set1 = new SortedSet<int>(values);
            SortedSet<int> set2 = new SortedSet<int>(values);
            Console.WriteLine(set1.SetEquals(set2));
        }

        /// <summary>
        /// Test the BuildSets algorithm.
        /// </summary>
        /// <Remarks>
        /// The BuildSets algorithm extracts some transactions from the specified table in the specified database, and
        /// then divides every transaction into separate items, in the end, build itemsets according to the items in
        /// every transactions.
        /// </Remarks>
        public static void BuildSets()
        {
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
        /// 
        /// </summary>
        public static void GetSubsetsDemo()
        {
            int[] values = { 1, 2, 3, 4, 5, 6, 7 };
            SortedSet<int> set = new SortedSet<int>(values);

            values = set.ToArray();

        }

        public static IEnumerable<T[]> Combinations<T>(T[] a, int numSelection)
        {
            LinkedList<T[]> combinations = new LinkedList<T[]>();
            int[] combination = new int[a.Length];
            for (int i = 0; i < numSelection; i++)
                combination[i] = 1;
            Combinations(a, combination, numSelection, combinations);

            return combinations;
        }

        /// <summary>
        /// Generate all the combinations with the specified number of total elements and 
        /// </summary>
        /// <Remarks>
        /// This method should be tested using reference types, so that some kind of errors will indicates the bug.
        /// </Remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="combination"></param>
        /// <param name="numSelection"></param>
        /// <param name="combinations"></param>
        private static void Combinations<T>(T[] a, int[] combination, int numSelection, LinkedList<T[]> combinations)
        {
            // Loop until there is no combination can be generated.
            while (HasNextCombination(combination, numSelection))
            {
                // A counter that stores the number of 1 that is before the 1 we swap.
                int count = 0;

                // The loop-counter that finds the index of the 1 we swap.
                int i = 0;

                // Add next combination to the linked list that stores all the combinations.
                AddNextCombination(a, combination, numSelection, combinations);

                // Find the first "10" pair and the make them "01".
                // Stores the number of 1 before the "10".
                for (i = 0; i < a.Length - 1; i++)
                {
                    if ((combination[i] == 1) && (combination[i + 1] == 0))
                    {
                        combination[i] = 0;
                        combination[i + 1] = 1;
                        break;
                    }

                    // Record the number of 1 before the 1 we swap.
                    if (combination[i] == 1)
                        count++;
                }

                // Move back.
                for (int j = 0; j < i; j++)
                {
                    if (j < count)
                        combination[j] = 1;
                    else
                        combination[j] = 0;
                }
            }

            // Add the last combination to the linked list that stores all the combinations.
            AddNextCombination(a, combination, numSelection, combinations);
        }

        /// <summary>
        /// Returns true if the caller can generate next combination.
        /// </summary>
        /// <param name="combination">The current combination.</param>
        /// <param name="numSelection">The number of elements to be extracted.</param>
        /// <returns></returns>
        private static bool HasNextCombination(int[] combination, int numSelection)
        {
            for (int i = 0; i < combination.Length - numSelection; i++)
            {
                if (combination[i] == 1)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Add the next combination of a into the collection of combinations.
        /// </summary>
        /// <typeparam name="T">A generic type parameter with no constrain.</typeparam>
        /// <param name="a">A array that contains all elements involved in the combination problem.</param>
        /// <param name="combination">The combination computed by the caller.</param>
        /// <param name="numSelection">The number of elements to be selected.</param>
        /// <param name="combinations">The collection of combinations that need to be extracted.</param>
        private static void AddNextCombination<T>(T[] a, int[] combination, int numSelection, LinkedList<T[]> combinations)
        {
            // Initializes the array to store the extracted elements.
            T[] nextCombination = new T[numSelection];

            // The next index that the next element to add to.
            int j = 0;

            // Add the selected elements if its value in the combination[] is 1.
            for (int i = 0; i < a.Length; i++)
            {
                if (combination[i] == 1)
                    nextCombination[j++] = a[i];
            }

            // Add the array of combination to the LinkedList.
            combinations.AddLast(nextCombination);
        }


    }
}