using System;
using System.ComponentModel.DataAnnotations;

namespace Spyglass.SDK.Providers
{
    [ConfigurableMetric("Database Query")]
    public class DatabaseQueryValueProvider : FunctionValueProvider
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

        public DatabaseQueryValueProvider()
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
