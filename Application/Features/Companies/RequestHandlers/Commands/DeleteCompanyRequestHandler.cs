using Application.Dtos.General;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.EventSourcing.EventSourcingHandlers;
using Application.Interfaces.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class DeleteCompanyRequestHandler(ICompanyEventSourcingHandler companyEventSourcingHandler, IValidator<DeleteCompanyRequest> validator, ILoggerService<DeleteCompanyRequestHandler> loggerService) : IRequestHandler<DeleteCompanyRequest, HttpResponseDto<DeleteCompanyRequest>>
    {
        public async Task<HttpResponseDto<DeleteCompanyRequest>> Handle(DeleteCompanyRequest deleteCompanyRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleDeleteCompanyRequest";
            var companyEventsTopicEnvironmentVariable = "COMPANY_EVENTS_TOPIC";

            try
            {
                loggerService.LogBeginInformation(methodName, deleteCompanyRequest);

                if (deleteCompanyRequest == null)
                {
                    return loggerService.LogArgumentNullException<DeleteCompanyRequest>(new ArgumentNullException(nameof(deleteCompanyRequest)), methodName);
                }

                var validationResult = await validator.ValidateAsync(deleteCompanyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    return loggerService.LogValidationException<DeleteCompanyRequest>(new ValidationException(validationResult.Errors), methodName);
                }

                var topic = Environment.GetEnvironmentVariable(companyEventsTopicEnvironmentVariable);

                if (topic == null)
                {
                    return loggerService.LogArgumentNullException<DeleteCompanyRequest>(new ArgumentNullException($"Could not find environment variable {companyEventsTopicEnvironmentVariable}."), methodName);
                }

                var companyAggregate = await companyEventSourcingHandler.ReadByAggregateIdAsync(deleteCompanyRequest.Id);
                companyAggregate.DeleteCompany();
                await companyEventSourcingHandler.SaveAsync(topic, companyAggregate);

                return loggerService.LogDoneInformation(methodName, deleteCompanyRequest, StatusCodes.Status200OK);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<DeleteCompanyRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<DeleteCompanyRequest>(ex, methodName);
            }
        }
    }
}
