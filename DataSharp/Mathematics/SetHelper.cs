using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharp.Mathematics
{
    /// <summary>
    /// The SetHelper class provides static method for supporting some frequent set operations.
    /// </summary>
    public static class SetHelper
    {
        /// <summary>
        /// Returns a collection that contains all the subsets of the input set with specified number of elements.
        /// </summary>
        /// <remarks>
        /// This method is suitable for the developer-defined class that implements the ISet&lt;T> interface.
        /// </remarks>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The input set.</param>
        /// <param name="numSelection">The number of elements in every subset to be generated.</param>
        /// <returns>A collection that contains all the subsets of the input set with specified number of elements.</returns>
        public static IEnumerable<ISet<T>> GetSubsets<T>(ISet<T> set, int numSelection)
        {
            // Check length before processing.
            LengthCheck(set.Count, numSelection);

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
        /// <remarks>
        /// This method is suitable for the SortedSet&lt;T> class.
        /// </remarks>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The input set.</param>
        /// <param name="numSelection">The number of elements in every subset to be generated.</param>
        /// <returns>A collection that contains all the subsets of the input set with specified number of elements.</returns>
        public static IEnumerable<SortedSet<T>> GetSubsets<T>(SortedSet<T> set, int numSelection)
        {
            // Check length before processing.
            LengthCheck(set.Count, numSelection);

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
        /// <remarks>
        /// This method is suitable for the HashSet&lt;T> class.
        /// </remarks>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The input set.</param>
        /// <param name="numSelection">The number of elements in every subset to be generated.</param>
        /// <returns>A collection that contains all the subsets of the input set with specified number of elements.</returns>
        public static IEnumerable<HashSet<T>> GetSubsets<T>(HashSet<T> set, int numSelection)
        {
            // Check length before processing.
            LengthCheck(set.Count, numSelection);

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
        /// <remarks>
        /// This method is suitable for the developer-defined class that implements the ISet&lt;T> interface.
        /// </remarks>
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
        /// <remarks>
        /// This method is suitable for the SortedSet&lt;T> class.
        /// </remarks>
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
        /// <remarks>
        /// This method is suitable for the HashSet&lt;T> class.
        /// </remarks>
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

        /// <summary>
        /// Returns all the subsets of the input set except the empty set and the set itself in an enumerator.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">A set that contains some elements.</param>
        /// <returns>All the subsets of the input set except the empty set and the set itself in an enumerator.</returns>
        public static IEnumerable<ISet<T>> GetAllSubsets<T>(ISet<T> set)
        {
            // Initialize an empty linked list to store all the subsets of the input set.
            LinkedList<ISet<T>> subsets = new LinkedList<ISet<T>>();

            // Generate all the subsets with i elements, where i = 1, 2, ..., set.Count - 1
            for (int i = 1; i < set.Count; i++)
            {
                // Get the subsets and add them to the LinkedList<ISet<T>>.
                foreach (ISet<T> s in GetSubsets(set, i))
                    subsets.AddLast(s);
            }

            // Return the subsets.
            return subsets;
        }

        /// <summary>
        /// Returns the complement of the subset in the complete set.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The complete set.</param>
        /// <param name="subset">A subset of the complete set.</param>
        /// <returns>The complement of the subset in the complete set.</returns>
        public static ISet<T> GetComplement<T>(ISet<T> set, ISet<T> subset)
        {
            // Throw an exception if the input sets are not correct.
            if (!set.IsSupersetOf(subset))
                throw new ArgumentException("Errors occured in input sets.");

            // Make a copy of the complete set because the ExceptWith() method will change the set itself.
            SortedSet<T> complement = new SortedSet<T>(set);

            // Remove all the element in the given subset from the complete set so that the complement will be the set of elements remained.
            complement.ExceptWith(subset);

            // Return the complete
            return complement;
        }

        /// <summary>
        /// Throws ArgumentException if the count of the input set is less than the number of elements to be selected.
        /// </summary>
        /// <param name="setLength">The number of elements in the input set.</param>
        /// <param name="numSelection">The number of elements in every subset to be generated.</param>
        private static void LengthCheck(int setLength, int numSelection)
        {
            if (setLength < numSelection)
                throw new ArgumentException("The number of elements in the input set must be equal to or greater than the number of elements in its subset(s).");
        }
    }
}