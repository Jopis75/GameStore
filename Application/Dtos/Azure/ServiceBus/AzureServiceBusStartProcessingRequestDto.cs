using Azure.Messaging.ServiceBus;

namespace Application.Dtos.Azure.ServiceBus
{
    public class AzureServiceBusStartProcessingRequestDto
    {
        public string QueueName { get; set; } = default!;

        public Func<ProcessMessageEventArgs, Task> ProcessMessageAsync { get; set; } = default!;

        public Func<ProcessErrorEventArgs, Task> ProcessErrorAsync { get; set; } = default!;
    }
}
