using Confluent.Kafka;
using GameStore.Application.Interfaces.EventSourcing;
using GameStore.Application.Interfaces.EventSourcing.Consumers;
using GameStore.Application.Interfaces.EventSourcing.EventHandlers;
using GameStore.Application.Interfaces.EventSourcing.EventSourcingHandlers;
using GameStore.Application.Interfaces.EventSourcing.Producers;
using GameStore.EventSourcing.Configurations;
using GameStore.EventSourcing.Consumers;
using GameStore.EventSourcing.EventHandlers;
using GameStore.EventSourcing.EventSourcingHandlers;
using GameStore.EventSourcing.Producers;
using GameStore.EventSourcing.Repositories;
using GameStore.EventSourcing.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.EventSourcing
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventSourcingServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddConfigurations(configuration);
            serviceCollection.AddRepositories();
            serviceCollection.AddServices();
            serviceCollection.AddHandlers();
            serviceCollection.AddProducers();
            serviceCollection.AddConsumers();
            return serviceCollection;
        }

        private static IServiceCollection AddConfigurations(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<MongoDbConfig>(configuration.GetSection("MongoDbConfig"));
            serviceCollection.Configure<ProducerConfig>(configuration.GetSection("ProducerConfig"));
            serviceCollection.Configure<ConsumerConfig>(configuration.GetSection("ConsumerConfig"));
            return serviceCollection;
        }

        private static IServiceCollection AddConsumers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAddressEventConsumer, AddressEventConsumer>();
            serviceCollection.AddHostedService<ConsumerHostedService>();
            return serviceCollection;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAddressEventHandler, AddressEventHandler>();
            serviceCollection.AddScoped<ICompanyEventHandler, CompanyEventHandler>();
            serviceCollection.AddScoped<IAddressEventSourcingHandler, AddressEventSourcingHandler>();
            serviceCollection.AddScoped<ICompanyEventSourcingHandler, CompanyEventSourcingHandler>();
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
