using Application.Dtos.Common;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadCompanyAllRequestHandler : IRequestHandler<ReadCompanyAllRequest, HttpResponseDto<List<CompanyDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<ReadCompanyAllRequestHandler> _logger;

        public ReadCompanyAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadCompanyAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<List<CompanyDto>>> Handle(ReadCompanyAllRequest readCompanyAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadCompanyAll {@ReadCompanyAllRequest}.", readCompanyAllRequest);

                cancellationToken.ThrowIfCancellationRequested();

                var companyDtos = await _unitOfWork.CompanyRepository.ReadAllAsync(true);

                var httpResponseDto = new HttpResponseDto<List<CompanyDto>>(companyDtos.ToList(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadCompanyAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<CompanyDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadCompanyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<CompanyDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadCompanyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
