using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningConsole
{
    /// <summary>
    /// The AssociationRule class represents the associatoin rule in the domain of data mining.
    /// </summary>
    /// <remarks>
    /// The association rule is in the format "x => (y - x)", where y is one of the frequent k-itemsets with the largest k, x is a non-empty proper subset of y.
    /// </remarks>
    public class AssociationRule
    {
        /// <summary>
        /// The string representation of the itemset x.
        /// </summary>
        /// <remarks>
        /// x is a non-empty proper subset of y.
        /// </remarks>
        public string X { get; }

        /// <summary>
        /// The string representation of the itemset y.
        /// </summary>
        /// <remarks>
        /// y is one of the frequent k-itemsets with the largest k.
        /// </remarks>
        public string Y { get; }

        /// <summary>
        /// The confidence of the association rule "x => (y - x)".
        /// </summary>
        public double Confidence { get; }

        public string ConfidenceString
        {
            get { return string.Format("{0:0.}%", Confidence * 100); }
        }

        /// <summary>
        /// The frequency count of itemset x in the data table.
        /// </summary>
        /// <remarks>
        /// x is a non-empty proper subset of y.
        /// </remarks>
        public int FrequencyX { get; }

        /// <summary>
        /// The frequency count of itemset y in the data table.
        /// </summary>
        /// <remarks>
        /// y is one of the frequent k-itemsets with the largest k.
        /// </remarks>
        public int FrequencyY { get; }

        public string Rule { get; }

        /// <summary>
        /// Initializes a new instance of the AssociationRule.
        /// </summary>
        /// <param name="x">The string representation of the itemset x.</param>
        /// <param name="y">The string representation of the itemset y.</param>
        /// <param name="frequencyX">The frequency count of itemset x in the data table.</param>
        /// <param name="frequencyY">The frequency count of itemset y in the data table.</param>
        /// <remarks>
        /// y is one of the frequent k-itemsets with the largest k, x is a non-empty proper subset of y.
        /// </remarks>
        public AssociationRule(string x, string y, int frequencyX, int frequencyY)
        {
            this.X = x;
            this.Y = y;
            this.FrequencyX = frequencyX;
            this.FrequencyY = frequencyY;
            this.Confidence = (double)FrequencyY / frequencyX;
            this.Rule = string.Format("{0} => {1}", X, Y);
        }

        /// <summary>
        /// The string representation of the association rule.
        /// </summary>
        /// <returns>The string representation of the association rule.</returns>
        public override string ToString()
        {
            return string.Format("{0}, confidence = {1} / {2} = {3}", Rule, FrequencyY, FrequencyX, ConfidenceString);
        }
    }
}
