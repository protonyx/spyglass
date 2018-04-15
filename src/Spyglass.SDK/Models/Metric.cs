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

        public Guid? MetricGroupId { get; set; }

        [Display(Order = 0)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string ProviderType { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }

        public IMetricValueProvider Provider { get; set; }

        public object GetKey()
        {
            return Id;
        }
    }
}
