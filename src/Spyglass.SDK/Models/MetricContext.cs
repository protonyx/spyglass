using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Models
{
    public class MetricContext : IHasKey
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public object GetKey()
        {
            return this.Id;
        }
    }
}
