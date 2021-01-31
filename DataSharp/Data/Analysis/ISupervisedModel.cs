using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSharp.Mathematics;

namespace DataSharp.Data.Analysis
{
    public interface ISupervisedModel
    {
        void Train(Vector[] samples, Vector[] labels);
        Vector Predict(Vector sample);
        Vector[] Predict(Vector[] samples);
    }
}
