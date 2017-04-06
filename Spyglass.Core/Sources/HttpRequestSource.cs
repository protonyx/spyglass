using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Spyglass.Core.Metrics;

namespace Spyglass.Core.Sources
{
    public class HttpRequestSource : IMetricSource
    {
        public string Name { get; set; }

        public string Method { get; set; } = "GET";

        public Uri Uri { get; set; }

        public IEnumerable<IMetric> GetMetrics()
        {
            var client = new HttpClient()
            {
                BaseAddress = Uri
            };

            // TODO
            var msg = new HttpRequestMessage
            {
                Method = HttpMethod.Get
            };

            var sw = Stopwatch.StartNew();
            var task = client.SendAsync(msg);
            task.Wait();
            var response = task.Result;
            sw.Stop();

            // Status code
            yield return new ValueMetric
            {
                Name = $"{Name}-StatusCode",
                Value = (int) response.StatusCode,
                Units = ""
            };

            // Response time
            yield return new ValueMetric
            {
                Name = $"{Name}-ResponseTime",
                Value = sw.Elapsed,
                Units = ""
            };

            // Response body
            var readTask = response.Content.ReadAsStringAsync();
            readTask.Wait();

            yield return new ValueMetric
            {
                Name = $"{Name}-ResponseBody",
                Value = readTask.Result,
                Units = ""
            };
        }
    }
}
