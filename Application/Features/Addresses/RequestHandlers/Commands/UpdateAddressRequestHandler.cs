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
    public class UpdateAddressRequestHandler : IRequestHandler<UpdateAddressRequest, HttpResponseDto<AddressDto>>
    {
        private readonly IAddressRepository _addressRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateAddressRequest> _validator;

        private readonly ILogger<UpdateAddressRequestHandler> _logger;

        public UpdateAddressRequestHandler(IAddressRepository addressRepository, IMapper mapper, IValidator<UpdateAddressRequest> validator, ILogger<UpdateAddressRequestHandler> logger)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<AddressDto>> Handle(UpdateAddressRequest updateAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateAddress {@UpdateAddressRequest}.", updateAddressRequest);

                if (updateAddressRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateAddressRequest));
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateAddressRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressDto = _mapper.Map<AddressDto>(updateAddressRequest);
                var updatedAddressDto = await _addressRepository.UpdateAsync(addressDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<AddressDto>(updatedAddressDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
