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
    public class UpdateAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateAddressRequest> validator, ILogger<UpdateAddressRequestHandler> logger) : IRequestHandler<UpdateAddressRequest, HttpResponseDto<AddressDto>>
    {
        public async Task<HttpResponseDto<AddressDto>> Handle(UpdateAddressRequest updateAddressRequest, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                logger.LogInformation("Begin UpdateAddress {@UpdateAddressRequest}.", updateAddressRequest);

                if (updateAddressRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateAddressRequest));
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(updateAddressRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressDto = mapper.Map<AddressDto>(updateAddressRequest);
                var updatedAddressDto = await unitOfWork.AddressRepository.UpdateAsync(addressDto, cancellationToken);

                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<AddressDto>(updatedAddressDto, StatusCodes.Status200OK);
                logger.LogInformation("Done UpdateAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error UpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
