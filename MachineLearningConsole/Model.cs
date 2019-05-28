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

        // Methods for the Model class.
        // * Fit(trainingFeatures, trainingLabels)
        // Fit the model using the training data.
        // * EvaluationResult Evaluate(testFeatures, testLabels, EvaluationOptions)
        // [Flag] EvaluationOptions, including accuracy, mean squared error, mean absolute error, precision and other options.
        // EvaluationResult: an entity class, you can get properties of corresponding evaluation metrics.
        // * public void NameScope(string name): Generate a scope (block) with the unified name, all the components in the same name scope will be shown as an entire block in tensorboard.
        // Animation toggle for data flow can be triggered.
        // All visualization result shown in browser are maintained by JavaScript, with over 20k lines of code.
        // Using browser instead of a windowed program is for cross-platform.

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

        // Callback functions for model training process.
        // This can be delegate, though in python it is a class implemented an interface.
        // * OnEpochBegin()
        // * OnEpochEnd()

        // public class Summary
        // Provides methods for summary like TF summary.
        // * Histogram() like tensorboard.
        // * Scalar like tensorboard.
        // * Computational graph.
        // * Draw graph in the browser.

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

        // Question: which one is better: property or method.
        /// <summary>
        /// Returns the summary information of this model.
        /// </summary>
        /// <returns></returns>
        public string Summary()
        {
            return "";
        }

        public void Save(string modelPath)
        {

        }

        public static Model Restore(string modelPath)
        {
            return null;
        }
    }
}
