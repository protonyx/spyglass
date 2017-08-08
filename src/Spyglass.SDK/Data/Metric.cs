using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Spyglass.SDK.Converters;
using Spyglass.SDK.Providers;

namespace Spyglass.SDK.Data
{
    public class Metric : IHasKey
    {
        [Display(AutoGenerateField = false)]
        public Guid Id { get; set; }

        [Display(Order = 0)]
        [Required]
        public string Name { get; set; }

        public string Type { get; set; }

        public object LastValue { get; set; }

        [JsonConverter(typeof(MetricProviderConverter))]
        public IMetricValueProvider ValueProvider { get; set; }

        public object GetKey()
        {
            return Id;
        }
    }
}
