using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Metrics
{
    public interface IMetricValueProvider
    {
        object GetValue();
    }

    public interface IMetricValueProvider<T> : IMetricValueProvider
    {
        new T GetValue();
    }
}
