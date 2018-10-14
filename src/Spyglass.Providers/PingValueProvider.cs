using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;

namespace Spyglass.SDK.Providers
{
    public class PingValueProvider : IMetricValueProvider
    {
        [Required]
        public string Hostname { get; set; }

        public string GetTypeName()
        {
          return "Ping";
        }

        public async Task<IEnumerable<IMetricValue>> GetValueAsync()
        {
            if (string.IsNullOrWhiteSpace(Hostname))
                throw new ArgumentNullException(nameof(Hostname));

            var metrics = new List<IMetricValue>();

            var ping = new Ping();
            var options = new PingOptions()
            {
                DontFragment = true
            };

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;

            bool pingable = false;

            int tries = 3;
            while (tries > 0)
            {
                try
                {
                    var reply = await ping.SendPingAsync(Hostname, timeout, buffer, options);

                    if (reply.Status == IPStatus.Success)
                    {
                        pingable = true;
                        break;
                    }
                }
                catch (Exception e)
                {
                    //
                }
                finally
                {
                    tries--;
                }
            }

            metrics.Add(new MetricValue
            {
                Name = "Pingable",
                Value = pingable
            });
            return metrics;
        }
    }
}
