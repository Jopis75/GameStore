using Application.Aggregates.Addresses;
using Application.Dtos.General;
using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.EventSourcing.Handlers;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Addresses.RequestHandlers.Commands
{
    public class CreateAddressRequestHandler(IEventSourcingHandler<AddressAggregate> eventSourcingHandler, IMapper mapper, ILogger<CreateAddressRequestHandler> logger) : IRequestHandler<CreateAddressRequest, HttpResponseDto<AddressDto>>
    {
        public async Task<HttpResponseDto<AddressDto>> Handle(CreateAddressRequest createAddressRequest, CancellationToken cancellationToken)
        {
            logger.LogInformation("Begin CreateAddress {@CreateAddressRequest}.", createAddressRequest);

            var addressAggregate = new AddressAggregate(
                createAddressRequest.StreetAddress,
                createAddressRequest.PostalCode,
                createAddressRequest.City,
                createAddressRequest.State,
                createAddressRequest.Country);

            await eventSourcingHandler.SaveAsync(addressAggregate);

            var addressDto = mapper.Map<AddressDto>(addressAggregate);

            var httpResponseDto = new HttpResponseDto<AddressDto>(addressDto, StatusCodes.Status201Created);

            logger.LogInformation("Done CreateAddress {@HttpResponseDto}.", httpResponseDto);

            return httpResponseDto;
        }
    }

    //public class CreateAddressRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateAddressRequest> validator, ILogger<CreateAddressRequestHandler> logger) : IRequestHandler<CreateAddressRequest, HttpResponseDto<AddressDto>>
    //{
    //    public async Task<HttpResponseDto<AddressDto>> Handle(CreateAddressRequest createAddressRequest, CancellationToken cancellationToken)
    //    {
    //        await unitOfWork.BeginTransactionAsync(cancellationToken);

    //        try
    //        {
    //            logger.LogInformation("Begin CreateAddress {@CreateAddressRequest}.", createAddressRequest);

    //            if (createAddressRequest == null)
    //            {
    //                var ex = new ArgumentNullException(nameof(createAddressRequest));
    //                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
    //                logger.LogError(ex, "Error CreateAddress {@HttpResponseDto}.", httpResponseDto1);
    //                return httpResponseDto1;
    //            }

    //            var validationResult = await validator.ValidateAsync(createAddressRequest, cancellationToken);

    //            if (validationResult.IsValid == false)
    //            {
    //                var ex = new ValidationException(validationResult.Errors);
    //                var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status400BadRequest);
    //                logger.LogError(ex, "Error CreateAddress {@HttpResponseDto}.", httpResponseDto1);
    //                return httpResponseDto1;
    //            }

    //            var addressDto = mapper.Map<AddressDto>(createAddressRequest);
    //            var createdAddressDto = await unitOfWork.AddressRepository.CreateAsync(addressDto, cancellationToken);

    //            await unitOfWork.CommitTransactionAsync(cancellationToken);

    //            var httpResponseDto = new HttpResponseDto<AddressDto>(createdAddressDto, StatusCodes.Status201Created);
    //            logger.LogInformation("Done CreateAddress {@HttpResponseDto}.", httpResponseDto);
    //            return httpResponseDto;
    //        }
    //        catch (OperationCanceledException ex)
    //        {
    //            await unitOfWork.RollbackTransactionAsync(cancellationToken);

    //            var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
    //            logger.LogError(ex, "Canceled CreateAddress {@HttpResponseDto}.", httpResponseDto1);
    //            return httpResponseDto1;
    //        }
    //        catch (Exception ex)
    //        {
    //            await unitOfWork.RollbackTransactionAsync(cancellationToken);

    //            var httpResponseDto1 = new HttpResponseDto<AddressDto>(ex.Message, StatusCodes.Status500InternalServerError);
    //            logger.LogError(ex, "Error CreateAddress {@HttpResponseDto}.", httpResponseDto1);
    //            return httpResponseDto1;
    //        }
    //    }
    //}
}
