using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public static class SetHelper
    {
        /// <summary>
        /// Returns a collection that contains all the subsets of the input set with specified number of elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The input set.</param>
        /// <param name="numSelection">The number of elements in every subset to be generated.</param>
        /// <returns>A collection that contains all the subsets of the input set with specified number of elements.</returns>
        public static IEnumerable<ISet<T>> GetSubsets<T>(ISet<T> set, int numSelection)
        {
            // Remarks :
            // This method is suitable for the developer-defined class that implements the ISet<T> interface.

            // Get the array format of the set.
            T[] values = set.ToArray();

            // Get all the subsets in a collection with the array format.
            IEnumerable<T[]> combinations = CombinationHelper.Combinations(values, numSelection);

            // Generate the subsets using the array format we computed above.
            LinkedList<ISet<T>> subsets = new LinkedList<ISet<T>>();
            foreach (T[] combination in combinations)
                subsets.AddLast(new SortedSet<T>(combination));

            // Return the collection that contains all the subset of the original set with specified number of elements.
            return subsets;
        }

        /// <summary>
        /// Returns a collection that contains all the subsets of the input set with specified number of elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The input set.</param>
        /// <param name="numSelection">The number of elements in every subset to be generated.</param>
        /// <returns>A collection that contains all the subsets of the input set with specified number of elements.</returns>
        public static IEnumerable<SortedSet<T>> GetSubsets<T>(SortedSet<T> set, int numSelection)
        {
            // Remarks :
            // This method is suitable for the SortedSet<T> class.

            // Get the array format of the set.
            T[] values = set.ToArray();

            // Get all the subsets in a collection with the array format.
            IEnumerable<T[]> combinations = CombinationHelper.Combinations(values, numSelection);

            // Generate the subsets using the array format we computed above.
            LinkedList<SortedSet<T>> subsets = new LinkedList<SortedSet<T>>();
            foreach (T[] combination in combinations)
                subsets.AddLast(new SortedSet<T>(combination));

            // Return the collection that contains all the subset of the original set with specified number of elements.
            return subsets;
        }

        /// <summary>
        /// Returns a collection that contains all the subsets of the input set with specified number of elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The input set.</param>
        /// <param name="numSelection">The number of elements in every subset to be generated.</param>
        /// <returns>A collection that contains all the subsets of the input set with specified number of elements.</returns>
        public static IEnumerable<HashSet<T>> GetSubsets<T>(HashSet<T> set, int numSelection)
        {
            // Remraks:
            // This method is suitable for the HashSet<T> class.

            // Get the array format of the set.
            T[] values = set.ToArray();

            // Get all the subsets in a collection with the array format.
            IEnumerable<T[]> combinations = CombinationHelper.Combinations(values, numSelection);

            // Generate the subsets using the array format we computed above.
            LinkedList<HashSet<T>> subsets = new LinkedList<HashSet<T>>();
            foreach (T[] combination in combinations)
                subsets.AddLast(new HashSet<T>(combination));

            // Return the collection that contains all the subset of the original set with specified number of elements.
            return subsets;
        }

        /// <summary>
        /// Returns true if there is a set in the input collection that contains exact same elements with the input set.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="collection">The collection that contains ISet&lt;T> instances.</param>
        /// <param name="set">The specified set.</param>
        /// <returns>true if there is a set in the input collection that contains exact same elements with the input set.</returns>
        public static bool ContainsSet<T>(IEnumerable<ISet<T>> collection, ISet<T> set)
        {
            // Remarks :
            // This method is suitable for the developer-defined class that implements the ISet<T> interface.

            foreach (ISet<T> s in collection)
            {
                if (s.SetEquals(set))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if there is a set in the input collection that contains exact same elements with the input set.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="collection">The collection that contains ISet&lt;T> instances.</param>
        /// <param name="set">The specified set.</param>
        /// <returns>true if there is a set in the input collection that contains exact same elements with the input set.</returns>
        public static bool ContainsSet<T>(IEnumerable<SortedSet<T>> collection, ISet<T> set)
        {
            // Remarks :
            // This method is suitable for the SortedSet<T> class.

            foreach (ISet<T> s in collection)
            {
                if (s.SetEquals(set))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if there is a set in the input collection that contains exact same elements with the input set.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="collection">The collection that contains ISet&lt;T> instances.</param>
        /// <param name="set">The specified set.</param>
        /// <returns>true if there is a set in the input collection that contains exact same elements with the input set.</returns>
        public static bool ContainsSet<T>(IEnumerable<HashSet<T>> collection, ISet<T> set)
        {
            // Remarks :
            // This method is suitable for the HastSet<T> class.

            foreach (ISet<T> s in collection)
            {
                if (s.SetEquals(set))
                    return true;
            }
            return false;
        }
    }
}