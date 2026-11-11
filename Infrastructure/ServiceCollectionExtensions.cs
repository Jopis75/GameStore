using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Application.Models.Email;
using GameStore.Infrastructure.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddEmailService(configuration);
            serviceCollection.AddAzureServiceClients(configuration);
            serviceCollection.AddServices();
            return serviceCollection;
        }

        private static IServiceCollection AddAzureServiceClients(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAzureClients(azureClientFactoryBuilder =>
            {
                //azureClientFactoryBuilder.AddSecretClient(configuration.GetSection("KeyVault"));
                azureClientFactoryBuilder.AddBlobServiceClient(configuration.GetSection("BlobStorage")).WithName("BlobStorage");
                azureClientFactoryBuilder.AddServiceBusClientWithNamespace(configuration["ServiceBus:Namespace"]).WithName("ServiceBus");
                // Set up any default settings.
                azureClientFactoryBuilder.ConfigureDefaults(configuration.GetSection("AzureDefaults"));
            });
            return serviceCollection;
        }

        private static IServiceCollection AddEmailService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            serviceCollection.AddTransient<IEmailService, EmailService>();
            return serviceCollection;
        }

        private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
            serviceCollection.AddScoped<IGameStoreFileService, GameStoreFileService>();
            serviceCollection.AddScoped<IExcelFileService, ExcelFileService>();
            serviceCollection.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
            return serviceCollection;
        }
    }
}
