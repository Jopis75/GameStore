using Azure.Messaging.ServiceBus;

namespace Application.Dtos.AzureServiceBus
{
    public class AzureServiceBusStopProcessingRequestDto
    {
        public ServiceBusProcessor? ServiceBusProcessor { get; set; }
    }
}
