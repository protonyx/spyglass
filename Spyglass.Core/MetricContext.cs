using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spyglass.Core.Metrics;
using Spyglass.Core.Reporters;

namespace Spyglass.Core
{
    public interface IMetricContext
    {
        string Name { get; }

        IEnumerable<IMetric> Metrics { get; }

        void AddMetric(IMetric metric);
        void Report(IMetricReporter reporter);
    }

    public class MetricContext : IMetricContext
    {
        protected List<IMetric> Metrics { get; }

        public string Name { get; set; }

        #region Constructor

        public MetricContext()
        {
            this.Metrics = new List<IMetric>();
        }

        #endregion
        
        #region IMetricContext Implementation

        IEnumerable<IMetric> IMetricContext.Metrics => this.Metrics;

        public void AddMetric(IMetric metric)
        {
            this.Metrics.Add(metric);
        }

        public void Report(IMetricReporter reporter)
        {
            foreach (var metric in this.Metrics)
            {
                reporter.ReportMetric(metric);
            }

            reporter.Flush();
        }

        #endregion
    }
}