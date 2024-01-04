using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadCompanyAllRequestHandler : IRequestHandler<ReadCompanyAllRequest, HttpResponseDto<ReadCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadCompanyAllRequestHandler> _logger;

        public ReadCompanyAllRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadCompanyAllRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadCompanyResponseDto>> Handle(ReadCompanyAllRequest readAllCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAllCompany {@ReadAllCompanyRequest}.", readAllCompanyRequest);

                var companies = await _unitOfWork.CompanyRepository.ReadAllAsync(true);
                var readAllCompanyResponseDtos = companies
                    .Select(_mapper.Map<ReadCompanyResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<ReadCompanyResponseDto>(readAllCompanyResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadAllCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAllCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
