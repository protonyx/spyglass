using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Spyglass.Core.Metrics
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
