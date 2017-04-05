using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Sinks
{
    public class DatabaseSink : IMetricSink
    {
        public void PutMetrics(IEnumerable<IMetric> metrics)
        {
            throw new NotImplementedException();
        }
    }
}
