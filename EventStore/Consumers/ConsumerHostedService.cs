using Application.Interfaces.EventSourcing.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventSourcing.Consumers
{
    public class ConsumerHostedService(IServiceProvider serviceProvider, ILogger<ConsumerHostedService> logger) : IHostedService
    {
        private readonly string addressEventsTopicEnvironmentVariable = "ADDRESS_EVENTS_TOPIC";

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Start ConsumerHostedService.");

            using (var scope = serviceProvider.CreateScope())
            {
                StartConsumeAddressEvents(scope, cancellationToken);
            }

            return Task.CompletedTask;
        }

        private void StartConsumeAddressEvents(IServiceScope scope, CancellationToken cancellationToken)
        {
            var addressEventsTopic = Environment.GetEnvironmentVariable(addressEventsTopicEnvironmentVariable);

            if (addressEventsTopic == null)
            {
                logger.LogError($"Environment variable {addressEventsTopicEnvironmentVariable} is not found.");
                throw new ArgumentNullException($"Environment variable {addressEventsTopicEnvironmentVariable} is not found.");
            }
            else
            {
                var addressEventConsumer = scope.ServiceProvider.GetRequiredService<IAddressEventConsumer>();

                Task.Run(() => addressEventConsumer.Consume(addressEventsTopic, cancellationToken), cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stop ConsumerHostedService.");

            return Task.CompletedTask;
        }
    }
}
