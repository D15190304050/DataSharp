using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningWpf
{
    public class FrequentItemsetData
    {
        public int ItemCount { get; private set; }
        
        public string FrequentItemsets { get; private set; }

        public int SupportCount { get; private set; }

        public FrequentItemsetData(KeyValuePair<SortedSet<string>, int> frequentItemset)
        {
            SortedSet<string> items = frequentItemset.Key;
            SupportCount = frequentItemset.Value;
            ItemCount = items.Count;

            // Use StringBuilder to accelerate.
            StringBuilder sbItems = new StringBuilder();
            foreach (string item in items)
                sbItems.Append(item + ", ");
            sbItems.Remove(sbItems.Length - 2, 2);

            FrequentItemsets = sbItems.ToString();
        }
    }
}
