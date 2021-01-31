using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public static class MathUtil
    {
        public static T Max<T>(T[] array, out int indexOfMax) where T : IComparable<T>
        {
            if (array == null)
                throw new ArgumentNullException("The input array is null.");

            indexOfMax = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(array[indexOfMax]) > 0)
                    indexOfMax = i;
            }
            return array[indexOfMax];
        }

        public static T Max<T>(T[] array) where T : IComparable<T>
        {
            if (array == null)
                throw new ArgumentNullException("The input array is null.");

            int indexOfMax = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(array[indexOfMax]) > 0)
                    indexOfMax = i;
            }
            return array[indexOfMax];
        }

        public static T Min<T>(T[] array, out int indexOfMin) where T : IComparable<T>
        {
            if (array == null)
                throw new ArgumentNullException("The input array is null.");

            indexOfMin = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(array[indexOfMin]) < 0)
                    indexOfMin = i;
            }
            return array[indexOfMin];
        }

        public static T Min<T>(T[] array) where T : IComparable<T>
        {
            if (array == null)
                throw new ArgumentNullException("The input array is null.");

            int indexOfMin = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(array[indexOfMin]) > 0)
                    indexOfMin = i;
            }
            return array[indexOfMin];
        }

        public static double Mean(double[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            double sum = 0;
            foreach (double x in array)
                sum += x;
            return sum / array.Length;
        }

        /// <summary>
        /// Returns the index of selected element using roulette selection algorithm.
        /// </summary>
        /// <param name="probabilities">Probabilities of every element.</param>
        /// <param name="epsilon">Upper limit of round-off error.</param>
        /// <returns>The index of selected element using roulette selection algorithm.</returns>
        /// <exception cref="ArgumentNullException">If <param name="probabilities" /> is null.</exception>
        /// <exception cref="ArgumentException">If <param name="probabilities" /> is not the normalized discrete probabilities.</exception>
        public static int RouletteSelection(double[] probabilities, double epsilon = 1e-12)
        {
            // Check if probabilities is null.
            if (probabilities == null)
                throw new ArgumentNullException("probabilities");

            // Check if probabilites is normalized discrete probabilites.
            double sum = 0;
            foreach (double p in probabilities)
            {
                if ((p < 0) || (p > 1))
                    throw new ArgumentException("Probability of an event must be in [0, 1].");
                sum += p;
            }
            if (Math.Abs(sum - 1) > epsilon)
                throw new ArgumentException("Sum of probabilities of each case must be 1.");

            // Get the number of elements.
            int elementCount = probabilities.Length;

            // Calculate cumulative probabilites of given elements.
            double[] cumulativeProbabilities = new double[elementCount];
            cumulativeProbabilities[0] = probabilities[0];
            for (int i = 1; i < elementCount; i++)
                cumulativeProbabilities[i] = cumulativeProbabilities[i - 1] + probabilities[i];

            // Generate a random double value uniformly distributed in [0, 1].
            double selection = StdRandom.Uniform();

            // Get and return the index of the selected element.
            int selectedIndex = -1;
            for (int i = 0; i < elementCount; i++)
            {
                if (selection <= cumulativeProbabilities[i])
                {
                    selectedIndex = i;
                    break;
                }
            }
            return selectedIndex;
        }
    }
}
