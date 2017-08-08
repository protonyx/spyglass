using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace Spyglass.SDK.Providers
{
    [ConfigurableMetric("Ping")]
    public class PingValueProvider : FunctionValueProvider
    {
        [Required]
        public string Hostname { get; set; }

        public PingValueProvider()
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
