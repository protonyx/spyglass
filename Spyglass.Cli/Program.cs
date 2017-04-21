using System;
using System.Linq;
using Spyglass.Core;
using Spyglass.Core.Gauge;
using Spyglass.Core.Reporters;

namespace Spyglass.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MetricContext
            {
                Name = "Test"
            };

            var httpMetric = new HttpRequestGauge
            {
                Name = "Google",
                Uri = new Uri("http://www.google.com")
            };
            context.AddMetric(httpMetric);

            var pingMetric = new PingGauge
            {
                Name = "Gateway Pingable",
                Hostname = "192.168.0.1"
            };
            context.AddMetric(pingMetric);

            context.Report(new ConsoleMetricReporter());

            Console.ReadKey();
        }
    }
}