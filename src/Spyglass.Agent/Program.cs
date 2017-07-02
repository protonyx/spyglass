using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spyglass.Core;
using Spyglass.Core.Gauge;
using Spyglass.Core.Reporters;

namespace Spyglass.Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            var iocBuilder = new ServiceCollection()
                .AddLogging();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            iocBuilder.AddSingleton(config);

            var context = new MetricContext
            {
                Name = "Test"
            };

            var httpMetric = new HttpRequestGauge
            {
                Name = "Google",
                Uri = new Uri("http://www.google.com")
            };
            context.Metrics.Add(httpMetric);

            var pingMetric = new PingGauge
            {
                Name = "Gateway Pingable",
                Hostname = "192.168.0.1"
            };
            context.Metrics.Add(pingMetric);

            var reporter = new ConsoleMetricReporter(context);
            reporter.Report();

            var ioc = iocBuilder.BuildServiceProvider();
            ioc.GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            Console.ReadKey();
        }
    }
}