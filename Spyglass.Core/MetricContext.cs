using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spyglass.Core.Metrics;

namespace Spyglass.Core
{
    public class MetricContext : IMetricContext
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<IMetric> Metrics { get; }

        #region Constructor

        public MetricContext()
        {
            this.Id = Guid.NewGuid();
            this.Metrics = new List<IMetric>();
        }

        #endregion
    }
}