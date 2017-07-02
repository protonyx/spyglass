namespace Spyglass.SDK.Metrics
{
    public interface IMetricValueProvider
    {
        object GetValue();
    }

    public interface IMetricValueProvider<T> : IMetricValueProvider
    {
        new T GetValue();
    }
}
