using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spyglass.SDK;
using Spyglass.SDK.Metrics;
using Spyglass.SDK.Reporters;

namespace Spyglass.Core.Reporters
{
    public class ConsoleMetricReporter : IMetricReporter
    {
        protected IMetricContext Context { get; }

        public ConsoleMetricReporter(IMetricContext context)
        {
            Context = context;
        }

        public void Report()
        {
            foreach (var metric in this.Context.Metrics.Where(t => t is IMetricValueProvider))
            {
                var value = ((IMetricValueProvider)metric).GetValue();
                Console.WriteLine($"{metric.Name}: {value}");
            }
        }

        public void Flush()
        {
            return;
        }
    }
}
