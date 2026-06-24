using EventStore.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStoreServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<MongoDbConfig>(configuration.GetSection("MongoDbConfig"));
            return serviceCollection;
        }
    }
}
