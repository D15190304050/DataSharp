using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    /// <summary>
    /// The ArtificialBeeColony class implements the artificial bee colony (ABC) algorithm.
    /// </summary>
    public class ArtificialBeeColony
    {
        /// <summary>
        /// Scale of population.
        /// </summary>
        private int populationScale;

        /// <summary>
        /// Number of food source.
        /// </summary>
        private int numFoodSource;

        /// <summary>
        /// Number of updates.
        /// </summary>
        private int limit;

        /// <summary>
        /// lowerBound[i] = lower bound of i-th entry of the solution vector.
        /// </summary>
        private double[] lowerBounds;

        /// <summary>
        /// upperBound[i] = lower bound of i-th entry of the solution vector.
        /// </summary>
        private double[] upperBounds;

        /// <summary>
        /// 
        /// </summary>
        //private double result;

        /// <summary>
        /// The Bee class represents a bee in the ABC algorithm.
        /// </summary>
        private class Bee
        {
            /// <summary>
            /// The solution vector.
            /// </summary>
            public Vector Solution { get; set; }

            /// <summary>
            /// Function value of current solution vector.
            /// </summary>
            public double Value { get; set; }

            /// <summary>
            /// Fitness of current solution vector.
            /// </summary>
            public double Fitness { get; set; }

            /// <summary>
            /// Probability of being chosen according to to fitness of this solution.
            /// </summary>
            public double FitnessRatio { get; set; }

            /// <summary>
            /// Number of updates of current solution vector.
            /// </summary>
            public int Trail { get; set; }

            /// <summary>
            /// Initializes a new data structure for the solution of the ABC algorithm.
            /// </summary>
            /// <param name="dimensions">Number of components in the solution vector.</param>
            public Bee(int dimensions)
            {
                this.Solution = new Vector(dimensions);
            }
        }

        /// <summary>
        /// Nectar sources.
        /// </summary>
        private Bee[] nectarSource;

        /// <summary>
        /// Employed bees.
        /// </summary>
        private Bee[] employed;

        /// <summary>
        /// On-lookers.
        /// </summary>
        private Bee[] onLookers;

        /// <summary>
        /// The best solution.
        /// </summary>
        private Bee bestSolution;

        /// <summary>
        /// Number of dimensions of the solution vector.
        /// </summary>
        private int dimensions;

        /// <summary>
        /// Maximum number of iterations.
        /// </summary>
        private int maxIterations;

        /// <summary>
        /// Objective function.
        /// </summary>
        public Func<Vector, double> ObjectiveFunction { get; set; }

        /// <summary>
        /// Initializes a new solver for the artificial bee colony algorithm.
        /// </summary>
        /// <param name="populationScale"></param>
        /// <param name="limit"></param>
        /// <param name="maxIterations"></param>
        /// <param name="dimensions"></param>
        /// <param name="lowerBounds"></param>
        /// <param name="upperBounds"></param>
        public ArtificialBeeColony(int populationScale = 10, int limit = 10, int maxIterations = 40, int dimensions = 0, double[] lowerBounds = null, double[] upperBounds = null)
        {
            this.populationScale = populationScale;
            this.numFoodSource = populationScale / 2;
            this.limit = limit;
            this.maxIterations = maxIterations;
            this.dimensions = dimensions;
            this.lowerBounds = lowerBounds;
            this.upperBounds = upperBounds;

            nectarSource = new Bee[numFoodSource];
            for (int i = 0; i < numFoodSource; i++)
                nectarSource[i] = new Bee(dimensions);
            employed = new Bee[numFoodSource];
            for (int i = 0; i < numFoodSource; i++)
                employed[i] = new Bee(dimensions);
            onLookers = new Bee[numFoodSource];
            for (int i = 0; i < numFoodSource; i++)
                onLookers[i] = new Bee(dimensions);

            bestSolution = new Bee(dimensions);
        }

        public double Solve(out Vector solution)
        {
            solution = null;
            if (!CanRun())
                return double.PositiveInfinity;

            Initialize();
            for (int i = 0; i < maxIterations; i++)
            {
                SendEmployedBees();
                UpdateFitnessRatio();
                SendOnLooerBees();
                MemorizeBestSolution();
            }

            solution = bestSolution.Solution;
            return bestSolution.Value;
        }

        /// <summary>
        /// Returns true if current configuration is suitable to run the artificial bee colony algorithm, otherwise, false.
        /// </summary>
        /// <returns>True if current configuration is suitable to run the artificial bee colony algorithm, otherwise, false.</returns>
        private bool CanRun()
        {
            if (this.ObjectiveFunction == null)
                return false;
            else if (populationScale <= 0)
                return false;
            else if (limit <= 1)
                return false;
            else if (dimensions <= 0)
                return false;
            else if (maxIterations <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// Initializez internal data structures for the artificial bee colony algorithm.
        /// </summary>
        private void Initialize()
        {
            for (int i = 0; i < numFoodSource; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    double r = StdRandom.Uniform(lowerBounds[j], upperBounds[j]);
                    nectarSource[i].Solution[j] = r;
                    employed[i].Solution[j] = r;
                    onLookers[i].Solution[j] = r;
                }

                // Initialize nectar sources.
                nectarSource[i].Value = ObjectiveFunction(nectarSource[i].Solution);
                nectarSource[i].Fitness = CalculateFitness(nectarSource[i].Value);
                nectarSource[i].FitnessRatio = 0;
                nectarSource[i].Trail = 0;

                // Initialize employed bees.
                employed[i].Value = nectarSource[i].Value;
                employed[i].Fitness = nectarSource[i].Fitness;
                employed[i].FitnessRatio = nectarSource[i].FitnessRatio;
                employed[i].Trail = nectarSource[i].Trail;

                // Initialize on-lookers.
                onLookers[i].Value = nectarSource[i].Value;
                onLookers[i].Fitness = nectarSource[i].Fitness;
                onLookers[i].FitnessRatio = nectarSource[i].FitnessRatio;
                onLookers[i].Trail = nectarSource[i].Trail;
            }

            // Initialize best solution.
            for (int j = 0; j < dimensions; j++)
                bestSolution.Solution[j] = nectarSource[0].Solution[j];
            bestSolution.Value = nectarSource[0].Value;
            bestSolution.Fitness = nectarSource[0].Fitness;
            bestSolution.FitnessRatio = nectarSource[0].FitnessRatio;
            bestSolution.Trail = nectarSource[0].Trail;
        }

        /// <summary>
        /// Calculates fitness for some function value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double CalculateFitness(double value)
        {
            if (value >= 0)
                return 1 / (value + 1);
            else
                return 1 + Math.Abs(value);
        }

        /// <summary>
        /// Updates employed bees.
        /// </summary>
        private void SendEmployedBees()
        {
            for (int i = 0; i < numFoodSource; i++)
            {
                // Randomly pick a dimension to change.
                int param2Change = StdRandom.Uniform(0, dimensions);

                // Randomly pick a different solution.
                int k = StdRandom.Uniform(0, numFoodSource);
                for (; ; )
                {
                    if (k != i)
                        break;
                    k = StdRandom.Uniform(0, numFoodSource);
                }

                // Copy i-th solution.
                for (int j = 0; j < dimensions; j++)
                    employed[i].Solution[j] = nectarSource[i].Solution[j];

                // Try to find another solution. i.e. Find a new food source.
                double r = StdRandom.Uniform(-1.0, 1.0);
                employed[i].Solution[param2Change] = nectarSource[i].Solution[param2Change] + r * (nectarSource[i].Solution[param2Change] - nectarSource[k].Solution[param2Change]);

                // Clip to bounds.
                employed[i].Solution[param2Change] = Clip(employed[i].Solution[param2Change], lowerBounds[param2Change], upperBounds[param2Change]);

                // Calculate value and fitness for the new solution.
                employed[i].Value = ObjectiveFunction(employed[i].Solution);
                employed[i].Fitness = CalculateFitness(employed[i].Value);

                // Choose a better solution greedily.
                if (employed[i].Value < nectarSource[i].Value)
                {
                    for (int j = 0; j < dimensions; j++)
                        nectarSource[i].Solution[j] = employed[i].Solution[j];
                    nectarSource[i].Trail = 0;
                    nectarSource[i].Value = employed[i].Value;
                    nectarSource[i].Fitness = employed[i].Fitness;
                }
                else
                    nectarSource[i].Trail++;
            }
        }

        /// <summary>
        /// Updates fitness ratio for every nectar source.
        /// </summary>
        private void UpdateFitnessRatio()
        {
            // Get the max fitness.
            // Note: this may be changed by the probability formula in the paper.
            double maxFitness = nectarSource[0].Fitness;
            for (int i = 1; i < numFoodSource; i++)
            {
                if (nectarSource[i].Fitness > maxFitness)
                    maxFitness = nectarSource[i].Fitness;
            }

            // Update probability of being chosen using max fitness.
            for (int i = 0; i < numFoodSource; i++)
                nectarSource[i].FitnessRatio = (0.9 * (nectarSource[i].Fitness / maxFitness)) + 0.1;
        }

        /// <summary>
        /// Updates solutions of on-lookers.
        /// </summary>
        private void SendOnLooerBees()
        {
            int onLookersChanged = 0;
            int i = 0;
            while (onLookersChanged < numFoodSource)
            {
                // Get the probability of choosing i-th on-looker.
                double pChosen = StdRandom.Uniform(0.0, 1.0);

                // Change i-th on-looker if pChosen < fitness ration of i-th solution.
                if (pChosen < nectarSource[i].FitnessRatio)
                {
                    int param2Change = StdRandom.Uniform(0, dimensions);

                    // Update the counter of changes.
                    onLookersChanged++;

                    // Randomly pick a different solution.
                    int k = StdRandom.Uniform(0, numFoodSource);
                    for (; ; )
                    {
                        if (k != i)
                            break;
                        k = StdRandom.Uniform(0, numFoodSource);
                    }

                    // Copy i-th solution.
                    for (int j = 0; j < dimensions; j++)
                        onLookers[i].Solution[j] = nectarSource[i].Solution[j];

                    // Try to find another solution. i.e. Find a new food source.
                    double r = StdRandom.Uniform(-1.0, 1.0);
                    onLookers[i].Solution[param2Change] = nectarSource[i].Solution[param2Change] + r * (nectarSource[i].Solution[param2Change] - nectarSource[k].Solution[param2Change]);

                    // Clip.
                    onLookers[i].Solution[param2Change] = Clip(onLookers[i].Solution[param2Change], lowerBounds[param2Change], upperBounds[param2Change]);

                    // Choose a better solution greedily.
                    if (onLookers[i].Value < nectarSource[i].Value)
                    {
                        for (int j = 0; j < dimensions; j++)
                            nectarSource[i].Solution[j] = employed[i].Solution[j];
                        nectarSource[i].Trail = 0;
                        nectarSource[i].Value = employed[i].Value;
                        nectarSource[i].Fitness = employed[i].Fitness;
                    }
                    else
                        nectarSource[i].Trail++;
                }

                i = (i + 1) % numFoodSource;
            }
        }

        /// <summary>
        /// Clip given value to given range.
        /// </summary>
        /// <param name="value">The value to clip.</param>
        /// <param name="lowerLimit">Lower limit of the variable.</param>
        /// <param name="upperLimit">Upper limit of the variable.</param>
        /// <returns>Clipped value of the given value.</returns>
        private double Clip(double value, double lowerLimit, double upperLimit)
        {
            if (value < lowerLimit)
                value = lowerLimit;
            if (value > upperLimit)
                value = upperLimit;
            return value;
        }

        /// <summary>
        /// Send a scout to find a new candidate solution.
        /// </summary>
        private void SendScoutBees()
        {
            // Get the index of the solution vector with max trail count.
            int indexOfMaxTrail = 0;
            for (int i = 1; i < numFoodSource; i++)
            {
                if (nectarSource[i].Trail > nectarSource[indexOfMaxTrail].Trail)
                    indexOfMaxTrail = i;
            }

            // Generate a new random solution if current solution vector has been optimized for limit times.
            if (nectarSource[indexOfMaxTrail].Trail >= limit)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    double r = StdRandom.Uniform(0.0, 1.0);
                    nectarSource[indexOfMaxTrail].Solution[j] = lowerBounds[j] + r * (upperBounds[j] - lowerBounds[j]);
                }
                nectarSource[indexOfMaxTrail].Value = ObjectiveFunction(nectarSource[indexOfMaxTrail].Solution);
                nectarSource[indexOfMaxTrail].Fitness = CalculateFitness(nectarSource[indexOfMaxTrail].Value);
                nectarSource[indexOfMaxTrail].Trail = 0;
            }
        }

        /// <summary>
        /// Memorizes best solution at present.
        /// </summary>
        private void MemorizeBestSolution()
        {
            // Get the index of the best solution in the nectarSource array.
            int indexOfBest = 0;
            for (int i = 1; i < numFoodSource; i++)
            {
                if (nectarSource[i].Value < nectarSource[indexOfBest].Value)
                    indexOfBest = i;
            }

            // Update the best solution.
            if (nectarSource[indexOfBest].Value < bestSolution.Value)
            {
                for (int j = 0; j < dimensions; j++)
                    bestSolution.Solution[j] = nectarSource[indexOfBest].Solution[j];
                bestSolution.Value = nectarSource[indexOfBest].Value;
            }
        }
    }
}
