using System;

namespace Spyglass.SDK.Data
{
    public interface IMetricValue
    {
        string Name { get; set; }

        double Value { get; set; }

        string Units { get; set; }
    }
}
