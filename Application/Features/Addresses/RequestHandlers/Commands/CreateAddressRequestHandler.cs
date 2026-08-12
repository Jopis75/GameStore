using Application.Aggregates.Addresses;
using Application.Dtos.General;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.EventSourcing.Handlers;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class CreateAddressRequestHandler(IEventSourcingHandler<AddressAggregate> eventSourcingHandler, IMapper mapper, IValidator<CreateAddressRequest> validator, ILogger<CreateAddressRequestHandler> logger) : IRequestHandler<CreateAddressRequest, HttpResponseDto<CreateAddressRequest>>
    {
        public async Task<HttpResponseDto<CreateAddressRequest>> Handle(CreateAddressRequest createAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin HandleCreateAddress {@CreateAddressRequest}.", createAddressRequest);

                if (createAddressRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createAddressRequest));
                    var httpResponseDto1 = new HttpResponseDto<CreateAddressRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleCreateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(createAddressRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<CreateAddressRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleCreateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");

                if (topic == null)
                {
                    var ex = new ArgumentNullException("KAFKA_TOPIC environment variable is not set.");
                    var httpResponseDto1 = new HttpResponseDto<CreateAddressRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleCreateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressAggregate = new AddressAggregate(
                    createAddressRequest.StreetAddress,
                    createAddressRequest.PostalCode,
                    createAddressRequest.City,
                    createAddressRequest.State,
                    createAddressRequest.Country);

                await eventSourcingHandler.SaveAsync(topic, addressAggregate);

                var httpResponseDto = new HttpResponseDto<CreateAddressRequest>(createAddressRequest, StatusCodes.Status201Created);
                logger.LogInformation("Done HandleCreateAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateAddressRequest>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled HandleCreateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateAddressRequest>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error HandleCreateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
