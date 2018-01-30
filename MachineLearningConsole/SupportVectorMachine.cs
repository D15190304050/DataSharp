using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
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

        public Func<Vector, Vector, double> Kernel { get; set; }

        public SupportVectorMachine(Matrix dataMatrix, Vector labels, double C, double tolerance)
        {
            if (dataMatrix == null)
                throw new ArgumentNullException("dataMatrix");
            if (labels == null)
                throw new ArgumentNullException("labels");
            if (dataMatrix.RowCount != labels.Count)
                throw new ArgumentException("The number of samples is not equal to the number of labels.");

            this.dataMatrix = dataMatrix;
            this.labels = labels;
            this.C = C;
            this.tolerance = tolerance;

            sampleCount = labels.Count;
            lagrangeMultipliers = new Vector(sampleCount);
            bias = 0;
            errorCache = new double[sampleCount, 2];
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

        }
    }
}