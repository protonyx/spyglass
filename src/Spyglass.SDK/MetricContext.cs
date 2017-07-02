using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spyglass.SDK;
using Spyglass.SDK.Metrics;
using Spyglass.Server.DAL;

namespace Spyglass.Core
{
    public class MetricContext : IMetricContext, IHasKey
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

        public object GetKey()
        {
            return this.Id;
        }
    }
}