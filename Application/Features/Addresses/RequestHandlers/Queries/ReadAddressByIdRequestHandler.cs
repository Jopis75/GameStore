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
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ReadAddressByIdRequest> _validator;

        private readonly ILogger<ReadAddressByIdRequestHandler> _logger;

        public ReadAddressByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadAddressByIdRequest> validator, ILogger<ReadAddressByIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<AddressDto>> Handle(ReadAddressByIdRequest readAddressByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadAddressById {@ReadAddressByIdRequest}.", readAddressByIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

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

                var addressDto = await _unitOfWork.AddressRepository.ReadByIdAsync(readAddressByIdRequest.Id, cancellationToken);

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
