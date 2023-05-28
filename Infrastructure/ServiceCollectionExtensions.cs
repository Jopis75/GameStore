using Application.Interfaces.Infrastructure;
using Application.Models.Email;
using Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddEmailSender(configuration);

            return serviceCollection;
        }

        private static IServiceCollection AddEmailSender(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            serviceCollection.AddTransient<IEmailSender, EmailSender>();

            return serviceCollection;
        }
    }
}
