using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Mathematics;

namespace DataMiningConsole
{
    /// <summary>
    /// The AprioriSqlServer class represents the solver of the Apriori algorith applied to SQL Server.
    /// </summary>
    /// <remarks>
    /// This class provides member methods for computing the frequent itemsets using the Apriori algorithm.
    /// </remarks>
    public class AprioriSqlServer
    {
        /// <summary>
        /// The minimum support count for the specified frequent itemsets problem.
        /// </summary>
        private int minSupportCount;

        /// <summary>
        /// Gets or sets the minimum support count.
        /// </summary>
        /// <remarks>
        /// Any itemset with occurance greater than or euqal to the minimum support count will be regarded as a frequent itemset.
        /// </remarks>
        public int MinSupportCount
        {
            get { return minSupportCount; }
            set { minSupportCount = value; }
        }

        /// <summary>
        /// The SQL Server connection.
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Gets or sets the SQL Server connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        /// <summary>
        /// The SQL command text that will be used to extrace shoppling lists.
        /// </summary>
        private string cmdText;

        /// <summary>
        /// Gets or sets the SQL command text that will be used to extract shoppling lists.
        /// </summary>
        public string CommandText
        {
            get { return cmdText; }
            set { cmdText = value; }
        }

        /// <summary>
        /// The SqlCommand that will be used to extract shoppling lists.
        /// </summary>
        private SqlCommand cmd;
        
        /// <summary>
        /// A LinkedList&lt;T> instance that stores all the frequent itemsets extracted by the Apriori algorithm.
        /// </summary>
        /// <remarks>
        /// The SortedSet&lt;string> class here represents the itemset that stores k items where k = 1, 2, 3, ...
        /// The Dictionary&lt;SortedSet&lt;string>, int> class here represents the collection of the KeyValuePair that associate the itemset with its occurance.
        /// The LinkedList&lt;Dictionary&lt;SortedSet&lt;string>, int>> class here represents the collection of the Dictionary&lt;TKey, TValue> mentioned above.
        /// </remarks>
        private LinkedList<Dictionary<SortedSet<string>, int>> frequentItemsets;

        /// <summary>
        /// Gets the frequent itemsets extracted by the Apriori algorithm.
        /// </summary>
        /// <remarks>
        /// The SortedSet&lt;string> class here represents the itemset that stores k items where k = 1, 2, 3, ...
        /// The Dictionary&lt;SortedSet&lt;string>, int> class here represents the collection of the KeyValuePair that associate the itemset with its occurance.
        /// The Dictionary&lt;SortedSet&lt;string>, int>[] class here represents the collection of the Dictionary&lt;TKey, TValue> mentioned above.
        /// </remarks>
        public LinkedList<Dictionary<SortedSet<string>, int>> FrequentItemsets { get { return frequentItemsets; } }

        /// <summary>
        /// A LinkedList&lt;T> instance that stores all the sets of items in the data table.
        /// </summary>
        /// <remarks>
        /// The SortedSet&lt;string> class here represents a collection of items in a shopping list.
        /// </remarks>
        private LinkedList<SortedSet<string>> transactions;

        /// <summary>
        /// Initialize a new Apriori solver for the database of SQL Server.
        /// </summary>
        public AprioriSqlServer()
        {
            frequentItemsets = new LinkedList<Dictionary<SortedSet<string>, int>>();
            transactions = new LinkedList<SortedSet<string>>();
            cmd = new SqlCommand();
        }

