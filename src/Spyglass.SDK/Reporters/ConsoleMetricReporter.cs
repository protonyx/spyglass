using System;
using System.Collections.Generic;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Reporters
{
    public class ConsoleMetricReporter : IMetricReporter
    {
        public void Report(IEnumerable<Metric> metrics)
        {
            foreach (var metric in metrics)
            {
                var value = metric.ValueProvider.GetValue();
                metric.LastValue = value;
                Console.WriteLine($"{metric.Name}: {value}");
            }
        }

        public void Flush()
        {
            return;
        }
    }
}
