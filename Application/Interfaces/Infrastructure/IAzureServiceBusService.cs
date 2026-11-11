using GameStore.Application.Dtos.Azure.ServiceBus;
using GameStore.Application.Dtos.General;

namespace GameStore.Application.Interfaces.Infrastructure
{
    public interface IAzureServiceBusService
    {
        Task<HttpResponseDto<AzureServiceBusSendMessagesResponseDto>> SendMessagesAsync(AzureServiceBusSendMessagesRequestDto azureServiceBusSendMessageRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<AzureServiceBusStartProcessingResponseDto>> StartProcessingAsync(AzureServiceBusStartProcessingRequestDto azureServiceBusStartProcessingRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<AzureServiceBusStopProcessingResponseDto>> StopProcessingAsync(AzureServiceBusStopProcessingRequestDto azureServiceBusStopProcessingRequestDto, CancellationToken cancellationToken);
    }
}
