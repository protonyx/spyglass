using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;

namespace Spyglass.Core.Gauge
{
    [ConfigurableMetric("Ping")]
    public class PingGauge : FunctionGauge
    {
        [Required]
        public string Hostname { get; set; }

        public PingGauge()
        {
            this._valueProvider = GetPing;
        }

        private double GetPing()
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

            return reply.Status == IPStatus.Success ? 1.0 : 0.0;
        }
    }
}
