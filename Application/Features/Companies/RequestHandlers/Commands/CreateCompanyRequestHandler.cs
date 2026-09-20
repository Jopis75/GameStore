using Application.Aggregates;
using Application.Dtos.General;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.EventSourcing.EventSourcingHandlers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class CreateCompanyRequestHandler(ICompanyEventSourcingHandler companyEventSourcingHandler, IValidator<CreateCompanyRequest> validator, ILogger<CreateCompanyRequestHandler> logger) : IRequestHandler<CreateCompanyRequest, HttpResponseDto<CreateCompanyRequest>>
    {
        private readonly string companyEventsTopicEnvironmentVariable = "COMPANY_EVENTS_TOPIC";

        public async Task<HttpResponseDto<CreateCompanyRequest>> Handle(CreateCompanyRequest createCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin HandleCreateCompany {@CreateCompanyRequest}.", createCompanyRequest);

                if (createCompanyRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(createCompanyRequest));
                    var httpResponseDto1 = new HttpResponseDto<CreateCompanyRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleCreateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(createCompanyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<CreateCompanyRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleCreateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var topic = Environment.GetEnvironmentVariable(companyEventsTopicEnvironmentVariable);

                if (topic == null)
                {
                    var ex = new ArgumentNullException($"Environment variable {companyEventsTopicEnvironmentVariable} is not found.");
                    var httpResponseDto1 = new HttpResponseDto<CreateCompanyRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleCreateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
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

                var httpResponseDto = new HttpResponseDto<CreateCompanyRequest>(createCompanyRequest, StatusCodes.Status201Created);
                logger.LogInformation("Done HandleCreateCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateCompanyRequest>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled HandleCreateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateCompanyRequest>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error HandleCreateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
