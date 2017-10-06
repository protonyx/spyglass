using System.Collections.Generic;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Reporters
{
    public interface IMetricReporter
    {
        void Report(IEnumerable<IMetricValueProvider> metrics);
        void Flush();
    }
}
