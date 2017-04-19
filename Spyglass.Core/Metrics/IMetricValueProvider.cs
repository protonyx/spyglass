using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Metrics
{
    public interface IMetricValueProvider
    {
        string Units { get; }

        object Value { get; }
    }

    public interface IMetricValueProvider<T> : IMetricValueProvider
    {
        T GetValue();
    }
}
