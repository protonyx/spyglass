using System;
using System.Collections.Generic;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Data
{
    public interface IMetricValueProvider
    {
        IEnumerable<IMetricValue> GetValue();

        string GetTypeName();
    }
}
