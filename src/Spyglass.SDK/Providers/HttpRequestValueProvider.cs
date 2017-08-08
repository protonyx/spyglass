using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace Spyglass.SDK.Providers
{
    [ConfigurableMetric("HTTP Request")]
    public class HttpRequestValueProvider : FunctionValueProvider
    {
        public string Method { get; set; } = "GET";

        [Required]
        public Uri Uri { get; set; }

        public HttpRequestValueProvider()
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

        public override void Set(double value)
        {
            throw new InvalidOperationException("Unable to set the value of an HTTP Request metric");
        }
    }
}
