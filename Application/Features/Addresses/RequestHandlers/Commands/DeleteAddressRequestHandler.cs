using Application.Dtos.General;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.EventSourcing.EventSourcingHandlers;
using Application.Interfaces.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class DeleteAddressRequestHandler(IAddressEventSourcingHandler addressEventSourcingHandler, IValidator<DeleteAddressRequest> validator, ILoggerService<DeleteAddressRequestHandler> loggerService) : IRequestHandler<DeleteAddressRequest, HttpResponseDto<DeleteAddressRequest>>
    {
        private readonly string addressEventsTopicEnvironmentVariable = "ADDRESS_EVENTS_TOPIC";

        public async Task<HttpResponseDto<DeleteAddressRequest>> Handle(DeleteAddressRequest deleteAddressRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleDeleteAddressRequest";
            var addressEventsTopicEnvironmentVariable = "ADDRESS_EVENTS_TOPIC";

            try
            {
                loggerService.LogBeginInformation(methodName, deleteAddressRequest);

                if (deleteAddressRequest == null)
                {
                    return loggerService.LogArgumentNullException<DeleteAddressRequest>(new ArgumentNullException(nameof(deleteAddressRequest)), methodName);
                }

                var validationResult = await validator.ValidateAsync(deleteAddressRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    return loggerService.LogValidationException<DeleteAddressRequest>(new ValidationException(validationResult.Errors), methodName);
                }

                var topic = Environment.GetEnvironmentVariable(addressEventsTopicEnvironmentVariable);

                if (topic == null)
                {
                    return loggerService.LogArgumentNullException<DeleteAddressRequest>(new ArgumentNullException($"Environment variable {addressEventsTopicEnvironmentVariable} is not found."), methodName);
                }

                var addressAggregate = await addressEventSourcingHandler.ReadByAggregateIdAsync(deleteAddressRequest.Id);
                addressAggregate.DeleteAddress();
                await addressEventSourcingHandler.SaveAsync(topic, addressAggregate);

                return loggerService.LogDoneInformation(methodName, deleteAddressRequest, StatusCodes.Status200OK);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<DeleteAddressRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<DeleteAddressRequest>(ex, methodName);
            }
        }
    }
}
