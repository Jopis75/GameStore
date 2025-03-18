using Application.Dtos.General;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadCompanyAllRequestHandler : IRequestHandler<ReadCompanyAllRequest, HttpResponseDto<CompanyDto>>
    {
        private readonly ICompanyRepository _companyRepository;

        private readonly ILogger<ReadCompanyAllRequestHandler> _logger;

        public ReadCompanyAllRequestHandler(ICompanyRepository companyRepository, ILogger<ReadCompanyAllRequestHandler> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<HttpResponseDto<CompanyDto>> Handle(ReadCompanyAllRequest readCompanyAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadCompanyAll {@ReadCompanyAllRequest}.", readCompanyAllRequest);

                var companyDtos = await _companyRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<CompanyDto>(companyDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadCompanyAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled ReadCompanyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error ReadCompanyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
