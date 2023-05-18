using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Queries;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Companies.RequestHandlers.Queries
{
    public class ReadByIdCompanyRequestHandler : IRequestHandler<ReadByIdCompanyRequest, HttpResponseDto<ReadByIdCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<ReadByIdCompanyRequestDto> _validator;

        public ReadByIdCompanyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ReadByIdCompanyRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<ReadByIdCompanyResponseDto>> Handle(ReadByIdCompanyRequest readByIdCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (readByIdCompanyRequest.ReadByIdCompanyRequestDto == null)
                {
                    return new HttpResponseDto<ReadByIdCompanyResponseDto>(new ArgumentNullException(nameof(readByIdCompanyRequest.ReadByIdCompanyRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(readByIdCompanyRequest.ReadByIdCompanyRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<ReadByIdCompanyResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var company = await _unitOfWork.CompanyRepository.ReadByIdAsync(readByIdCompanyRequest.ReadByIdCompanyRequestDto.Id);
                var readByIdCompanyResponseDto = _mapper.Map<ReadByIdCompanyResponseDto>(company);

                return new HttpResponseDto<ReadByIdCompanyResponseDto>(readByIdCompanyResponseDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<ReadByIdCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
