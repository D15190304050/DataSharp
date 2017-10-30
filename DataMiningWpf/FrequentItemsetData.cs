using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningWpf
{
    /// <summary>
    /// The FrequentItemsetData class represents a single frequent itemset for window presentation.
    /// </summary>
    internal class FrequentItemsetData
    {
        /// <summary>
        /// Gets the number of items in this frequent itemset.
        /// </summary>
        public int ItemCount { get; private set; }
        
        /// <summary>
        /// Gets the string representation of the frequent itemset.
        /// </summary>
        public string FrequentItemset { get; private set; }

        /// <summary>
        /// Gets the support count of the frequent itemset.
        /// </summary>
        public int SupportCount { get; private set; }

        /// <summary>
        /// Initializes an instance of the FrequentItemsetData.
        /// </summary>
        /// <param name="frequentItemset">The frequent itemset to represent.</param>
        public FrequentItemsetData(KeyValuePair<SortedSet<string>, int> frequentItemset)
        {
            // Get the set that stores all the items.
            SortedSet<string> items = frequentItemset.Key;

            // Get the support count and the item count.
            SupportCount = frequentItemset.Value;
            ItemCount = items.Count;

            // Use StringBuilder to accelerate.
            // Add every item in the set to the StringBuilder
            StringBuilder sbItems = new StringBuilder();
            foreach (string item in items)
                sbItems.Append(item + ", ");
            sbItems.Remove(sbItems.Length - 2, 2);

            // Get the string representation of the frequent itemset.
            FrequentItemset = sbItems.ToString();
        }
    }
}
