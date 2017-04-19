using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Metrics
{
    public abstract class MetricBase : IMetric
    {
        public string Name { get; set; }

        public abstract IMetricValueProvider GetValueProvider();
    }
}
