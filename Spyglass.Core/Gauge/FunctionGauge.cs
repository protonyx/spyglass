using System;
using System.Collections.Generic;
using System.Text;
using Spyglass.Core.Metrics;

namespace Spyglass.Core.Gauge
{
    public class FunctionGauge : MetricBase, IGaugeMetric
    {
        protected Func<double> _valueProvider;

        public string Units { get; set; }

        public object Value => GetValue();

        public FunctionGauge(Func<double> valueProvider)
        {
            _valueProvider = valueProvider;
        }

        public FunctionGauge()
        {
            
        }

        public double GetValue()
        {
            try
            {
                return _valueProvider?.Invoke() ?? double.NaN;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        public void Set(double value)
        {
            throw new InvalidOperationException("Unable to set the value of a Function Gauge");
        }

        public override IMetricValueProvider GetValueProvider()
        {
            return this;
        }
    }
}
