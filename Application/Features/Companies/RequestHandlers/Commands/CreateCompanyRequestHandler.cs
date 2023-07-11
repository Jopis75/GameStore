using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class CreateCompanyRequestHandler : IRequestHandler<CreateCompanyRequest, HttpResponseDto<CreateCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateCompanyRequestDto> _validator;

        public CreateCompanyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateCompanyRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<CreateCompanyResponseDto>> Handle(CreateCompanyRequest createCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (createCompanyRequest.CreateCompanyRequestDto == null)
                {
                    return new HttpResponseDto<CreateCompanyResponseDto>(new ArgumentNullException(nameof(createCompanyRequest.CreateCompanyRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(createCompanyRequest.CreateCompanyRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<CreateCompanyResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var company = _mapper.Map<Company>(createCompanyRequest.CreateCompanyRequestDto);
                var createdCompany = await _unitOfWork.CompanyRepository.CreateAsync(company);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<CreateCompanyResponseDto>(new CreateCompanyResponseDto
                {
                    Id = createdCompany.Id,
                    CreatedAt = createdCompany.CreatedAt,
                    CreatedBy = string.Empty
                }, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<CreateCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
