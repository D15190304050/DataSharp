using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace DataMiningConsole
{
    /// <summary>
    /// The Apriori class represents the Apriori algorithm that is used for finding frequent itemsets from specified data table.
    /// </summary>
    /// <remarks>
    /// When inheriting this abstract class, you must provide a database connection and implements the methods defined here.
    /// </remarks>
    public abstract class Apriori : FrequentItemset
    {
        /// <summary>
        /// A LinkedList&lt;T> instance that stores all the sets of items in the data table.
        /// </summary>
        /// <remarks>
        /// The SortedSet&lt;string> class here represents a collection of items in a shopping list.
        /// </remarks>
        protected LinkedList<SortedSet<string>> transactions;

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

                    // Get the superset of 2 selected frequent (k-1)-itemsets.
                    SortedSet<string> potentialCandidateItemset = new SortedSet<string>(frequentItemset1);
                    potentialCandidateItemset.UnionWith(frequentItemset2);

                    // Generate candidate k-itemsets if the number of elements in the superset is k,
                    // where (k-1) is the number of elements in the selected frequent itemsets.
                    if (potentialCandidateItemset.Count == frequentItemset1.Count + 1)
                    {
                        // Skip this potential candidate itemset if it exists in the collection to return.
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
        protected abstract void BuildTransactionSets();

        /// <summary>
        /// Computes all the frequent k-itemsets from the transactions extracted before.
        /// </summary>
        public override void ComputeFrequentItemsets()
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