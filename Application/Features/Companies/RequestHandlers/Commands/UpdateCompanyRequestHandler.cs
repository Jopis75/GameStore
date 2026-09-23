using Application.Dtos.General;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.EventSourcing.EventSourcingHandlers;
using Application.Interfaces.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class UpdateCompanyRequestHandler(ICompanyEventSourcingHandler companyEventSourcingHandler, IValidator<UpdateCompanyRequest> validator, ILoggerService<UpdateCompanyRequestHandler> loggerService) : IRequestHandler<UpdateCompanyRequest, HttpResponseDto<UpdateCompanyRequest>>
    {
        public async Task<HttpResponseDto<UpdateCompanyRequest>> Handle(UpdateCompanyRequest updateCompanyRequest, CancellationToken cancellationToken)
        {
            var methodName = "HandleUpdateCompanyRequest";
            var companyEventsTopicEnvironmentVariable = "COMPANY_EVENTS_TOPIC";

            try
            {
                loggerService.LogBeginInformation(methodName, updateCompanyRequest);

                if (updateCompanyRequest == null)
                {
                    return loggerService.LogArgumentNullException<UpdateCompanyRequest>(new ArgumentNullException(nameof(updateCompanyRequest)), methodName);
                }

                var validationResult = await validator.ValidateAsync(updateCompanyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    return loggerService.LogValidationException<UpdateCompanyRequest>(new ValidationException(validationResult.Errors), methodName);
                }

                var topic = Environment.GetEnvironmentVariable(companyEventsTopicEnvironmentVariable);

                if (topic == null)
                {
                    return loggerService.LogArgumentNullException<UpdateCompanyRequest>(new ArgumentNullException($"Could not find environment variable {companyEventsTopicEnvironmentVariable}."), methodName);
                }

                var companyAggregate = await companyEventSourcingHandler.ReadByAggregateIdAsync(updateCompanyRequest.Id);
                companyAggregate.UpdateCompany(
                    updateCompanyRequest.Name,
                    updateCompanyRequest.TradeName,
                    updateCompanyRequest.CompanyType,
                    updateCompanyRequest.Industry,
                    updateCompanyRequest.ParentCompanyId,
                    updateCompanyRequest.HeadquarterId,
                    updateCompanyRequest.LogoImageUri,
                    updateCompanyRequest.EmailAddress,
                    updateCompanyRequest.PhoneNumber,
                    updateCompanyRequest.WebsiteUrl);
                await companyEventSourcingHandler.SaveAsync(topic, companyAggregate);

                return loggerService.LogDoneInformation(methodName, updateCompanyRequest, StatusCodes.Status200OK);
            }
            catch (OperationCanceledException ex)
            {
                return loggerService.LogOperationCanceledException<UpdateCompanyRequest>(ex, methodName);
            }
            catch (Exception ex)
            {
                return loggerService.LogException<UpdateCompanyRequest>(ex, methodName);
            }
        }
    }
}
