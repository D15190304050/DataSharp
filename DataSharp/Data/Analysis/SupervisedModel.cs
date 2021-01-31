using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharp.Data.Analysis
{
    /// <summary>
    /// The SupervisedModel represents the root class of supervised machine learning algorithms.
    /// </summary>
    [Serializable]
    public abstract class SupervisedModel
    {
        /// <summary>
        /// Saves this model with current state.
        /// </summary>
        /// <param name="filePath">The file path to save this model.</param>
        public abstract void Save(string filePath);

        /// <summary>
        /// Saves check points in the specified directory.
        /// </summary>
        /// <param name="directoryPath">The specified directory.</param>
        public abstract void SaveCheckPoints(string directoryPath);

        // The design of this method is not finished.
        /// <summary>
        /// Trains this model using given features and labels.
        /// </summary>
        public abstract void Train();

        // The design of this method is not finished.
        /// <summary>
        /// Evaluates the accuracy of this model using given features and labels.
        /// </summary>
        public abstract void Evaluate();

        // The design of this method is not finished.
        /// <summary>
        /// Applies this model to the given features to obtain its predictions.
        /// </summary>
        public abstract void Predict();

        /// <summary>
        /// Returns true if current configuration of this model is ready to be trained, otherwise, false.
        /// </summary>
        /// <returns>True if current configuration of this model is ready to be trained, otherwise, false.</returns>
        protected abstract bool CanTrain();
    }
}
