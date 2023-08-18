using Example.Domain.Shared.Metrics.Interfaces;
using System.Diagnostics;

namespace Example.Domain.Shared.Metrics.Models
{
    public sealed class DurationModel : IDisposable
    {
        private readonly string _eventName;
        private readonly IMetricsTracker _metricsTracker;

        public Stopwatch Stopwatch { get; }

        public DurationModel(string eventName, IMetricsTracker metricsTracker)
        {
            Stopwatch = Stopwatch.StartNew();
            _eventName = eventName;
            _metricsTracker = metricsTracker;
        }

        public void Dispose()
        {
            Stopwatch.Stop();
            _metricsTracker.TrackMetric($"{_eventName}:{Constants.Metrics.Duration}", Stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}
