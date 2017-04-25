using System;
using System.Collections.Generic;
using System.Text;
using Spyglass.Core.Metrics;

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
            foreach (var metric in this.Context.Metrics)
            {
                var valueProvider = metric.GetValueProvider();
                Console.WriteLine($"{metric.Name}: {valueProvider.Value} {valueProvider.Units}");
            }
        }

        public void Flush()
        {
            return;
        }
    }
}
