using System;
using System.Collections.Generic;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Providers
{
  public abstract class MetricValueProviderBase : IMetricValueProvider
  {
        public abstract IEnumerable<IMetricValue> GetValue();

        public abstract string GetTypeName();
    }
}
