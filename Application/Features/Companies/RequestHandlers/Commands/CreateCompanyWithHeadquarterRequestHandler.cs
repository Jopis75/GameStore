using Application.Dtos.General;
using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Companies.RequestHandlers.Commands
{
    public class CreateCompanyWithHeadquarterRequestHandler : IRequestHandler<CreateCompanyWithHeadquarterRequest, HttpResponseDto<CompanyDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateCompanyWithHeadquarterRequest> _validator;

        private readonly ILogger<CreateCompanyWithHeadquarterRequestHandler> _logger;

        public CreateCompanyWithHeadquarterRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateCompanyWithHeadquarterRequest> validator, ILogger<CreateCompanyWithHeadquarterRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CompanyDto>> Handle(CreateCompanyWithHeadquarterRequest createCompanyWithHeadquarterRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateCompanyWithHeadquarter {@CreateCompanyWithHeadquarterRequest}.", createCompanyWithHeadquarterRequest);

                if (createCompanyWithHeadquarterRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ArgumentNullException(nameof(createCompanyWithHeadquarterRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createCompanyWithHeadquarterRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<CompanyDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressDto = _mapper.Map<AddressDto>(createCompanyWithHeadquarterRequest);
                var createdAddressDto = await _unitOfWork.AddressRepository.CreateAsync(addressDto, cancellationToken);

                createCompanyWithHeadquarterRequest.HeadquarterId = createdAddressDto.Id;

                var companyDto = _mapper.Map<CompanyDto>(createCompanyWithHeadquarterRequest);
                var createdCompanyDto = await _unitOfWork.CompanyRepository.CreateAsync(companyDto, cancellationToken);

                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CompanyDto>(createdCompanyDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CompanyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateCompanyWithHeadquarter {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
