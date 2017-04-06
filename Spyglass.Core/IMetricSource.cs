using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core
{
    public interface IMetricSource
    {
        string Name { get; set; }

        IEnumerable<IMetric> GetMetrics();
    }
}
