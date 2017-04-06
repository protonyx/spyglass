using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Sources
{
    public class FileSource : IMetricSource
    {
        public string FilePath { get; set; }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<IMetric> GetMetrics()
        {
            throw new NotImplementedException();

            // Existance
            // Time since modified
            // Size
        }
    }
}
