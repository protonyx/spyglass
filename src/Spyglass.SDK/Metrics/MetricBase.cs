using System;
using System.ComponentModel.DataAnnotations;

namespace Spyglass.SDK.Metrics
{
    public abstract class MetricBase : IMetric
    {
        [Display(AutoGenerateField = false)]
        public Guid Id { get; set; }

        [Display(Order = 0)]
        [Required]
        public string Name { get; set; }
    }
}
