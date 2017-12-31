using System;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Models
{
    public class MetricValue : IMetricValue
    {
        public Guid MetricId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }

        public string Units { get; set; }
    }
}
