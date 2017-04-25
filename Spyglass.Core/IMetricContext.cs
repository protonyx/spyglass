using System.Collections.Generic;
using Spyglass.Core.Metrics;

namespace Spyglass.Core
{
    public interface IMetricContext
    {
        string Name { get; set; }
        ICollection<IMetric> Metrics { get; }
    }
}