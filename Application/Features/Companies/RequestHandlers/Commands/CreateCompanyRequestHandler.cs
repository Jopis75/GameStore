using FluentValidation;
using GameStore.Application.Aggregates;
using GameStore.Application.Dtos.General;
using GameStore.Application.Features.Companies.Requests.Commands;
using GameStore.Application.Interfaces.EventSourcing.EventSourcingHandlers;
using GameStore.Application.Interfaces.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Features.Companies.RequestHandlers.Commands
{
    public class CreateCompanyRequestHandler(ICompanyEventSourcingHandler companyEventSourcingHandler, IValidator<CreateCompanyRequest> validator, ILoggerService<CreateCompanyRequestHandler> loggerService) : IRequestHandler<CreateCompanyRequest, HttpResponseDto<CreateCompanyRequest>>
    {
        public async Task<HttpResponseDto<CreateCompanyRequest>> Handle(CreateCompanyRequest createCompanyRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleCreateCompanyRequest";
            var companyEventsTopicEnvironmentVariable = "COMPANY_EVENTS_TOPIC";

            try
            {
                loggerService.LogBeginInformation(methodName, createCompanyRequest);

                if (createCompanyRequest == null)
                {
                    return loggerService.LogArgumentNullException<CreateCompanyRequest>(new ArgumentNullException(nameof(createCompanyRequest)), methodName);
                }

                var validationResult = await validator.ValidateAsync(createCompanyRequest, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return loggerService.LogValidationException<CreateCompanyRequest>(new ValidationException(validationResult.Errors), methodName);
                }

                var topic = Environment.GetEnvironmentVariable(companyEventsTopicEnvironmentVariable);

                if (topic == null)
                {
                    return loggerService.LogArgumentNullException<CreateCompanyRequest>(new ArgumentNullException($"Could not find environment variable {companyEventsTopicEnvironmentVariable}."), methodName);
                }

                var companyAggregate = new CompanyAggregate(
                    createCompanyRequest.Name,
                    createCompanyRequest.TradeName,
                    createCompanyRequest.CompanyType,
                    createCompanyRequest.Industry,
                    createCompanyRequest.ParentCompanyId,
                    createCompanyRequest.HeadquarterId,
                    createCompanyRequest.LogoImageUri,
                    createCompanyRequest.EmailAddress,
                    createCompanyRequest.PhoneNumber,
                    createCompanyRequest.WebsiteUrl);

                await companyEventSourcingHandler.SaveAsync(topic, companyAggregate);

                return loggerService.LogDoneInformation(methodName, createCompanyRequest, StatusCodes.Status201Created);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<CreateCompanyRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<CreateCompanyRequest>(ex, methodName);
            }
        }
    }
}
