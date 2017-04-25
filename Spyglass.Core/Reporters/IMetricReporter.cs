using System.Collections.Generic;
using Spyglass.Core.Metrics;

namespace Spyglass.Core.Reporters
{
    public interface IMetricReporter
    {
        void Report();
        void Flush();
    }
}