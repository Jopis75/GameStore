using Application.Dtos.AzureServiceBus;
using Application.Dtos.Common;

namespace Application.Interfaces.Infrastructure
{
    public interface IAzureServiceBusService
    {
        Task<HttpResponseDto<AzureServiceBusSendMessagesResponseDto>> SendMessagesAsync(AzureServiceBusSendMessagesRequestDto azureServiceBusSendMessageRequestDto);

        Task<HttpResponseDto<AzureServiceBusStartProcessingResponseDto>> StartProcessingAsync(AzureServiceBusStartProcessingRequestDto azureServiceBusStartProcessingRequestDto);

        Task<HttpResponseDto<AzureServiceBusStopProcessingResponseDto>> StopProcessingAsync(AzureServiceBusStopProcessingRequestDto azureServiceBusStopProcessingRequestDto);
    }
}
