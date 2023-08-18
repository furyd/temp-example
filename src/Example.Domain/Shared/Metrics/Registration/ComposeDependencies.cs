using Example.Domain.Shared.Metrics.Implementation;
using Example.Domain.Shared.Metrics.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Domain.Shared.Metrics.Registration
{
    public static class ComposeDependencies
    {
        public static IServiceCollection RegisterMetrics(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMetrics, ApplicationInsightsMetrics>();
            return serviceCollection;
        }
    }
}
