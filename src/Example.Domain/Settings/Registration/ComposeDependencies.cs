using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Domain.Settings.Registration
{
    public static class ComposeDependencies
    {
        public static IServiceCollection RegisterSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions();
            serviceCollection.Configure<SqlServerSettings>(configuration.GetSection(nameof(SqlServerSettings)));
            serviceCollection.Configure<ServiceBusSettings>(configuration.GetSection(nameof(ServiceBusSettings)));
            return serviceCollection;
        }
    }
}
