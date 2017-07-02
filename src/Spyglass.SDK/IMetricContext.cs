using System.Collections.Generic;
using Spyglass.SDK.Metrics;

namespace Spyglass.SDK
{
    public interface IMetricContext
    {
        string Name { get; set; }
        ICollection<IMetric> Metrics { get; }
    }
}