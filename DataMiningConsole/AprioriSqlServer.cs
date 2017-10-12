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
    public class AprioriSqlServer
    {
        private int minSupportCount;

        public int MinSupportCount
        {
            get { return minSupportCount; }
            set { minSupportCount = value; }
        }

        private SqlConnection connection;

        public SqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        private string cmdText;

        public string CommandText
        {
            get { return cmdText; }
            set { cmdText = value; }
        }

        private LinkedList<LinkedList<SortedSet<string>>> frequentItemsets;

        private LinkedList<SortedSet<string>> transactions;

        public LinkedList<SortedSet<string>>[] FrequentItemsets { get { return frequentItemsets.ToArray(); } }

        public AprioriSqlServer()
        {
            frequentItemsets = new LinkedList<LinkedList<SortedSet<string>>>();
            transactions = new LinkedList<SortedSet<string>>();
        }

        /// <summary>
        /// Returns true if there is a (k-1)-itemset of the candidate itmesets that is not in the frequent (k-1)-itemsets.
        /// </summary>
        /// <param name="candidate">A candicate itemset with k items.</param>
        /// <param name="frequentItemsets">A collection that contains all frequent (k-1)-itemsets.</param>
        /// <returns>True if there is a (k-1)-itemset of the candidate itmesets that is not in the frequent (k-1)-itemsets.</returns>
        private static bool HasInfrequentSubset(SortedSet<string> potentialCandidateItemset, LinkedList<SortedSet<string>> frequentItemsets)
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
        private static Dictionary<SortedSet<string>, int> GenerateNextCandidates(LinkedList<SortedSet<string>> frequentItemsets)
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

            // Initialize a SqlCommand instance that represents the query command will be executed.
            SqlCommand cmd = new SqlCommand(cmdText, connection);

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
            if (connection == null)
                return false;

            if (connection.ConnectionString.Equals(""))
                return false;

            if ((cmdText == null) || (cmdText.Equals("")))
                return false;

            return true;
        }

        /// <summary>
        /// Computes all the frequent k-itemsets from the transactions extracted before.
        /// </summary>
        public void ComputeFrequentItemsets()
        {
            BuildTransactionSets();

            // Get the frequent 1-itemsets.
            LinkedList<SortedSet<string>> nextFrequentItemsets = ComputeFrequentOneItemsets();

            // Loop until find the frequent itemsets that can not contain any more item.
            while (nextFrequentItemsets.Count != 0)
            {
                // Add the frequent 1-itemsets to the collection of frequent itemsets.
                frequentItemsets.AddLast(nextFrequentItemsets);

                // Get the next candidateItemsets.
                Dictionary<SortedSet<string>, int> nextCandidateItemsets = GenerateNextCandidates(nextFrequentItemsets);

                // Traverese through all the candidate k-itemsets.
                foreach (KeyValuePair<SortedSet<string>, int> candidate in nextCandidateItemsets)
                {
                    // Update the counter if the itemset is a subset (not strictly) of some transaction.
                    foreach (SortedSet<string> s in transactions)
                    {
                        // Write operation is forbidden in the foreach-clause, so we use indexer here.
                        if (candidate.Key.IsSubsetOf(s))
                            nextCandidateItemsets[candidate.Key]++;
                    }
                }

                // Extract itemsets with occurance greater than the minimum support count.
                var extractedItemsets =
                    from c in nextCandidateItemsets
                    where c.Value >= minSupportCount
                    select c.Key;

                // Generate next frequent k-itemsets.
                nextFrequentItemsets = new LinkedList<SortedSet<string>>(extractedItemsets);
            }
        }

        /// <summary>
        /// Computes the frequent 1-itemsets from the transactions extracted before.
        /// </summary>
        /// <returns>The frequent 1-itemsets from the transactions extracted before.</returns>
        private LinkedList<SortedSet<string>> ComputeFrequentOneItemsets()
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
            var extractedOneItemsets =
                from c in oneItemsets
                where c.Value >= minSupportCount
                select c.Key;

            // Generate the frequent 1-itemsets and add them to a LinkedList<Sorted<string>>.
            LinkedList<SortedSet<string>> frequentOneItemsets = new LinkedList<SortedSet<string>>();
            foreach (string s in extractedOneItemsets)
                frequentOneItemsets.AddLast(new SortedSet<string>() { s });

            // Return the frequent 1-itemsets.
            return frequentOneItemsets;
        }
    }
}