namespace Spyglass.SDK.Reporters
{
    public interface IMetricReporter
    {
        void Report();
        void Flush();
    }
}