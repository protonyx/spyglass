using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spyglass.SDK.Data;
using Spyglass.SDK.Providers;
using Spyglass.SDK.Reporters;

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
            
            var metrics = new[]
            {
                new Metric
                {
                    Name = "Google",
                    ValueProvider = new HttpRequestValueProvider
                    {
                        Uri = new Uri("http://www.google.com")
                    }
                },
                new Metric
                {
                    Name = "Gateway Pingable",
                    ValueProvider = new PingValueProvider
                    {
                        Hostname = "192.168.0.1"
                    }
                }
            };

            var reporter = new ConsoleMetricReporter();
            reporter.Report(metrics);

            var ioc = iocBuilder.BuildServiceProvider();
            ioc.GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            Console.ReadKey();
        }
    }
}