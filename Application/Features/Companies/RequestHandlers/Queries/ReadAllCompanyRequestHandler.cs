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
    public class ReadAllCompanyRequestHandler : IRequestHandler<ReadAllCompanyRequest, HttpResponseDto<ReadAllCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<ReadAllCompanyRequestHandler> _logger;

        public ReadAllCompanyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ReadAllCompanyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReadAllCompanyResponseDto>> Handle(ReadAllCompanyRequest readAllCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAllCompany {@ReadAllCompanyRequest}.", readAllCompanyRequest);

                var companies = await _unitOfWork.CompanyRepository.ReadAllAsync(true);
                var readAllCompanyResponseDtos = companies
                    .Select(_mapper.Map<ReadAllCompanyResponseDto>)
                    .ToList();

                var httpResponseDto = new HttpResponseDto<ReadAllCompanyResponseDto>(readAllCompanyResponseDtos, StatusCodes.Status200OK);
                _logger.LogInformation("End ReadAllCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReadAllCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAllCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
