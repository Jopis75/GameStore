using Application.Dtos.General;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadCompanyByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadCompanyByIdRequest> validator, ILogger<ReadCompanyByIdRequestHandler> logger) : IRequestHandler<ReadCompanyByIdRequest, HttpResponseDto<CompanyDto>>
    {
        public async Task<HttpResponseDto<CompanyDto>> Handle(ReadCompanyByIdRequest readCompanyByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadCompanyById {@ReadCompanyByIdRequest}.", readCompanyByIdRequest);

                if (readCompanyByIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readCompanyByIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadCompanyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(readCompanyByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadCompanyById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var companyDto = await unitOfWork.CompanyRepository.ReadByIdAsync(readCompanyByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<CompanyDto>(companyDto, StatusCodes.Status200OK);
                logger.LogInformation("Done ReadCompanyById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadCompanyById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadCompanyById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
