using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Models
{
    public class MetricContext : IHasKey
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Metric> Metrics { get; }

        #region Constructor

        public MetricContext()
        {
            this.Id = Guid.NewGuid();
            this.Metrics = new List<Metric>();
        }

        #endregion

        public object GetKey()
        {
            return this.Id;
        }
    }
}
