using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class CreateCompanyWithAddressRequestHandler : IRequestHandler<CreateCompanyWithAddressRequest, HttpResponseDto<CreateCompanyWithAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateCompanyWithAddressRequestDto> _validator;

        private readonly ILogger<CreateCompanyWithAddressRequestHandler> _logger;

        public CreateCompanyWithAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateCompanyWithAddressRequestDto> validator, ILogger<CreateCompanyWithAddressRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CreateCompanyWithAddressResponseDto>> Handle(CreateCompanyWithAddressRequest createCompanyWithAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateCompanyWithAddress {@CreateCompanyWithRequest}.", createCompanyWithAddressRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (createCompanyWithAddressRequest.CreateCompanyWithAddressRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateCompanyWithAddressResponseDto>(new ArgumentNullException(nameof(createCompanyWithAddressRequest.CreateCompanyWithAddressRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateCompanyWithAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createCompanyWithAddressRequest.CreateCompanyWithAddressRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateCompanyWithAddressResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateCompanyWithAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var address = _mapper.Map<Address>(createCompanyWithAddressRequest.CreateCompanyWithAddressRequestDto);
                var createdAddress = await _unitOfWork.AddressRepository.CreateAsync(address);
                var company = _mapper.Map<Company>(createCompanyWithAddressRequest.CreateCompanyWithAddressRequestDto);
                company.HeadquarterId = createdAddress.Id;
                var createdCompany = await _unitOfWork.CompanyRepository.CreateAsync(company);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CreateCompanyWithAddressResponseDto>(new CreateCompanyWithAddressResponseDto
                {
                    Id = createdCompany.Id,
                    AddressId = createdAddress.Id,
                    CreatedAt = createdCompany.CreatedAt,
                    CreatedBy = createdCompany.CreatedBy,
                }, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateCompanyWithAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateCompanyWithAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateCompanyWithAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateCompanyWithAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateCompanyWithAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
