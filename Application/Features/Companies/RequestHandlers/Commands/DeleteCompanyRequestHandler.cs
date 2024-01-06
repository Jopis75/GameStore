using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class DeleteCompanyRequestHandler : IRequestHandler<DeleteCompanyRequest, HttpResponseDto<DeleteCompanyResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteCompanyRequestDto> _validator;

        private readonly ILogger<DeleteCompanyRequestHandler> _logger;

        public DeleteCompanyRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteCompanyRequestDto> validator, ILogger<DeleteCompanyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<DeleteCompanyResponseDto>> Handle(DeleteCompanyRequest deleteCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteCompany {@DeleteCompanyRequest}.", deleteCompanyRequest);

                if (deleteCompanyRequest.DeleteCompanyRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteCompanyResponseDto>(new ArgumentNullException(nameof(deleteCompanyRequest.DeleteCompanyRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteCompanyRequest.DeleteCompanyRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteCompanyResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var company = await _unitOfWork.CompanyRepository.ReadByIdAsync(deleteCompanyRequest.DeleteCompanyRequestDto.Id);
                var deletedCompany = await _unitOfWork.CompanyRepository.DeleteAsync(company);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<DeleteCompanyResponseDto>(new DeleteCompanyResponseDto
                {
                    Id = deletedCompany.Id,
                    DeletedAt = deletedCompany.DeletedAt,
                    DeletedBy = deletedCompany.DeletedBy,
                }, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteCompanyResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
