using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    public interface IUnsupervisedModel
    {
        void Fit(Vector[] knownSamples);
        Vector Transform(Vector sample);
        Vector[] Transform(Vector[] samples);
    }
}
