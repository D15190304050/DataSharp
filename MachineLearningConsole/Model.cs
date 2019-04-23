using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    public abstract class Model
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

        // Model MakePipeline(): like make_pipeline of the sklearn (sklearn.pipeline.make_pipeline) package of python
        // Support Gaussian basis function for linear regression.

        // SVM
        // * public IEnumerable<Vector> SupportVectors { get; }

        // public abstract class Classifier : Model
        // * ClassificationReport()

        // public class CrossValidation
        // * TrainTestSplit(data, labels, double trainingPortion, int validationFolds, int randomSeed) => (trainingData, testData, trainingLabels, testLabels)

        // public class RandomNumberGenerator
        // * public RandomNumberGenerator(int seed)

        // public class PrincipalComponentAnalysis
        // * public PrincipalComponentAnalysis(int components)
        // * public PrincipalComponentAnalysis(double keepRatio) => how many variance should be preserved
        // * public void Fit(data)
        // * compute the cumulative explained variance.
        // * public Vector[] InverseTransform(Vector[] compressedData) => returns the data re-constructed by PCA.
        // * public Vector[] Transform(Vector[] data) => returns the compressed data.

        // namespace MachineLearning.Datasets
        // * Provides simple open datasets for machine learning.
        // * Or maybe this can be a simple class => public class Datasets
        // * public string Info(): get a quick description of the data.
        // * public void PlotHistogram(): plot histogram for each numerical attribute of the dataset.

        // public class GridSearch
        // Performing grid search to find the best hyper-parameter set from the given range.

        // public class GridSearchCrossValidation
        // Performing grid search to find the best hyper-parameter set from the given range by cross validation.

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
