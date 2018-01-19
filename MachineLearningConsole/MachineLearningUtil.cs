using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mathematics;
using System.IO;

namespace MachineLearning
{
    public class MachineLearningUtil
    {

        /// <summary>
        /// Returns an array of Vector that contains all the normalized Vector correspond to original Vector.
        /// </summary>
        /// <param name="vectors">An array of Vector.</param>
        /// <returns>An array of Vector that contains all the normalized Vector correspond to original Vector.</returns>
        public static (Vector[], Vector, Vector) Normalize(Vector[] vectors)
        {
            if (vectors == null)
                throw new ArgumentNullException("vectors", "The input array of Vector is null.");
            for (int i = 0; i < vectors.Length; i++)
            {
                if (vectors[i] == null)
                    throw new ArgumentNullException($"vectors[{i}]", "There is an entry for Vector has null value.");
            }

            int vectorLength = vectors[0].Count;
            Vector min = new Vector(vectorLength);
            Vector max = new Vector(vectorLength);

            for (int i = 0; i < vectorLength; i++)
            {
                min[i] = vectors[0][i];
                max[i] = vectors[0][i];
            }
            for (int i = 1; i < vectors.Length; i++)
            {
                for (int j = 0; j < vectorLength; j++)
                {
                    if (vectors[i][j] < min[j])
                        min[j] = vectors[i][j];
                    else if (vectors[i][j] > max[j])
                        max[j] = vectors[i][j];
                }
            }

            Vector ranges = max - min;

            Vector[] normalizedVector = new Vector[vectors.Length];
            for (int i = 0; i < vectors.Length; i++)
            {
                normalizedVector[i] = new Vector(vectorLength);
                for (int j = 0; j < vectorLength; j++)
                    normalizedVector[i][j] = (vectors[i][j] - min[j]) / ranges[j];
            }

            return (normalizedVector, min, ranges);
        }

        public static Vector[] FileToMatrix(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException("No such file.");

            string[] contents = File.ReadAllLines(filePath);
            Vector[] matrix = new Vector[contents.Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                string[] line = contents[i].Split('\t');
                double[] lineData = new double[line.Length];
                for (int j = 0; j < line.Length; j++)
                    lineData[j] = double.Parse(line[j]);
                matrix[i] = new Vector(lineData);
            }
            return matrix;
        }
    }
}
