using Application.Interfaces.EventStore;
using EventStore.Configurations;
using EventStore.Repositories;
using EventStore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStoreServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddConfigurations(configuration);
            serviceCollection.AddRepositories();
            serviceCollection.AddServices();

            return serviceCollection;
        }

        private static IServiceCollection AddConfigurations(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<MongoDbConfig>(configuration.GetSection("MongoDbConfig"));

            return serviceCollection;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEventStoreRepository, EventStoreRepository>();

            return serviceCollection;
        }

        private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEventStoreService, EventStoreService>();

            return serviceCollection;
        }
    }
}
