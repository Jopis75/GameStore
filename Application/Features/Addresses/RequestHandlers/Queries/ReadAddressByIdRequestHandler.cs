using Application.Dtos.General;
using Application.Exceptions;
using Application.Features.Addresses.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Queries
{
    public class ReadAddressByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadAddressByIdRequest> validator, ILogger<ReadAddressByIdRequestHandler> logger) : IRequestHandler<ReadAddressByIdRequest, HttpResponseDto<AddressDto>>
    {
        public async Task<HttpResponseDto<AddressDto>> Handle(ReadAddressByIdRequest readAddressByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadAddressById {@ReadAddressByIdRequest}.", readAddressByIdRequest);

                if (readAddressByIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readAddressByIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(readAddressByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressDto = await unitOfWork.AddressRepository.ReadByIdAsync(readAddressByIdRequest.Id, cancellationToken);

                if (addressDto.IsNullObject)
                {
                    var ex = new NotFoundException($"Could not find Address object with Id {readAddressByIdRequest.Id}.");
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status404NotFound);
                    logger.LogError(ex, "Error ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var httpResponseDto = new HttpResponseDto<AddressDto>(addressDto, StatusCodes.Status200OK);
                logger.LogInformation("Done ReadAddressById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadAddressById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
