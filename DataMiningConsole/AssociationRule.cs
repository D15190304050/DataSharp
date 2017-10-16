using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiningConsole
{
    public class AssociationRule
    {
        public string X { get; }

        public string Y { get; }

        public double Confidence { get; }

        public int FrequencyX { get; }

        public int FrequencyY { get; }

        public AssociationRule(string x, string y, int frequencyX, int frequencyY)
        {
            this.X = x;
            this.Y = y;
            this.FrequencyX = frequencyX;
            this.FrequencyY = frequencyY;
            this.Confidence = (double)FrequencyY / frequencyX;
        }

        public override string ToString()
        {
            return string.Format("{0} => {1}, confidence = {2} / {3} = {4:0.}%", X, Y, FrequencyY, FrequencyX, Confidence * 100);
        }
    }
}
