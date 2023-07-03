using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadAllCompanyRequestHandler : IRequestHandler<ReadAllCompanyRequest, HttpResponseDto<ReadAllCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ReadAllCompanyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<HttpResponseDto<ReadAllCompanyResponseDto>> Handle(ReadAllCompanyRequest readAllCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                var companies = await _unitOfWork.CompanyRepository.ReadAllAsync(true);
                var readAllCompanyResponseDtos = companies.Select(_mapper.Map<ReadAllCompanyResponseDto>).ToList();

                return new HttpResponseDto<ReadAllCompanyResponseDto>(readAllCompanyResponseDtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadAllCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
