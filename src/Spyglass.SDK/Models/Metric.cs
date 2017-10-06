using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Models
{
    public class Metric : IHasKey
    {
        [Display(AutoGenerateField = false)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Order = 0)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public IMetricValueProvider ValueProvider { get; set; }

        public object GetKey()
        {
            return Id;
        }
    }
}
