using Spyglass.Server.Data;

namespace Spyglass.Server.ValueObjects
{
    public class MetricValue : IMetricValue
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public string Units { get; set; }
    }
}
