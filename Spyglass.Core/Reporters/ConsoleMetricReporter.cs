using System;
using System.Collections.Generic;
using System.Text;
using Spyglass.Core.Metrics;

namespace Spyglass.Core.Reporters
{
    public class ConsoleMetricReporter : IMetricReporter
    {
        public void ReportMetric(IMetric metric)
        {
            var valueProvider = metric.GetValueProvider();
            Console.WriteLine($"{metric.Name}: {valueProvider.Value} {valueProvider.Units}");
        }

        public void Flush()
        {
            return;
        }
    }
}
