using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class UpdateCompanyRequestHandler : IRequestHandler<UpdateCompanyRequest, HttpResponseDto<UpdateCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateCompanyRequestDto> _validator;

        public UpdateCompanyRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateCompanyRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<UpdateCompanyResponseDto>> Handle(UpdateCompanyRequest updateCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (updateCompanyRequest.UpdateCompanyRequestDto == null)
                {
                    return new HttpResponseDto<UpdateCompanyResponseDto>(new ArgumentNullException(nameof(updateCompanyRequest.UpdateCompanyRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(updateCompanyRequest.UpdateCompanyRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<UpdateCompanyResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var company = await _unitOfWork.CompanyRepository.ReadByIdAsync(updateCompanyRequest.UpdateCompanyRequestDto.Id);
                _mapper.Map(updateCompanyRequest.UpdateCompanyRequestDto, company);
                var updatedCompany = await _unitOfWork.CompanyRepository.UpdateAsync(company);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<UpdateCompanyResponseDto>(new UpdateCompanyResponseDto
                {
                    Id = updatedCompany.Id,
                    UpdatedAt = updatedCompany.UpdatedAt,
                    UpdatedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<UpdateCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
