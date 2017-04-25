using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spyglass.Core.Metrics;
using Spyglass.Core.Reporters;

namespace Spyglass.Core
{
    public class MetricContext : IMetricContext
    {
        public string Name { get; set; }

        public ICollection<IMetric> Metrics { get; }

        #region Constructor

        public MetricContext()
        {
            this.Metrics = new List<IMetric>();
        }

        #endregion
    }
}