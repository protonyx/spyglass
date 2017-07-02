using System;
using System.Collections.Generic;
using System.Text;
using Spyglass.SDK.Metrics;

namespace Spyglass.Core.Gauge
{
    public class FunctionGauge : MetricBase, IGaugeMetric
    {
        protected Func<double> _valueProvider;

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

        object IMetricValueProvider.GetValue()
        {
            return GetValue();
        }

        public void Set(double value)
        {
            throw new InvalidOperationException("Unable to set the value of a Function Gauge");
        }
    }
}
