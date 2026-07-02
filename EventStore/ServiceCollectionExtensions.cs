using Application.Interfaces.EventSourcing;
using Application.Interfaces.EventSourcing.Handlers.Addresses;
using Application.Interfaces.EventSourcing.Producers;
using Confluent.Kafka;
using EventSourcing.Configurations;
using EventSourcing.Handlers.Addresses;
using EventSourcing.Producers;
using EventSourcing.Repositories;
using EventSourcing.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStoreServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddConfigurations(configuration);
            serviceCollection.AddRepositories();
            serviceCollection.AddServices();
            serviceCollection.AddHandlers();
            serviceCollection.AddProducers();

            return serviceCollection;
        }

        private static IServiceCollection AddConfigurations(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<MongoDbConfig>(configuration.GetSection("MongoDbConfig"));
            serviceCollection.Configure<ProducerConfig>(configuration.GetSection("ProducerConfig"));

            return serviceCollection;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAddressEventHandler, AddressEventHandler>();

            return serviceCollection;
        }

        private static IServiceCollection AddProducers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEventProducer, EventProducer>();
           
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
