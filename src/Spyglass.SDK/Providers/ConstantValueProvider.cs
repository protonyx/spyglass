namespace Spyglass.SDK.Providers
{
    public class ConstantValueProvider : IMetricValueProvider<double>
    {
        public string Units { get; set; }

        public double Value { get; set; }

        public void Set(double value)
        {
            this.Value = value;
        }

        public double GetValue()
        {
            return this.Value;
        }

        object IMetricValueProvider.GetValue()
        {
            return GetValue();
        }
    }
}
