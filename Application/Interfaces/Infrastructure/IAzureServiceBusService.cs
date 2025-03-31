using Application.Dtos.Azure.ServiceBus;
using Application.Dtos.General;

namespace Application.Interfaces.Infrastructure
{
    public interface IAzureServiceBusService
    {
        Task<HttpResponseDto<AzureServiceBusSendMessagesResponseDto>> SendMessagesAsync(AzureServiceBusSendMessagesRequestDto azureServiceBusSendMessageRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<AzureServiceBusStartProcessingResponseDto>> StartProcessingAsync(AzureServiceBusStartProcessingRequestDto azureServiceBusStartProcessingRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<AzureServiceBusStopProcessingResponseDto>> StopProcessingAsync(AzureServiceBusStopProcessingRequestDto azureServiceBusStopProcessingRequestDto, CancellationToken cancellationToken);
    }
}
