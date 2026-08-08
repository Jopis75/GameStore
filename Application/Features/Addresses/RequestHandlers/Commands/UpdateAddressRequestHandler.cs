using Application.Aggregates.Addresses;
using Application.Dtos.General;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.EventSourcing.Handlers;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class UpdateAddressRequestHandler(IEventSourcingHandler<AddressAggregate> eventSourcingHandler, IMapper mapper, IValidator<UpdateAddressRequest> validator, ILogger<UpdateAddressRequestHandler> logger) : IRequestHandler<UpdateAddressRequest, HttpResponseDto<AddressDto>>
    {
        public async Task<HttpResponseDto<AddressDto>> Handle(UpdateAddressRequest updateAddressRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin HandleUpdateAddress {@UpdateAddressRequest}.", updateAddressRequest);

                if (updateAddressRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateAddressRequest));
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleUpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(updateAddressRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error HandleUpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var addressAggregate = await eventSourcingHandler.ReadByAggregateIdAsync(updateAddressRequest.Id);

                addressAggregate.UpdateAddress(
                    updateAddressRequest.StreetAddress,
                    updateAddressRequest.PostalCode,
                    updateAddressRequest.City,
                    updateAddressRequest.State,
                    updateAddressRequest.Country);

                await eventSourcingHandler.SaveAsync(addressAggregate);

                var addressDto = mapper.Map<AddressDto>(updateAddressRequest);

                var httpResponseDto = new HttpResponseDto<AddressDto>(addressDto, StatusCodes.Status200OK);
                logger.LogInformation("Done HandleUpdateAddress {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled HandleUpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error HandleUpdateAddress {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
