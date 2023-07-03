using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class DeleteCompanyRequestHandler : IRequestHandler<DeleteCompanyRequest, HttpResponseDto<DeleteCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteCompanyRequestDto> _validator;

        public DeleteCompanyRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteCompanyRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<DeleteCompanyResponseDto>> Handle(DeleteCompanyRequest deleteCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (deleteCompanyRequest.DeleteCompanyRequestDto == null)
                {
                    return new HttpResponseDto<DeleteCompanyResponseDto>(new ArgumentNullException(nameof(deleteCompanyRequest.DeleteCompanyRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(deleteCompanyRequest.DeleteCompanyRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<DeleteCompanyResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var company = await _unitOfWork.CompanyRepository.ReadByIdAsync(deleteCompanyRequest.DeleteCompanyRequestDto.Id);
                var deletedCompany = await _unitOfWork.CompanyRepository.DeleteAsync(company);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<DeleteCompanyResponseDto>(new DeleteCompanyResponseDto
                {
                    Id = deletedCompany.Id,
                    DeletedAt = deletedCompany.DeletedAt,
                    DeletedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<DeleteCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
