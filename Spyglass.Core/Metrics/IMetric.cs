using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Metrics
{
    public interface IMetric
    {
        string Name { get; }

        IMetricValueProvider GetValueProvider();
    }
}
