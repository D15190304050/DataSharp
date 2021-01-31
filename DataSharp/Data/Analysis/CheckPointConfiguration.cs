using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharp.Data.Analysis
{
    /// <summary>
    /// The CheckPointConfiguration class represents the way to save check points of machine learning models during training.
    /// </summary>
    public class CheckPointConfiguration
    {
        /// <summary>
        /// Gets or sets the time interval between 2 check point saves.
        /// </summary>
        public TimeSpan SavingInterval { get; set; }

        /// <summary>
        /// Gets or sets the max number of check points need to be saved.
        /// </summary>
        public int CheckPointMax { get; set; }

        /// <summary>
        /// The default check point configuration.
        /// </summary>
        public static CheckPointConfiguration DefaultConfiguration { get; }

        static CheckPointConfiguration()
        {
            
        }
    }
}
