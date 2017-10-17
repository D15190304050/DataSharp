using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningConsole
{
    /// <summary>
    /// The FrequentItemset class represents a solver to extract frequent itemsets from given data base and data table.
    /// </summary>
    public abstract class FrequentItemset
    {
        /// <summary>
        /// The minimum support count for the specified frequent itemsets problem.
        /// </summary>
        protected int minSupportCount;

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
        /// The SQL command text that will be used to extrace shoppling lists.
        /// </summary>
        protected string cmdText;

        /// <summary>
        /// Gets or sets the SQL command text that will be used to extract shoppling lists.
        /// </summary>
        public string CommandText
        {
            get { return cmdText; }
            set { cmdText = value; }
        }

        /// <summary>
        /// A LinkedList&lt;T> instance that stores all the frequent itemsets extracted by the Apriori algorithm.
        /// </summary>
        /// <remarks>
        /// The SortedSet&lt;string> class here represents the itemset that stores k items where k = 1, 2, 3, ...
        /// The Dictionary&lt;SortedSet&lt;string>, int> class here represents the collection of the KeyValuePair that associate the itemset with its occurance.
        /// The LinkedList&lt;Dictionary&lt;SortedSet&lt;string>, int>> class here represents the collection of the Dictionary&lt;TKey, TValue> mentioned above.
        /// </remarks>
        protected LinkedList<Dictionary<SortedSet<string>, int>> frequentItemsets;

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
        /// Returns true if the current configuration of the SqlConnction and the CommandText is executable, false otherwise.
        /// </summary>
        /// <returns>True if the current configuration of the SqlConnction and the CommandText is executable, false otherwise.</returns>
        protected abstract bool CanQuery();

        /// <summary>
        /// Computes all the frequent k-itemsets from the transactions extracted before.
        /// </summary>
        public abstract void ComputeFrequentItemsets();
    }
}