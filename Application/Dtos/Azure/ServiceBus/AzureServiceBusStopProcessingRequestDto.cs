using Azure.Messaging.ServiceBus;

namespace Application.Dtos.Azure.ServiceBus
{
    public class AzureServiceBusStopProcessingRequestDto
    {
        public ServiceBusProcessor? ServiceBusProcessor { get; set; }
    }
}
