using Application.Dtos.General;
using Application.Features.Addresses.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Queries
{
    public class ReadAddressByIdRequestHandler : IRequestHandler<ReadAddressByIdRequest, HttpResponseDto<AddressDto>>
    {
        private readonly IAddressRepository _addressRepository;

        private readonly IValidator<ReadAddressByIdRequest> _validator;

        private readonly ILogger<ReadAddressByIdRequestHandler> _logger;

        public ReadAddressByIdRequestHandler(IAddressRepository addressRepository, IValidator<ReadAddressByIdRequest> validator, ILogger<ReadAddressByIdRequestHandler> logger)
        {
            _addressRepository = addressRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<AddressDto>> Handle(ReadAddressByIdRequest readAddressByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAddressById {@ReadAddressByIdRequest}.", readAddressByIdRequest);

                if (readAddressByIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(new ArgumentNullException(nameof(readAddressByIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readAddressByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressDto = await _addressRepository.ReadByIdAsync(readAddressByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<AddressDto>(addressDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadAddressById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
