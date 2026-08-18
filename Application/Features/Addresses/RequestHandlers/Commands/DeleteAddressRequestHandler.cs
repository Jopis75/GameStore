using Application.Aggregates.Addresses;
using Application.Dtos.General;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.EventSourcing.Handlers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class DeleteAddressRequestHandler(IEventSourcingHandler<AddressAggregate> eventSourcingHandler, IValidator<DeleteAddressRequest> validator, ILogger<DeleteAddressRequestHandler> logger) : IRequestHandler<DeleteAddressRequest, HttpResponseDto<DeleteAddressRequest>>
    {
        private readonly string addressEventsTopicEnvironmentVariable = "ADDRESS_EVENTS_TOPIC";

        public async Task<HttpResponseDto<DeleteAddressRequest>> Handle(DeleteAddressRequest deleteAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin HandleDeleteAddress {@DeleteAddressRequest}.", deleteAddressRequest);

                if (deleteAddressRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteAddressRequest));
                    var httpResponseDto1 = new HttpResponseDto<DeleteAddressRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleDeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(deleteAddressRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<DeleteAddressRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleDeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var topic = Environment.GetEnvironmentVariable(addressEventsTopicEnvironmentVariable);

                if (topic == null)
                {
                    var ex = new ArgumentNullException($"Environment variable {addressEventsTopicEnvironmentVariable} is not found.");
                    var httpResponseDto1 = new HttpResponseDto<DeleteAddressRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleDeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressAggregate = await eventSourcingHandler.ReadByAggregateIdAsync(deleteAddressRequest.Id);
                addressAggregate.DeleteAddress();
                await eventSourcingHandler.SaveAsync(topic, addressAggregate);

                var httpResponseDto = new HttpResponseDto<DeleteAddressRequest>(deleteAddressRequest, StatusCodes.Status200OK);
                logger.LogInformation("Done HandleDeleteAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteAddressRequest>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled HandleDeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteAddressRequest>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error HandleDeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
