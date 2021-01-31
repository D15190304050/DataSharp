using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace DataMiningConsole
{
    /// <summary>
    /// The AssociationRules class provides a static method for generating association rules from the extracted frequent itemsets.
    /// </summary>
    public static class AssociationRules
    {
        /// <summary>
        /// Generate association rules from the extracted frequent itemsets.
        /// </summary>
        /// <param name="frequentItemsets">The extracted frequent itemsets.</param>
        /// <returns>Association ruels generated from the extracted frequent itemsets.</returns>
        public static IEnumerable<AssociationRule> GenerateAssociationRules(LinkedList<Dictionary<SortedSet<string>, int>> frequentItemsets)
        {
            // Return null if there is no frequent itemset.
            if ((frequentItemsets == null) || (frequentItemsets.Count == 0))
                return null;

            // Add an empty node to the linked list so that the frequentItemsets[k] will be the frequent k-itemsets.
            frequentItemsets.AddFirst(new Dictionary<SortedSet<string>, int>());

            // Initialize a linked list of string to store the text of the association rules and their confidences.
            LinkedList<AssociationRule> associationRules = new LinkedList<AssociationRule>();

            // Split the frequent itemsets into arrays.
            Dictionary<SortedSet<string>, int>[] frequentItemsetsArray = frequentItemsets.ToArray();

            // Get the frequent k-itemsets for every k >= 2.
            for (int k = 2; k < frequentItemsetsArray.Length; k++)
            {
                // Get the frequents itemset with the number of items equal to k.
                Dictionary<SortedSet<string>, int> completes = frequentItemsetsArray[k];

                // Traverse through each frequent k-itemset.
                foreach (SortedSet<string> complete in completes.Keys)
                {
                    // Get the frequency of Y.
                    int frequencyY = completes[complete];

                    // Traverse through each subset of the complete set that named 'complete' here.
                    foreach (ISet<string> x in SetHelper.GetAllSubsets(complete))
                    {
                        // Get the number of items in the set x that implies the index of the Dictionary<> that contains x.
                        int itemCount = x.Count;

                        // Initialize the occurance of x to 0.
                        int frequencyX = 0;

                        // Find the key set that equals to x.
                        foreach (SortedSet<string> itemset in frequentItemsetsArray[itemCount].Keys)
                        {
                            // There must be a set in the frequentItemsetsArray[itemCount].Keys that is equal to x.
                            // So the frequencyX must be greater than 0 when exit this foreach-loop.
                            if (x.SetEquals(itemset))
                            {
                                frequencyX = frequentItemsetsArray[itemCount][itemset];
                                break;
                            }
                        }

                        // For now we have the frequency of x, we can generate the association rule of x and Complement(complete, x).

                        // Get the complement set of x in the complete set.
                        ISet<string> y = SetHelper.GetComplement(complete, x);

                        // Get the name list of the itemset x.
                        StringBuilder itemsX = new StringBuilder();
                        foreach (string s in x)
                            itemsX.Append(s + " ");
                        itemsX.Remove(itemsX.Length - 1, 1);

                        // Get the name list of the itemset y.
                        StringBuilder itemsY = new StringBuilder();
                        foreach (string s in y)
                            itemsY.Append(s + " ");
                        itemsY.Remove(itemsY.Length - 1, 1);

                        // Initialize a new instance of the AssociationRule.
                        AssociationRule ar = new AssociationRule(itemsX.ToString(), itemsY.ToString(), frequencyX, frequencyY);
                        associationRules.AddLast(ar);
                    }
                }
            }

            // Remove the empty node we add before.
            frequentItemsets.RemoveFirst();

            // Return the association rules generated from the frequent itemsets extracted before.
            return associationRules;
        }

        /// <summary>
        /// Generate association rules from the extracted frequent itemsets whose confidence is larger than or equal to the minimum confidence.
        /// </summary>
        /// <param name="frequentItemsets">The extracted frequent itemsets.</param>
        /// <param name="minConfidence">The minimum confidence.</param>
        /// <returns>Association rules from the extracted frequent itemsets whose confidence is larger than or equal to the minimum confidence.</returns>
        public static IEnumerable<AssociationRule> GenerateStrongAssociationRules(LinkedList<Dictionary<SortedSet<string>, int>> frequentItemsets, double minConfidence)
        {
            // Get all the association rules.
            IEnumerable<AssociationRule> associationRules = GenerateAssociationRules(frequentItemsets);

            // Extract and return the association rules whose confidence is larger than or equal to the minimum confidence.
            IEnumerable<AssociationRule> strongAR =
                from ar in associationRules
                where ar.Confidence >= minConfidence
                select ar;
            return strongAR;
        }
    }
}
