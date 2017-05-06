using System;
using System.Collections.Generic;
using System.Text;
using Spyglass.Core.Metrics;

namespace Spyglass.Core.Gauge
{
    public class ValueGauge : MetricBase, IGaugeMetric
    {
        private double _value = 0.0;
        
        public string Units { get; set; }

        public object Value => _value;

        public void Set(double value)
        {
            _value = value;
        }

        public double GetValue()
        {
            return _value;
        }

        object IMetricValueProvider.GetValue()
        {
            return GetValue();
        }
    }
}
