using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class DeleteAddressRequestHandler : IRequestHandler<DeleteAddressRequest, HttpResponseDto<DeleteAddressResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteAddressRequestDto> _validator;

        private readonly ILogger<DeleteAddressRequestHandler> _logger;

        public DeleteAddressRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteAddressRequestDto> validator, ILogger<DeleteAddressRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<DeleteAddressResponseDto>> Handle(DeleteAddressRequest deleteAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteAddress {@DeleteAddressRequest}.", deleteAddressRequest);

                if (deleteAddressRequest.DeleteAddressRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteAddressResponseDto>(new ArgumentNullException(nameof(deleteAddressRequest.DeleteAddressRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteAddressRequest.DeleteAddressRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<DeleteAddressResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var address = await _unitOfWork.AddressRepository.ReadByIdAsync(deleteAddressRequest.DeleteAddressRequestDto.Id);
                var deletedAddress = await _unitOfWork.AddressRepository.DeleteAsync(address);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<DeleteAddressResponseDto>(new DeleteAddressResponseDto
                {
                    Id = deletedAddress.Id,
                    DeletedAt = deletedAddress.DeletedAt,
                    DeletedBy = deletedAddress.DeletedBy,
                }, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DeleteAddressResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
