using Application.Dtos.General;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.EventSourcing.EventSourcingHandlers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class UpdateCompanyRequestHandler(ICompanyEventSourcingHandler companyEventSourcingHandler, IValidator<UpdateCompanyRequest> validator, ILogger<UpdateCompanyRequestHandler> logger) : IRequestHandler<UpdateCompanyRequest, HttpResponseDto<UpdateCompanyRequest>>
    {
        private readonly string companyEventsTopicEnvironmentVariable = "COMPANY_EVENTS_TOPIC";

        public async Task<HttpResponseDto<UpdateCompanyRequest>> Handle(UpdateCompanyRequest updateCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin HandleUpdateCompany {@UpdateCompanyRequest}.", updateCompanyRequest);

                if (updateCompanyRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateCompanyRequest));
                    var httpResponseDto1 = new HttpResponseDto<UpdateCompanyRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleUpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(updateCompanyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<UpdateCompanyRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleUpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
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
                await companyEventSourcingHandler.SaveAsync(companyEventsTopicEnvironmentVariable, companyAggregate);

                var httpResponseDto = new HttpResponseDto<UpdateCompanyRequest>(updateCompanyRequest, StatusCodes.Status200OK);
                logger.LogInformation("Done HandleUpdateCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UpdateCompanyRequest>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled HandleUpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UpdateCompanyRequest>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error HandleUpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
