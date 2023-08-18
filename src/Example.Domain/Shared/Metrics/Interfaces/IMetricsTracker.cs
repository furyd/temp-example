namespace Example.Domain.Shared.Metrics.Interfaces
{
    public interface IMetricsTracker
    {
        ValueTask TrackMetric<T>(string name, T value);
    }
}
