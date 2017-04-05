using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core
{
    public interface IMetricSink
    {
        void PutMetrics(IEnumerable<IMetric> metrics);
    }
}
