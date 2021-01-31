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
        /// <summary>
        /// The Matrix that contains all the training samples without their labels.
        /// </summary>
        /// <remarks>
        /// Each row of this Matrix is a Vector that contains values of features of each sample.
        /// </remarks>
        private Matrix dataMatrix;

        /// <summary>
        /// Labels of the training set.
        /// </summary>
        private Vector labels;

        /// <summary>
        /// Upper bound.
        /// </summary>
        private double C;

        /// <summary>
        /// Error tolerance.
        /// </summary>
        private double tolerance;

        /// <summary>
        /// The Vector that contains all the lagrange multipliers.
        /// </summary>
        private Vector alphas;

        /// <summary>
        /// Bias of the decision boundary.
        /// </summary>
        private double bias;

        /// <summary>
        /// Number of traning samples.
        /// </summary>
        private int sampleCount;

        /// <summary>
        /// Error cache of SMO algorithm.
        /// </summary>
        private double[,] errorCache;

        /// <summary>
        /// Weights of the decision boundary.
        /// </summary>
        private Vector weights;

        /// <summary>
        /// Kernel function of this SVM.
        /// </summary>
        public IKernel Kernel { get; set; }

        /// <summary>
        /// Values calculated by the given kernel function.
        /// kernelValues[i, j] = kernelValues[j, i] = values calculated by the given kernel function on the i-th and j-th sample.
        /// </summary>
        private Matrix kernelValues;

        private Matrix supportVectors;
        private Vector supportVectorLabels;
        private Vector supportVectorAlphas;

        /// <summary>
        /// Initializes an instance of linear SupportVectorMachine.
        /// </summary>
        /// <param name="dataMatrix">The Matrix that contains all the training samples without their labels.</param>
        /// <param name="labels">Labels of the training set.</param>
        /// <param name="C">Upper bound.</param>
        /// <param name="tolerance">Error tolerance.</param>
        public SupportVectorMachine(Matrix dataMatrix, Vector labels, double C, double tolerance)
        {
            // Check parameters.
            if (dataMatrix == null)
                throw new ArgumentNullException("dataMatrix");
            if (labels == null)
                throw new ArgumentNullException("labels");
            if (dataMatrix.RowCount != labels.Count)
                throw new ArgumentException("The number of samples is not equal to the number of labels.");

            // Make a shallow copy of given training data and hyper parameters.
            this.dataMatrix = dataMatrix;
            this.labels = labels;
            this.C = C;
            this.tolerance = tolerance;

            // Initialize internal data structures.
            sampleCount = labels.Count;
            alphas = new Vector(sampleCount);
            bias = 0;
            errorCache = new double[sampleCount, 2];
            weights = new Vector(dataMatrix.ColumnCount);
            kernelValues = new Matrix(dataMatrix.RowCount, dataMatrix.RowCount);
        }

        /// <summary>
        /// Gets error on k-th sample with current parameters.
        /// </summary>
        /// <param name="k">The index the sampel.</param>
        /// <returns>Error on k-th sample with current parameters.</returns>
        private double CalculateError(int k)
        {
            // Calculate the hypothesis of this SVM using current lagrange multipliers.
            double hypothesis = Vector.ElementWiseMultiplication(alphas, labels) * kernelValues.GetRow(k) + bias;

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

        /// <summary>
        /// Returns a random index different from i.
        /// </summary>
        /// <param name="i">The index of some lagrange multiplier to optimize in an iteration.</param>
        /// <returns>A random index different from i.</returns>
        private int Select2ndMultiplierRandom(int i)
        {
            int j = i;
            while (j == i)
                j = StdRandom.Uniform(sampleCount);
            return j;
        }

        /// <summary>
        /// Returns the index of the 2nd lagrange multiplier to optimize in an iteration.
        /// </summary>
        /// <param name="i">The index of the 1st lagrange multiplier to optimize.</param>
        /// <param name="errorI">Error on i-th sample with current parameters.</param>
        /// <param name="errorJ">Error on j-th sample with current parameters.</param>
        /// <returns>the index of the 2nd lagrange multiplier to optimize in an iteration.</returns>
        private int Select2ndMultiplier(int i, double errorI, out double errorJ)
        {
            // Initialize the index and errors.
            int multiplierIndex = -1;
            double maxDeltaError = 0;
            errorJ = 0;

            // Update error cache.
            errorCache[i, 0] = 1;
            errorCache[i, 1] = errorI;

            // Get the indices of lagrange multipliers which is valid.
            LinkedList<int> validErrorCacheIndices = GetNonZeroIndices();

            // Try to find another lagrange multiplier to optimize if the number of valid lagrange multipliers is larger than 1.
            // Otherwise, select a random lagrange multiplier different from i to optimize.
            if (validErrorCacheIndices.Count > 1)
            {
                // Tranverse through all valid indices.
                foreach (int k in validErrorCacheIndices)
                {
                    // The indices of 2 lagrange multipliers to optimize must be un-equal.
                    if (k == i)
                        continue;

                    // Calculate error and delta-error on k-th sample.
                    double errorK = CalculateError(k);
                    double deltaError = Math.Abs(errorI - errorK);

                    // Keep tracking the potential lagrange multiplier with maximum delta error.
                    if (deltaError > maxDeltaError)
                    {
                        maxDeltaError = deltaError;
                        multiplierIndex = k;
                        errorJ = errorK;
                    }
                }

                // Return the index of the 2nd lagrange multiplier if find one.
                // Otherwise, select a random lagrange multiplier different from i to optimize.
                if (multiplierIndex != -1)
                    return multiplierIndex;
                else
                {
                    multiplierIndex = Select2ndMultiplierRandom(i);
                    errorJ = CalculateError(multiplierIndex);
                    //return multiplierIndex;
                    return alphas.Count - 1;
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
            // Get error on i-th sample with current parameters.
            double errorI = CalculateError(i);

            // Determine whether i-th sample is a support vector.
            if (((labels[i] * errorI < -tolerance) && (alphas[i] < C)) ||
                ((labels[i] * errorI > tolerance) && (alphas[i] > 0)))
            {
                // Get the index of the 2nd lagrange multiplier to optimize.
                int j = Select2ndMultiplier(i, errorI, out double errorJ);

                // Cache the old value of the 2 lagrange multipliers.
                double alphaIOld = alphas[i];
                double alphaJOld = alphas[j];

                // Calculate upper limit and lower limit of 2nd lagrange multiplier.
                double upperLimit;
                double lowerLimit;
                if (labels[i] != labels[j])
                {
                    upperLimit = Math.Max(0, alphas[j] - alphas[i]);
                    lowerLimit = Math.Min(C, C + alphas[j] - alphas[i]);
                }
                else
                {
                    upperLimit = Math.Max(0, alphas[j] + alphas[i] - C);
                    lowerLimit = Math.Min(C, alphas[j] + alphas[i]);
                }

                // No update if upper limit == lower limit.
                if (Math.Abs(upperLimit - lowerLimit) < double.Epsilon)
                    return 0;

                // Calculate values of k11, k12, k22, eta.
                // This will be cahnged by kernel function.
                Vector ithRow = dataMatrix.GetRow(i);
                Vector jthRow = dataMatrix.GetRow(j);
                double k11 = kernelValues[i, i];
                double k12 = kernelValues[i, j];
                double k22 = kernelValues[j, j];
                double eta = k11 + k22 - 2 * k12;

                // No optimization if eta <= 0.
                if (eta <= 0)
                    return 0;

                // Calculate new value of lagrangeMultipliers[j] and update error on j-th sample.
                alphas[j] = alphaJOld + labels[j] * (errorI - errorJ) / eta;
                alphas[j] = Clip(alphas[j], upperLimit, lowerLimit);
                UpdateError(j);

                // No optimization if the change of j-th lagrange multiplier is not large enough.
                if (Math.Abs(alphas[j] - alphaJOld) < epsilon)
                    return 0;

                // Calculate new value of lagrangeMultipliers[i] and update error on i-th sample.
                alphas[i] = alphaIOld + labels[i] * labels[j] * (alphaJOld - alphas[j]);
                UpdateError(i);

                // This will be changed by kernel function.
                double b1 = bias - errorI - labels[i] * (alphas[i] - alphaIOld) * k11 - labels[j] * (alphas[j] - alphaJOld) * k12;
                double b2 = bias - errorJ - labels[i] * (alphas[i] - alphaIOld) * k12 - labels[j] * (alphas[j] - alphaJOld) * k22;

                // Update bias using formula in the paper.
                if ((0 < alphas[i]) && (alphas[i] < C))
                    bias = b1;
                else if ((0 < alphas[j]) && (alphas[j] < C))
                    bias = 2;
                else
                    bias = (b1 + b2) / 2;

                // Return 1 to indicate that there is a pair of lagrange multipliers that has been optimized.
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// Training this SVM using the SMO algorithm.
        /// </summary>
        /// <param name="maxIterations">Max count of iterations.</param>
        /// <exception cref="ArgumentNullException">If Kernel is null.</exception>
        public void Train(int maxIterations = 100)
        {
            if (Kernel == null)
                throw new ArgumentNullException("Kernel");

            for (int i = 0; i < dataMatrix.RowCount; i++)
                kernelValues.SetColumn(Kernel.KernelTransform(dataMatrix, dataMatrix.GetRow(i)), i);

            // Iteration count starts from 0.
            int iterations = 0;

            // The counter of how many pairs of lagrange multipliers changed in an iteration.
            int alphaPairsChanged = 0;

            // The boolean flag that indicate whether last iteration traversed the entire traning set or not.
            bool entireSet = true;

            // Iterate until converge or reach maxIterations.
            while (((iterations < maxIterations) && (alphaPairsChanged > 0)) || 
                   (entireSet))
            {
                // No lagrange multiplier changed at the beginning of this iteration.
                alphaPairsChanged = 0;

                //
                if (entireSet)
                {
                    for (int i = 0; i < sampleCount; i++)
                        alphaPairsChanged += InnerLoop(i);
                    iterations++;
                }
                else
                {
                    LinkedList<int> nonBoundIndices = GetNonBoundIndices();
                    foreach (int i in nonBoundIndices)
                        alphaPairsChanged += InnerLoop(i);
                    iterations++;
                }

                if (entireSet)
                    entireSet = false;
                else if (alphaPairsChanged == 0)
                    entireSet = true;
            }

            // Calculate weights of this SVM.
            CalculateSupportVectors();
        }

        /// <summary>
        /// Calculates weights of this SVM using formula in the paper.
        /// </summary>
        private void CalculateSupportVectors()
        {
            for (int i = 0; i < sampleCount; i++)
                weights += alphas[i] * labels[i] * dataMatrix.GetRow(i);

            LinkedList<int> nonZeroIndices = new LinkedList<int>();
            for (int i = 0; i < alphas.Count; i++)
            {
                if (alphas[i] > 0)
                    nonZeroIndices.AddLast(i);
            }

            supportVectors = new Matrix(nonZeroIndices.Count, dataMatrix.ColumnCount);
            supportVectorLabels = new Vector(nonZeroIndices.Count);
            supportVectorAlphas = new Vector(nonZeroIndices.Count);
            int rowCount = 0;
            foreach (int i in nonZeroIndices)
            {
                supportVectorLabels[rowCount] = labels[i];
                supportVectors.SetRow(dataMatrix.GetRow(i), rowCount);
                supportVectorAlphas[rowCount] = alphas[i];
                rowCount++;
            }
        }

        /// <summary>
        /// Returns the hypothesis of this SVM on given sample.
        /// </summary>
        /// <param name="sample">The given sample.</param>
        /// <returns>The hypothesis of this SVM on given sample.</returns>
        public double GetHypothesis(Vector sample)
        {
            if (sample == null)
                throw new ArgumentNullException("sample");
            if (sample.Count != dataMatrix.ColumnCount)
                throw new ArgumentException("The sample must have the same data format as the training set.");

            Vector kernelValues = Kernel.KernelTransform(supportVectors, sample);
            double hypothesis = kernelValues * Vector.ElementWiseMultiplication(supportVectorLabels, supportVectorAlphas) + bias;

            return hypothesis;
        }

        /// <summary>
        /// Returns the class label of the given sample classfied by this SVM.
        /// </summary>
        /// <param name="sample">The given sample.</param>
        /// <returns>The class label of the given sample classfied by this SVM.</returns>
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
                if ((alphas[i] > 0) && (alphas[i] < C))
                    nonBoundIndices.AddLast(i);
            }
            return nonBoundIndices;
        }

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