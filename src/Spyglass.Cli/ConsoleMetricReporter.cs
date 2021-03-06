using System;
using System.Collections.Generic;
using Spyglass.SDK.Reporters;
using Spyglass.SDK.Data;

namespace Spyglass.Cli
{
    public class ConsoleMetricReporter : IMetricReporter
    {
        public void Report(IEnumerable<IMetricValueProvider> metrics)
        {
            foreach (var metric in metrics)
            {
                var values = metric.GetValueAsync();
                var name = metric.GetTypeName();
                foreach (var value in values.Result)
                {
                    Console.WriteLine($"{name}: {value}");
                }
            }
        }

        public void Flush()
        {
            return;
        }
    }
}
