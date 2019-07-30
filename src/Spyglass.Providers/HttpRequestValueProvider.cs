using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;

namespace Spyglass.SDK.Providers
{
    public class HttpRequestValueProvider : IMetricValueProvider
    {
        public string Method { get; set; } = "GET";

        [Required]
        public Uri Uri { get; set; }

        public string GetTypeName()
        {
          return "HTTP Request";
        }

        public async Task<IEnumerable<IMetricValue>> GetValueAsync()
        {
            var metrics = new List<IMetricValue>();

            var client = new HttpClient()
            {
                BaseAddress = Uri
            };

            HttpMethod method;
            switch (this.Method)
            {
                case "POST":
                    method = HttpMethod.Post;
                    break;
                default:
                case "GET":
                    method = HttpMethod.Get;
                    break;
            }

            var msg = new HttpRequestMessage
            {
                Method = method
            };

            var response = await client.SendAsync(msg);

            metrics.Add(new MetricValue
            {
              Name = "Status Code",
              Value = (float) response.StatusCode
            });
            return metrics;
        }
    }
}
