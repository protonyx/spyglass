using System;

namespace Spyglass.SDK.Metrics
{
    public interface IMetric
    {
        Guid Id { get; set; }

        string Name { get; }
    }
}
