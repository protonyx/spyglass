using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spyglass.Server.Data;

namespace Spyglass.Server.Models
{
    public class MetricGroup : IHasKey
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        
        public virtual ICollection<Metric> Metrics { get; set; }

        public object GetKey()
        {
            return this.Id;
        }
    }
}
