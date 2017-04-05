using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Sources
{
    public class HttpRequestSource : IMetricSource
    {
        public IEnumerable<IMetric> GetMetrics()
        {
            throw new NotImplementedException();

            // Status code
            // Response time
            // Response body
        }
    }
}
