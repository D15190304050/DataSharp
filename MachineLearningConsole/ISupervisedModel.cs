using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;

namespace MachineLearning
{
    public interface ISupervisedModel
    {
        void Train(Vector[] samples, Vector[] labels);
        Vector Predict(Vector sample);
        Vector[] Predict(Vector[] samples);
    }
}
