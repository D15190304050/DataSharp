using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    /// <summary>
    /// Provides measures like accuracy for supervised models.
    /// </summary>
    public static class SupervisedPerformanceMeasure
    {
        /// <summary>
        /// Calculates and returns the accuracy of the specified model on the specified test data.
        /// </summary>
        /// <param name="model">The model to measure.</param>
        /// <param name="testSamples">The features of the test data.</param>
        /// <param name="testLabels">The labels of the test data.</param>
        /// <returns>The accuracy of the specified model on the specified test data.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="testSamples" /> or <paramref name="testLabels" /> is null.</exception>
        /// <exception cref="ArgumentException">If <paramref name="testSamples" /> and <paramref name="testLabels" /> have different length.</exception>
        public static double Accuracy(ISupervisedModel model, Vector[] testSamples, Vector[] testLabels)
        {
            if (testSamples == null)
                throw new ArgumentNullException(nameof(testSamples));
            if (testLabels == null)
                throw new ArgumentNullException(nameof(testLabels));
            if (testSamples.Length != testLabels.Length)
                throw new ArgumentException("The specified samples and labels have different lengths", nameof(testSamples));

            int sampleCount = testSamples.Length;
            Vector[] predictedLabels = model.Predict(testSamples);
            double correctCount = 0;
            for (int i = 0; i < sampleCount; i++)
            {
                if (predictedLabels[i].Equals(testSamples[i]))
                    correctCount++;
            }

            return correctCount / sampleCount;
        }


    }
}
