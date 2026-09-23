using Application.Dtos.General;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.EventSourcing.EventSourcingHandlers;
using Application.Interfaces.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class UpdateAddressRequestHandler(IAddressEventSourcingHandler addressEventSourcingHandler, IValidator<UpdateAddressRequest> validator, ILoggerService<UpdateAddressRequestHandler> loggerService) : IRequestHandler<UpdateAddressRequest, HttpResponseDto<UpdateAddressRequest>>
    {
        public async Task<HttpResponseDto<UpdateAddressRequest>> Handle(UpdateAddressRequest updateAddressRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleUpdateAddressRequest";
            var addressEventsTopicEnvironmentVariable = "ADDRESS_EVENTS_TOPIC";

            try
            {
                loggerService.LogBeginInformation(methodName, updateAddressRequest);

                if (updateAddressRequest == null)
                {
                   return loggerService.LogArgumentNullException<UpdateAddressRequest>(new ArgumentNullException(nameof(updateAddressRequest)), methodName);
                }

                var validationResult = await validator.ValidateAsync(updateAddressRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    return loggerService.LogValidationException<UpdateAddressRequest>(new ValidationException(validationResult.Errors), methodName);
                }

                var topic = Environment.GetEnvironmentVariable(addressEventsTopicEnvironmentVariable);

                if (topic == null)
                {
                    return loggerService.LogArgumentNullException<UpdateAddressRequest>(new ArgumentNullException($"Could not find environment variable {addressEventsTopicEnvironmentVariable}."), methodName);
                }

                var addressAggregate = await addressEventSourcingHandler.ReadByAggregateIdAsync(updateAddressRequest.Id);
                addressAggregate.UpdateAddress(updateAddressRequest.StreetAddress, updateAddressRequest.PostalCode, updateAddressRequest.City, updateAddressRequest.State, updateAddressRequest.Country);
                await addressEventSourcingHandler.SaveAsync(topic, addressAggregate);

                return loggerService.LogDoneInformation(methodName, updateAddressRequest, StatusCodes.Status200OK);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<UpdateAddressRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<UpdateAddressRequest>(ex, methodName);
            }
        }
    }
}
