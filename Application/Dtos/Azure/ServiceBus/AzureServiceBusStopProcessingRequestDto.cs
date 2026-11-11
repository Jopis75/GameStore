using Azure.Messaging.ServiceBus;

namespace GameStore.Application.Dtos.Azure.ServiceBus
{
    public class AzureServiceBusStopProcessingRequestDto
    {
        public ServiceBusProcessor? ServiceBusProcessor { get; set; }
    }
}
