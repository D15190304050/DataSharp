using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSharp.Mathematics;

namespace DataSharp.Data.Analysis
{
    public interface IUnsupervisedModel
    {
        void Fit(Vector[] knownSamples);
        Vector Transform(Vector sample);
        Vector[] Transform(Vector[] samples);
    }
}
