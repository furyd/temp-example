using Example.Domain.Queries.Implementation;
using Example.Domain.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Domain.Queries.Registration
{
    public static class ComposeDependencies
    {
        public static IServiceCollection RegisterQueries(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFieldQueries, DefaultFieldQueries>();
            return serviceCollection;
        }
    }
}
