using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spyglass.Core.Metrics;
using Spyglass.Core.Gauge;

namespace Spyglass.Core.Gauge
{
    public class DatabaseQueryGauge : FunctionGauge
    {
        public string Hostname { get; set; }

        public string InstanceName { get; set; }

        public int? Port { get; set; }

        public string Database { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Query { get; set; }
        
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
