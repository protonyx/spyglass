using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spyglass.Core.Gauge;

namespace Spyglass.Core.Gauge
{
    [ConfigurableMetric("Database Query")]
    public class DatabaseQueryGauge : FunctionGauge
    {
        public string Hostname { get; set; }

        [Required]
        public string InstanceName { get; set; }

        public int? Port { get; set; }

        [Required]
        public string Database { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Query { get; set; }

        public string Units { get; set; }

        public DatabaseQueryGauge()
        {
            this._valueProvider = GetQueryValue;
        }

        private double GetQueryValue()
        {
            if (string.IsNullOrWhiteSpace(Query))
                throw new ArgumentNullException(nameof(Query));

            return double.NaN;
        }
    }
}
