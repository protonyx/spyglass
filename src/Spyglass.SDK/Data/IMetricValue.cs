using System;

namespace Spyglass.SDK.Data
{
    public interface IMetricValue
    {
        string Name { get; set; }

        object Value { get; set; }

        string Units { get; set; }
    }
}
