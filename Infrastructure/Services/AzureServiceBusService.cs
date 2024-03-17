using Application.Dtos.AzureServiceBus;
using Application.Dtos.Common;
using Application.Interfaces.Infrastructure;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class AzureServiceBusService : IAzureServiceBusService
    {
        private readonly IAzureClientFactory<ServiceBusClient> _azureClientFactory;

        private readonly ServiceBusClient _serviceBusClient;

        private readonly ILogger<AzureServiceBusService> _logger;

        public AzureServiceBusService(IAzureClientFactory<ServiceBusClient> azureClientFactory, ILogger<AzureServiceBusService> logger)
        {
            _azureClientFactory = azureClientFactory ?? throw new ArgumentNullException(nameof(azureClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _serviceBusClient = _azureClientFactory.CreateClient("ServiceBus");
        }

        public async Task<HttpResponseDto<AzureServiceBusSendMessagesResponseDto>> SendMessagesAsync(AzureServiceBusSendMessagesRequestDto azureServiceBusSendMessagesRequestDto)
        {
            ServiceBusSender serviceBusSender = null!;

            try
            {
                _logger.LogInformation("Begin SendMessagesAsync {@AzureServiceBusSendMessagesRequestDto}.", azureServiceBusSendMessagesRequestDto);

                if (azureServiceBusSendMessagesRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<AzureServiceBusSendMessagesResponseDto>(new ArgumentNullException(nameof(azureServiceBusSendMessagesRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error SendMessagesAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                serviceBusSender = _serviceBusClient.CreateSender(azureServiceBusSendMessagesRequestDto.QueueOrTopicName);

                using (var serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync())
                {
                    foreach (var message in azureServiceBusSendMessagesRequestDto.Messages)
                    {
                        if (!serviceBusMessageBatch.TryAddMessage(message))
                        {
                            var httpResponseDto1 = new HttpResponseDto<AzureServiceBusSendMessagesResponseDto>(new ArgumentOutOfRangeException(nameof(azureServiceBusSendMessagesRequestDto)).Message, StatusCodes.Status400BadRequest);
                            _logger.LogError("Error SendMessagesAsync {@HttpResponseDto}.", httpResponseDto1);
                            return httpResponseDto1;
                        }
                    }

                    await serviceBusSender.SendMessagesAsync(serviceBusMessageBatch);

                    var azureServiceBusSendMessagesResponseDto = new AzureServiceBusSendMessagesResponseDto();

                    var httpResponseDto = new HttpResponseDto<AzureServiceBusSendMessagesResponseDto>(azureServiceBusSendMessagesResponseDto, StatusCodes.Status200OK);
                    _logger.LogInformation("Done SendMessagesAsync {@HttpResponseDto}.", httpResponseDto);
                    return httpResponseDto;
                }
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AzureServiceBusSendMessagesResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error SendMessagesAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            finally
            {
                if (serviceBusSender != null)
                {
                    await serviceBusSender.DisposeAsync();
                }
            }
        }

        public async Task<HttpResponseDto<AzureServiceBusStartProcessingResponseDto>> StartProcessingAsync(AzureServiceBusStartProcessingRequestDto azureServiceBusStartProcessingRequestDto)
        {
            ServiceBusProcessor serviceBusProcessor = null!;

            try
            {
                _logger.LogInformation("Begin StartProcessingAsync {@AzureServiceBusStartProcessingRequestDto}.", azureServiceBusStartProcessingRequestDto);

                if (azureServiceBusStartProcessingRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<AzureServiceBusStartProcessingResponseDto>(new ArgumentNullException(nameof(azureServiceBusStartProcessingRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error StartProcessingAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                serviceBusProcessor = _serviceBusClient.CreateProcessor(azureServiceBusStartProcessingRequestDto.QueueName, new ServiceBusProcessorOptions());

                serviceBusProcessor.ProcessMessageAsync += azureServiceBusStartProcessingRequestDto.ProcessMessageAsync;
                serviceBusProcessor.ProcessErrorAsync += azureServiceBusStartProcessingRequestDto.ProcessErrorAsync;

                await serviceBusProcessor.StartProcessingAsync();

                var azureServiceBusStartProcessingResponseDto = new AzureServiceBusStartProcessingResponseDto();

                var httpResponseDto = new HttpResponseDto<AzureServiceBusStartProcessingResponseDto>(azureServiceBusStartProcessingResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done StartProcessingAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AzureServiceBusStartProcessingResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error StartProcessingAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            finally
            {
                if (serviceBusProcessor != null)
                {
                    await serviceBusProcessor.DisposeAsync();
                }
            }
        }
    }
}
