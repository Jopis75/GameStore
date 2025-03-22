using Application.Dtos.General;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class DeleteAddressRequestHandler : IRequestHandler<DeleteAddressRequest, HttpResponseDto<AddressDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteAddressRequest> _validator;

        private readonly ILogger<DeleteAddressRequestHandler> _logger;

        public DeleteAddressRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteAddressRequest> validator, ILogger<DeleteAddressRequestHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<AddressDto>> Handle(DeleteAddressRequest deleteAddressRequest, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                _logger.LogInformation("Begin DeleteAddress {@DeleteAddressRequest}.", deleteAddressRequest);

                if (deleteAddressRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteAddressRequest));
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteAddressRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedAddressDto = await _unitOfWork.AddressRepository.DeleteByIdAsync(deleteAddressRequest.Id, cancellationToken);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<AddressDto>(deletedAddressDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled DeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error DeleteAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
