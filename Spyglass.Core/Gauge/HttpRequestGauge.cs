using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Spyglass.Core.Metrics;

namespace Spyglass.Core.Gauge
{
    [ConfigurableMetric("HTTP Request")]
    public class HttpRequestGauge : FunctionGauge
    {
        public string Method { get; set; } = "GET";

        [Required]
        public Uri Uri { get; set; }

        public HttpRequestGauge()
        {
            this._valueProvider = GetStatusCode;
        }

        private double GetStatusCode()
        {
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

            var task = client.SendAsync(msg);
            task.Wait();
            var response = task.Result;

            return (double) response.StatusCode;
        }

        public void Set(double value)
        {
            throw new InvalidOperationException("Unable to set the value of an HTTP Request metric");
        }
    }
}
