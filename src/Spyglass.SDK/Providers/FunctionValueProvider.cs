using System;

namespace Spyglass.SDK.Providers
{
    public class FunctionValueProvider : IMetricValueProvider<double>
    {
        protected Func<double> _valueProvider;

        public FunctionValueProvider(Func<double> valueProvider)
        {
            _valueProvider = valueProvider;
        }

        public FunctionValueProvider()
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

        public virtual void Set(double value)
        {
            throw new InvalidOperationException("Unable to set the value of a Function Gauge");
        }
    }
}
