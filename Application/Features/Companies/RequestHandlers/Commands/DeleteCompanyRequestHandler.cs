using Application.Dtos.General;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.EventSourcing.EventSourcingHandlers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class DeleteCompanyRequestHandler(ICompanyEventSourcingHandler companyEventSourcingHandler, IValidator<DeleteCompanyRequest> validator, ILogger<DeleteCompanyRequestHandler> logger) : IRequestHandler<DeleteCompanyRequest, HttpResponseDto<DeleteCompanyRequest>>
    {
        private readonly string companyEventsTopicEnvironmentVariable = "COMPANY_EVENTS_TOPIC";

        public async Task<HttpResponseDto<DeleteCompanyRequest>> Handle(DeleteCompanyRequest deleteCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin HandleDeleteCompany {@DeleteCompanyRequest}.", deleteCompanyRequest);

                if (deleteCompanyRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteCompanyRequest));
                    var httpResponseDto1 = new HttpResponseDto<DeleteCompanyRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleDeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(deleteCompanyRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<DeleteCompanyRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleDeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var topic = Environment.GetEnvironmentVariable(companyEventsTopicEnvironmentVariable);

                if (topic == null)
                {
                    var ex = new ArgumentNullException($"Environment variable {companyEventsTopicEnvironmentVariable} is not found.");
                    var httpResponseDto1 = new HttpResponseDto<DeleteCompanyRequest>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleDeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var companyAggregate = await companyEventSourcingHandler.ReadByAggregateIdAsync(deleteCompanyRequest.Id);
                companyAggregate.DeleteCompany();
                await companyEventSourcingHandler.SaveAsync(topic, companyAggregate);

                var httpResponseDto = new HttpResponseDto<DeleteCompanyRequest>(deleteCompanyRequest, StatusCodes.Status200OK);
                logger.LogInformation("Done HandleDeleteCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteCompanyRequest>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled HandleDeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteCompanyRequest>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error HandleDeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
