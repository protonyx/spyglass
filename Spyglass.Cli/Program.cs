using System;
using System.Linq;
using Spyglass.Core.Sources;

namespace Spyglass.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var metricTest = new HttpRequestSource
            {
                Name = "Google",
                Uri = new Uri("http://www.google.com")
            };

            var metrics = metricTest.GetMetrics().ToList();

            Console.WriteLine("");
        }
    }
}