using System;
using System.Collections.Generic;

namespace Spyglass.SDK.Data
{
    public class MetricContext : IHasKey
    {
        public Guid Id { get; set; }

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