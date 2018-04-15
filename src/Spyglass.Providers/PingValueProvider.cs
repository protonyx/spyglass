using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
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

        public IEnumerable<IMetricValue> GetValue()
        {
            if (string.IsNullOrWhiteSpace(Hostname))
                throw new ArgumentNullException(nameof(Hostname));

            var waiter = new AutoResetEvent(false);

            var ping = new Ping();
            var options = new PingOptions()
            {
                DontFragment = true
            };

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            var pingTask = ping.SendPingAsync(Hostname, timeout, buffer, options);
            pingTask.Wait();

            var reply = pingTask.Result;

            yield return new MetricValue
            {
              Name = "Pingable",
              Value = reply.Status == IPStatus.Success
            };
        }
    }
}
