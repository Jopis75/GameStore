using Application.Interfaces.EventSourcing.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventSourcing.Consumers
{
    public class ConsumerHostedService(IServiceProvider serviceProvider, ILogger<ConsumerHostedService> logger) : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Start ConsumerHostedService.");

            using (var scope = serviceProvider.CreateScope())
            {
                var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC") ?? throw new ArgumentNullException("KAFKA_TOPIC", "Environment variable KAFKA_TOPIC is not set.");

                Task.Run(() => eventConsumer.Consume(topic), cancellationToken);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stop ConsumerHostedService.");

            return Task.CompletedTask;
        }
    }
}
