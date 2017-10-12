using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public static class CombinationHelper
    {
        /// <summary>
        /// Generate all the combinations with the specified number of total elements and the number of elements to be
        /// extracted.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="a">The array that contains all the candidate elements.</param>
        /// <param name="numSelection">The number of elements to be extracted.</param>
        /// <returns>
        /// All the combinations with the specified number of total elements and the number of elements to be extracted.
        /// </returns>
        public static IEnumerable<T[]> Combinations<T>(T[] a, int numSelection)
        {
            // Remark :
            // This method should be tested using reference types, so that some kind of errors will indicates the bug.

            // Initialize an empty linked list to store all the combinations.
            LinkedList<T[]> combinations = new LinkedList<T[]>();

            // Compute all the possible combinations and store them in the linked list we initialized before.
            Combinations(a, numSelection, combinations);

            // Return all the combinations we computed above.
            return combinations;
        }

        /// <summary>
        /// Generate all the combinations with the specified number of total elements and the number of elements to be
        /// extracted.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="a">The array that contains all the candidate elements.</param>
        /// <param name="numSelection">The number of elements to be extracted.</param>
        /// <param name="combinations">The collection of all the combinations that are needed to be extracted.</param>
        private static void Combinations<T>(T[] a, int numSelection, LinkedList<T[]> combinations)
        {
            // Remark :
            // This method should be tested using reference types, so that some kind of errors will indicates the bug.

            // combination is an int array that indicates conbination.
            // combination[i] == 1 means that the element in the input array with index i is selected.
            // combination[i] == 0 means that the element in the input array with index i is not selected.
            int[] combination = new int[a.Length];
            for (int i = 0; i < numSelection; i++)
                combination[i] = 1;

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
        /// Returns true if the caller can generate next combination, false otherwise.
        /// </summary>
        /// <param name="combination">The current combination.</param>
        /// <param name="numSelection">The number of elements to be extracted.</param>
        /// <returns>Returns true if the caller can generate next combination, false otherwise.</returns>
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
        /// <typeparam name="T">The type of elements in the array.</typeparam>
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
