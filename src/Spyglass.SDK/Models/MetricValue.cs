using System;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Models
{
    public class MetricValue : IMetricValue
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public string Units { get; set; }
    }
}
