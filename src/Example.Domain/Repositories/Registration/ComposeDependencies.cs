using Example.Domain.Repositories.Implementation;
using Example.Domain.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Domain.Repositories.Registration
{
    public static class ComposeDependencies
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFieldRepository, SqlServerFieldRepository>();
            return serviceCollection;
        }
    }
}
