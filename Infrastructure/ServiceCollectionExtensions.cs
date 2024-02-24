using Application.Interfaces.Infrastructure;
using Application.Models.Email;
using Infrastructure.Email;
using Infrastructure.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddEmailSender(configuration);
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
                //azureClientFactoryBuilder.AddServiceBusClientWithNamespace(configuration["ServiceBus:Namespace"]);
                // Set up any default settings.
                azureClientFactoryBuilder.ConfigureDefaults(configuration.GetSection("AzureDefaults"));
            });

            return serviceCollection;
        }

        private static IServiceCollection AddEmailSender(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            serviceCollection.AddTransient<IEmailSender, EmailSender>();

            return serviceCollection;
        }

        private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();

            return serviceCollection;
        }
    }
}
