using Example.Domain.Shared.Metrics.Models;

namespace Example.Domain.Shared.Metrics.Interfaces
{
    public interface IMetrics : IMetricsTracker
    {
        DurationModel TrackDuration(string eventName);
    }
}
