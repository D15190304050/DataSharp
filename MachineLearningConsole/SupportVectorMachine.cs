using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    /// <summary>
    /// The SupportVectorMachine class represents the classic binary classification support vector machine implementation.
    /// </summary>
    public class SupportVectorMachine
    {
        private Matrix dataMatrix;

        private Vector labels;

        private double C;

        private double tolerance;

        private Vector lagrangeMultipliers;
        private double bias;

        private int sampleCount;

        private double[,] errorCache;

        private Vector weights;

        public Func<Vector, Vector, double> Kernel { get; set; }

        public SupportVectorMachine(Matrix dataMatrix, Vector labels, double C, double tolerance)
        {
            if (dataMatrix == null)
                throw new ArgumentNullException("dataMatrix");
            if (labels == null)
                throw new ArgumentNullException("labels");
            if (dataMatrix.RowCount != labels.Count)
                throw new ArgumentException("The number of samples is not equal to the number of labels.");

            //foreach ()

            this.dataMatrix = dataMatrix;
            this.labels = labels;
            this.C = C;
            this.tolerance = tolerance;

            sampleCount = labels.Count;
            lagrangeMultipliers = new Vector(sampleCount);
            bias = 0;
            errorCache = new double[sampleCount, 2];
            weights = new Vector(dataMatrix.ColumnCount);
        }

        /// <summary>
        /// Gets error of k-th sample.
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        private double CalculateError(int k)
        {
            // Calculate the hypothesis of this SVM using current lagrange multipliers.
            double hypothesis = Vector.ElementWiseMultiplication(lagrangeMultipliers, labels) * (dataMatrix * dataMatrix.GetRow(k)) + bias;

            // Calculate the error using the formula
            double error = hypothesis - labels[k];

            return error;
        }

        /// <summary>
        /// Updates the error of k-th sample.
        /// </summary>
        /// <param name="k"></param>
        private void UpdateError(int k)
        {
            double error = CalculateError(k);
            errorCache[k, 0] = 1;
            errorCache[k, 1] = error;
        }

        private int Select2ndMultiplierRandom(int i)
        {
            int j = i;
            while (j == i)
                j = StdRandom.Uniform(sampleCount);
            return j;
        }

        private int Select2ndMultiplier(int i, double errorI, out double errorJ)
        {
            int multiplierIndex = -1;
            double maxDeltaError = 0;
            errorJ = 0;

            errorCache[i, 0] = 1;
            errorCache[i, 1] = errorI;
            LinkedList<int> validErrorCacheIndices = GetNonZeroIndices();

            if (validErrorCacheIndices.Count > 1)
            {
                foreach (int k in validErrorCacheIndices)
                {
                    if (k == i)
                        continue;
                    double errorK = CalculateError(k);
                    double deltaError = Math.Abs(errorI - errorK);

                    if (deltaError > maxDeltaError)
                    {
                        maxDeltaError = deltaError;
                        multiplierIndex = k;
                        errorJ = errorK;
                    }
                }
                if (multiplierIndex != -1)
                    return multiplierIndex;
                else
                {
                    multiplierIndex = Select2ndMultiplierRandom(i);
                    errorJ = CalculateError(multiplierIndex);
                    return multiplierIndex;
                }
            }
            else
            {
                multiplierIndex = Select2ndMultiplierRandom(i);
                errorJ = CalculateError(multiplierIndex);
                return multiplierIndex;
            }
        }

        /// <summary>
        /// Selects 2nd lagrange multiplier and updates them.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        private int InnerLoop(int i, double epsilon = 0.00001)
        {
            double errorI = CalculateError(i);

            if (((labels[i] * errorI < -tolerance) && (lagrangeMultipliers[i] < C)) ||
                ((labels[i] * errorI > tolerance) && (lagrangeMultipliers[i] > 0)))
            {
                int j = Select2ndMultiplier(i, errorI, out double errorJ);
                double multiplierIOld = lagrangeMultipliers[i];
                double multiplierJOld = lagrangeMultipliers[j];

                double upperLimit;
                double lowerLimit;

                if (labels[i] != labels[j])
                {
                    upperLimit = Math.Max(0, lagrangeMultipliers[j] - lagrangeMultipliers[i]);
                    lowerLimit = Math.Min(C, C + lagrangeMultipliers[j] - lagrangeMultipliers[i]);
                }
                else
                {
                    upperLimit = Math.Max(0, lagrangeMultipliers[j] + lagrangeMultipliers[i] - C);
                    lowerLimit = Math.Min(C, lagrangeMultipliers[j] + lagrangeMultipliers[i]);
                }

                // No update if upper limit == lower limit.
                if (Math.Abs(upperLimit - lowerLimit) < double.Epsilon)
                    return 0;

                Vector ithRow = dataMatrix.GetRow(i);
                Vector jthRow = dataMatrix.GetRow(j);
                double k11 = ithRow * ithRow;
                double k12 = ithRow * jthRow;
                double k22 = jthRow * jthRow;
                double eta = k11 + k22 - 2 * k12;

                if (eta <= 0)
                    return 0;

                lagrangeMultipliers[j] = multiplierJOld + labels[j] * (errorI - errorJ) / eta;
                lagrangeMultipliers[j] = Clip(lagrangeMultipliers[j], upperLimit, lowerLimit);
                UpdateError(j);

                if (Math.Abs(lagrangeMultipliers[j] - multiplierJOld) < epsilon)
                    return 0;

                lagrangeMultipliers[i] = multiplierIOld + labels[i] * labels[j] * (multiplierJOld - lagrangeMultipliers[j]);
                UpdateError(i);

                // This will be changed by kernel function.
                double b1 = bias - errorI - labels[i] * (lagrangeMultipliers[i] - multiplierIOld) * k11 - labels[j] * (lagrangeMultipliers[j] - multiplierJOld) * k12;
                double b2 = bias - errorJ - labels[i] * (lagrangeMultipliers[i] - multiplierIOld) * k12 - labels[j] * (lagrangeMultipliers[j] - multiplierJOld) * k22;

                if ((0 < lagrangeMultipliers[i]) && (lagrangeMultipliers[i] < C))
                    bias = b1;
                else if ((0 < lagrangeMultipliers[j]) && (lagrangeMultipliers[j] < C))
                    bias = 2;
                else
                    bias = (b1 + b2) / 2;

                return 1;
            }
            else
                return 0;
        }

        public void SequentialMinOptimization(int maxIterations = 100)
        {
            int iterations = 0;
            int multiplierPairsChanged = 0;
            bool entireSet = true;

            while (((iterations < maxIterations) && (multiplierPairsChanged > 0)) || 
                   (entireSet))
            {
                multiplierPairsChanged = 0;

                if (entireSet)
                {
                    for (int i = 0; i < sampleCount; i++)
                        multiplierPairsChanged += InnerLoop(i);
                    iterations++;
                }
                else
                {
                    LinkedList<int> nonBoundIndices = GetNonBoundIndices();
                    foreach (int i in nonBoundIndices)
                        multiplierPairsChanged += InnerLoop(i);
                    iterations++;
                }

                if (entireSet)
                    entireSet = false;
                else if (multiplierPairsChanged == 0)
                    entireSet = true;
            }

            CalculateWeights();
        }

        private void CalculateWeights()
        {
            for (int i = 0; i < sampleCount; i++)
                weights += lagrangeMultipliers[i] * labels[i] * dataMatrix.GetRow(i);
        }

        public double GetHypothesis(Vector sample)
        {
            if (sample == null)
                throw new ArgumentNullException("sample");
            if (sample.Count != dataMatrix.ColumnCount)
                throw new ArgumentException("The sample must have the same data format as the training set.");

            double hypothesis = sample * weights + bias;
            return hypothesis;
        }

        public int Classify(Vector sample)
        {
            return GetHypothesis(sample) > 0 ? 1 : -1;
        }

        /// <summary>
        /// Returns a LinkedList&lt;T> that contains the indices whose correspond errors are non-zero.
        /// </summary>
        /// <returns>A LinkedList&lt;T> that contains the indices whose correspond errors are non-zero.</returns>
        private LinkedList<int> GetNonZeroIndices()
        {
            LinkedList<int> nonZeroIndices = new LinkedList<int>();
            for (int i = 0; i < errorCache.GetLength(0); i++)
            {
                if (errorCache[i, 0] != 0)
                    nonZeroIndices.AddLast(i);
            }
            return nonZeroIndices;
        }

        /// <summary>
        /// Returns a LinkedList&lt;int> that contains the indices whose correspond lagrange multipliers are non-bound values.
        /// </summary>
        /// <returns></returns>
        private LinkedList<int> GetNonBoundIndices()
        {
            LinkedList<int> nonBoundIndices = new LinkedList<int>();
            for (int i = 0; i < sampleCount; i++)
            {
                if ((lagrangeMultipliers[i] > 0) && (lagrangeMultipliers[i] < C))
                    nonBoundIndices.AddLast(i);
            }
            return nonBoundIndices;
        }

        //private LinkedList<int> GetIndices(Predicate)

        /// <summary>
        /// Clips given multiplier into the specified range.
        /// </summary>
        /// <param name="multiplier">The multiplier to be clipped.</param>
        /// <param name="upperLimit">Upper limit of the specified range.</param>
        /// <param name="lowerLimit">Lower limit of the specified range.</param>
        /// <returns></returns>
        private double Clip(double multiplier, double upperLimit, double lowerLimit)
        {
            if (multiplier > upperLimit)
                multiplier = upperLimit;
            if (multiplier < lowerLimit)
                multiplier = lowerLimit;
            return multiplier;
        }
    }
}