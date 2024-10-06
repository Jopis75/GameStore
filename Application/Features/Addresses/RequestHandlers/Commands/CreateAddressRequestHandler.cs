using Application.Dtos.General;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class CreateAddressRequestHandler : IRequestHandler<CreateAddressRequest, HttpResponseDto<AddressDto>>
    {
        private readonly IAddressRepository _addressRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateAddressRequest> _validator;

        private readonly ILogger<CreateAddressRequestHandler> _logger;

        public CreateAddressRequestHandler(IAddressRepository addressRepository, IMapper mapper, IValidator<CreateAddressRequest> validator, ILogger<CreateAddressRequestHandler> logger)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<AddressDto>> Handle(CreateAddressRequest createAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateAddress {@CreateAddressRequest}.", createAddressRequest);

                if (createAddressRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(new ArgumentNullException(nameof(createAddressRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createAddressRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressDto = _mapper.Map<AddressDto>(createAddressRequest);
                var createdAddressDto = await _addressRepository.CreateAsync(addressDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<AddressDto>(createdAddressDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
