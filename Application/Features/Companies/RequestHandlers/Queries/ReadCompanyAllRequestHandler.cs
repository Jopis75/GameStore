using Application.Dtos.General;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadCompanyAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadCompanyAllRequestHandler> logger) : IRequestHandler<ReadCompanyAllRequest, HttpResponseDto<CompanyDto>>
    {
        public async Task<HttpResponseDto<CompanyDto>> Handle(ReadCompanyAllRequest readCompanyAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadCompanyAll {@ReadCompanyAllRequest}.", readCompanyAllRequest);

                if (readCompanyAllRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readCompanyAllRequest));
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadCompanyAll {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var companyDtos = await unitOfWork.CompanyRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<CompanyDto>(companyDtos.ToArray(), StatusCodes.Status200OK);
                logger.LogInformation("Done ReadCompanyAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadCompanyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadCompanyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
