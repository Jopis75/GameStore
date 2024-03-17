using Azure.Messaging.ServiceBus;

namespace Application.Dtos.AzureServiceBus
{
    public class AzureServiceBusSendMessagesRequestDto
    {
        public string QueueOrTopicName { get; set; } = default!;

        public List<ServiceBusMessage> Messages { get; set; } = new();
    }
}
