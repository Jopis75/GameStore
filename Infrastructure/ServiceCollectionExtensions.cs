using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Application.Models.Email;
using GameStore.Infrastructure.Azure;
using GameStore.Infrastructure.Email;
using GameStore.Infrastructure.IO;
using GameStore.Infrastructure.Logging;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAzureServices(configuration);
            serviceCollection.AddEmailService(configuration);
            serviceCollection.AddIOServices();
            serviceCollection.AddLoggerServices();
            return serviceCollection;
        }

        private static IServiceCollection AddAzureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAzureClients(azureClientFactoryBuilder =>
            {
                //azureClientFactoryBuilder.AddSecretClient(configuration.GetSection("KeyVault"));
                azureClientFactoryBuilder.AddBlobServiceClient(configuration.GetSection("BlobStorage")).WithName("BlobStorage");
                azureClientFactoryBuilder.AddServiceBusClientWithNamespace(configuration["ServiceBus:Namespace"]).WithName("ServiceBus");
                // Set up any default settings.
                azureClientFactoryBuilder.ConfigureDefaults(configuration.GetSection("AzureDefaults"));
            });
            serviceCollection.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
            return serviceCollection;
        }

        private static IServiceCollection AddEmailService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            serviceCollection.AddTransient<IEmailService, EmailService>();
            return serviceCollection;
        }

        private static IServiceCollection AddIOServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IGameStoreCsvFileParser, GameStoreCsvFileParser>();
            serviceCollection.AddScoped<IGameStoreCsvFileStreamReader, GameStoreCsvFileStreamReader>();
            serviceCollection.AddScoped<IGameStoreCsvFileService, GameStoreCsvFileService>();
            serviceCollection.AddScoped<IExcelFileService, ExcelFileService>();
            return serviceCollection;
        }

        public static IServiceCollection AddLoggerServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
            return serviceCollection;
        }
    }
}
