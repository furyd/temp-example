using Example.Domain.Shared.Metrics.Interfaces;
using Example.Domain.Shared.Metrics.Models;
using Microsoft.ApplicationInsights;

namespace Example.Domain.Shared.Metrics.Implementation
{
    public class ApplicationInsightsMetrics : IMetrics
    {
        private readonly TelemetryClient _telemetryClient;

        public ApplicationInsightsMetrics(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public DurationModel TrackDuration(string eventName)
        {
            return new DurationModel(eventName, this);
        }

        public ValueTask TrackMetric<T>(string name, T value)
        {
            var metric = _telemetryClient.GetMetric(name);
            metric.TrackValue(value);

            return ValueTask.CompletedTask;
        }
    }
}
