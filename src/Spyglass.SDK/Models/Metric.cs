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

        public Guid? ContextId { get; set; }

        [Display(Order = 0)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string ProviderType { get; set; }

        public IMetricValueProvider ProviderConfiguration { get; set; }

        public object GetKey()
        {
            return Id;
        }
    }
}
