using Application.Dtos.Common;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class UpdateCompanyRequestHandler : IRequestHandler<UpdateCompanyRequest, HttpResponseDto<CompanyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<CompanyDto> _validator;

        private readonly ILogger<UpdateCompanyRequestHandler> _logger;

        public UpdateCompanyRequestHandler(IUnitOfWork unitOfWork, IValidator<CompanyDto> validator, ILogger<UpdateCompanyRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CompanyDto>> Handle(UpdateCompanyRequest updateCompanyRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateCompany {@UpdateCompanyRequest}.", updateCompanyRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (updateCompanyRequest.CompanyDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ArgumentNullException(nameof(updateCompanyRequest.UpdateCompanyRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateCompanyRequest.CompanyDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var updatedCompanyDto = await _unitOfWork.CompanyRepository.UpdateAsync(updateCompanyRequest.CompanyDto);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CompanyDto>(updatedCompanyDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateCompany {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateCompany {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
