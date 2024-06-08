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
    public class CreateCompanyWithAddressRequestHandler : IRequestHandler<CreateCompanyWithAddressRequest, HttpResponseDto<CompanyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<CompanyDto> _validator;

        private readonly ILogger<CreateCompanyWithAddressRequestHandler> _logger;

        public CreateCompanyWithAddressRequestHandler(IUnitOfWork unitOfWork, IValidator<CompanyDto> validator, ILogger<CreateCompanyWithAddressRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CompanyDto>> Handle(CreateCompanyWithAddressRequest createCompanyWithAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateCompanyWithAddress {@CreateCompanyWithRequest}.", createCompanyWithAddressRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (createCompanyWithAddressRequest.CompanyDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ArgumentNullException(nameof(createCompanyWithAddressRequest.CompanyDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateCompanyWithAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createCompanyWithAddressRequest.CompanyDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateCompanyWithAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var createdAddressDto = await _unitOfWork.AddressRepository.CreateAsync(createCompanyWithAddressRequest.CompanyDto.Headquarter);
                createCompanyWithAddressRequest.CompanyDto.HeadquarterId = createdAddressDto.Id;
                var createdCompanyDto = await _unitOfWork.CompanyRepository.CreateAsync(createCompanyWithAddressRequest.CompanyDto);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CompanyDto>(createdCompanyDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateCompanyWithAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateCompanyWithAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateCompanyWithAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
