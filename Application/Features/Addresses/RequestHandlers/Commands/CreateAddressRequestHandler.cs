using Application.Aggregates;
using Application.Dtos.General;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.EventSourcing.EventSourcingHandlers;
using Application.Interfaces.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class CreateAddressRequestHandler(IAddressEventSourcingHandler addressEventSourcingHandler, IValidator<CreateAddressRequest> validator, ILoggerService<CreateAddressRequestHandler> loggerService) : IRequestHandler<CreateAddressRequest, HttpResponseDto<CreateAddressRequest>>
    {
        public async Task<HttpResponseDto<CreateAddressRequest>> Handle(CreateAddressRequest createAddressRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleCreateAddressRequest";
            var addressEventsTopicEnvironmentVariable = "ADDRESS_EVENTS_TOPIC";

            try
            {
                loggerService.LogBeginInformation(methodName, createAddressRequest);

                if (createAddressRequest == null)
                {
                    return loggerService.LogArgumentNullException<CreateAddressRequest>(new ArgumentNullException(nameof(createAddressRequest)), methodName);
                }

                var validationResult = await validator.ValidateAsync(createAddressRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    return loggerService.LogValidationException<CreateAddressRequest>(new ValidationException(validationResult.Errors), methodName);
                }

                var topic = Environment.GetEnvironmentVariable(addressEventsTopicEnvironmentVariable);

                if (topic == null)
                {
                    return loggerService.LogArgumentNullException<CreateAddressRequest>(new ArgumentNullException($"Could not find environment variable {addressEventsTopicEnvironmentVariable}."), methodName);
                }

                var addressAggregate = new AddressAggregate(createAddressRequest.StreetAddress, createAddressRequest.PostalCode, createAddressRequest.City, createAddressRequest.State, createAddressRequest.Country);
                await addressEventSourcingHandler.SaveAsync(topic, addressAggregate);

                return loggerService.LogDoneInformation(methodName, createAddressRequest, StatusCodes.Status201Created);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<CreateAddressRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<CreateAddressRequest>(ex, methodName);
            }
        }
    }
}
