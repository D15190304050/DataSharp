using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public class Model
    {
        // Unsolved:
        // * Accuracy(): returns the accuracy of the model on the given test data.
        // * CrossValidate(X, y, numFolds): apply cross validation to the entire data set to get the performance.
        // * Evaluate(): apply the specified evaluation on the given dataset on this model, such as accuracy, recall, f1-score, precision and so on.

        // * class DictionaryVectorizer: transform a dictionary to a feature matrix with one-hot encoding for categorical features.
        //   - FeatureNames { get; }
        //   - Fit(featureDictionary)

        // * class CountVectorizer: text word count

        // TF-IDF

        // Fit the model using the training data.

        // class ProbabilityClassifier
        // * PredictProbability(samples): probabilityOfSample 

        // class NaiveBayesClassifier
        // class GaussianNaiveBayesClassifier

        public void Fit()
        {
        }

        // Predict the label of the new input data.
        public void Predict()
        {
        }

        public double Accuracy()
        {
            return 0;
        }
    }
}