        /// <summary>
        /// Returns true if there is a (k-1)-itemset of the candidate itmesets that is not in the frequent (k-1)-itemsets.
        /// </summary>
        /// <param name="candidate">A candicate itemset with k items.</param>
        /// <param name="frequentItemsets">A collection that contains all frequent (k-1)-itemsets.</param>
        /// <returns>True if there is a (k-1)-itemset of the candidate itmesets that is not in the frequent (k-1)-itemsets.</returns>
        private static bool HasInfrequentSubset(SortedSet<string> potentialCandidateItemset, IEnumerable<SortedSet<string>> frequentItemsets)
        {
            // Get the value of (k-1).
            int numSelection = potentialCandidateItemset.Count - 1;

            // Get all the subsets of candidate itemset.
            var subsets = SetHelper.GetSubsets(potentialCandidateItemset, numSelection);

            // Returns true If any of the subset is not a set in frequent (k-1)-itemsets, false otherwise.
            foreach (var s in subsets)
            {
                if (!SetHelper.ContainsSet(frequentItemsets, s))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Generate candidate itemsets with k items from frequent (k-1)-itemsets.
        /// </summary>
        /// <param name="frequentItemsets">The frequent (k-1)-itemsets.</param>
        /// <returns>Candidate itemsets with k items</returns>
        private static Dictionary<SortedSet<string>, int> GenerateNextCandidates(IEnumerable<SortedSet<string>> frequentItemsets)
        {
            // Initialize an empty Dictionary<SortedSet<string>, int> instance to store the candidate itemsets.
            Dictionary<SortedSet<string>, int> candidateItemsets = new Dictionary<SortedSet<string>, int>();

            // Get the array of frequent (k-1)-itemsets.
            SortedSet<string>[] itemsets = frequentItemsets.ToArray();

            // Traverse through the frequent (k-1)-itemsets.
            for (int i = 0; i < itemsets.Length - 1; i++)
            {
                for (int j = i + 1; j < itemsets.Length; j++)
                {
                    // Get 2 frequent (k-1)-itemsets.
                    SortedSet<string> frequentItemset1 = itemsets[i];
                    SortedSet<string> frequentItemset2 = itemsets[j];

                    // Find the items that is contained in frequentItemset1 but is not contained in frequentItemset2, and store the result in variable delta.
                    SortedSet<string> delta = new SortedSet<string>(frequentItemset1);
                    delta.ExceptWith(frequentItemset2);

                    // Generate candidate k-itemsets if the delta contains excat 1 item.
                    if (delta.Count == 1)
                    {
                        // Initialize the potential candidate itemset using frequentItemset2.
                        SortedSet<string> potentialCandidateItemset = new SortedSet<string>(frequentItemset2);

                        // Add the item in the delta to the potential candidate itemset.
                        string[] item = delta.ToArray();
                        potentialCandidateItemset.Add(item[0]);

                        if (SetHelper.ContainsSet(candidateItemsets.Keys, potentialCandidateItemset))
                            continue;

                        // Add this potential candidate itemset to the collection of candidate k-itemsets if its every subset with (k-1) elements is contained in the frequent (k-1)-itemsets.
                        if (!HasInfrequentSubset(potentialCandidateItemset, frequentItemsets))
                            candidateItemsets.Add(potentialCandidateItemset, 0);
                    }
                }
            }

            // Return the candidate itemsets with k items.
            return candidateItemsets;
        }

        /// <summary>
        /// Build the transacton sets using the connection and query command configured by developer.
        /// </summary>
        private void BuildTransactionSets()
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
        private bool CanQuery()
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

            // Return true if the SQL Server connection and the command text are configured.
            return true;
        }

        /// <summary>
        /// Computes all the frequent k-itemsets from the transactions extracted before.
        /// </summary>
        public void ComputeFrequentItemsets()
        {
            // Extract all the transactions and store them before computing.
            BuildTransactionSets();
            
            // The nextFrequentItemsets variable tracks the frequent itemsets with the largest k we archived during the processing.
            // The k is 1 at first.
            Dictionary<SortedSet<string>, int> nextFrequentItemsets = ComputeFrequentOneItemsets();

            // Loop until find the frequent itemsets that can not contain any more item.
            while (nextFrequentItemsets.Count != 0)
            {
                // Add the frequent 1-itemsets to the collection of frequent itemsets.
                frequentItemsets.AddLast(nextFrequentItemsets);

                // Get the next candidateItemsets.
                Dictionary<SortedSet<string>, int> nextCandidateItemsets = GenerateNextCandidates(nextFrequentItemsets.Keys);

                // Traverese through all the candidate k-itemsets.
                // Write operation is forbidden in the foreach-clause, but it is necessary to update the counter of the occurace of every candidate itemset.
                // So, use the IEnumberable<T>.ToArray() method to create an array that contains the same contents, 
                // and then we traverse through the array and update the counter.
                KeyValuePair<SortedSet<string>, int>[] candidates = nextCandidateItemsets.ToArray();
                for (int i = 0; i < candidates.Length; i++)
                {
                    // Get the itemset as a key that will be used.
                    SortedSet<string> key = candidates[i].Key;

                    // Update the counter if the itemset is a subset (not strictly) of some transaction.
                    foreach (SortedSet<string> s in transactions)
                    {
                        if (key.IsSubsetOf(s))
                            nextCandidateItemsets[key]++;
                    }
                }

                // Extract itemsets with occurance greater than the minimum support count.
                // This LINQ expression will be explained in the document.
                var extractedItemsets =
                    from c in nextCandidateItemsets
                    where c.Value >= minSupportCount
                    select new { Key = c.Key, Value = c.Value };

                // Generate next frequent k-itemsets.
                // Create the Dictionary<TKey, TValue> object from the itemsets extracted above.
                Dictionary<SortedSet<string>, int> extractedFrequentItemsets = new Dictionary<SortedSet<string>, int>();
                foreach (var itemset in extractedItemsets)
                    extractedFrequentItemsets.Add(itemset.Key, itemset.Value);

                // Update the nextFrequentItemsets with the max-k archived.
                nextFrequentItemsets = extractedFrequentItemsets;
            }
        }

        /// <summary>
        /// Computes the frequent 1-itemsets from the transactions extracted before.
        /// </summary>
        /// <returns>The frequent 1-itemsets from the transactions extracted before.</returns>
        private Dictionary<SortedSet<string>, int> ComputeFrequentOneItemsets()
        {
            // Initialize an empty Dictionary<string, int> instance to count the occurance of every kind of items.
            Dictionary<string, int> oneItemsets = new Dictionary<string, int>();

            // Scan each transaction.
            foreach (SortedSet<string> set in transactions)
            {
                // Scan every single item in a transaction.
                foreach (string s in set)
                {
                    // Add the item to the dictionary when first meet it.
                    // Update the counter otherwise.
                    if (!oneItemsets.ContainsKey(s))
                        oneItemsets[s] = 1;
                    else
                        oneItemsets[s]++;
                }
            }

            // Extract all the items with occurance greater than the minimum support count.
            // This LINQ expression will be explained in the document.
            var extractedOneItemsets =
                from c in oneItemsets
                where c.Value >= minSupportCount
                select new { Key = c.Key, Value = c.Value };

            // Generate the frequent 1-itemsets and add them to a LinkedList<Sorted<string>>.
            Dictionary<SortedSet<string>, int> frequentOneItemsets = new Dictionary<SortedSet<string>, int>();
            foreach (var itemset in extractedOneItemsets)
            {
                SortedSet<string> items = new SortedSet<string> { itemset.Key };
                frequentOneItemsets.Add(items, itemset.Value);
            }

            // Return the frequent 1-itemsets.
            return frequentOneItemsets;
        }
    }
}