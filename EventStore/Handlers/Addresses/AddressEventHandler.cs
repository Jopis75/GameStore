using Application.Events.Addresses;
using Application.Interfaces.EventSourcing.Handlers;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Microsoft.Extensions.Logging;

namespace EventSourcing.Handlers.Addresses
{
    public class AddressEventHandler(IAddressRepository addressRepository, IMapper mapper, ILogger<AddressEventHandler> logger) : IAddressEventHandler
    {
        public async Task Handle(AddressCreatedEvent addressCreatedEvent, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin HandleAddressCreatedEvent {@AddressCreatedEvent}.", addressCreatedEvent);

                var addressDto = mapper.Map<AddressDto>(addressCreatedEvent);

                await addressRepository.CreateAsync(addressDto, cancellationToken);

                logger.LogInformation("Done HandleAddressCreatedEvent {@AddressCreatedEvent}.", addressCreatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                logger.LogError(ex, "Canceled HandleAddressCreatedEvent {@AddressCreatedEvent}.", addressCreatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error HandleAddressCreatedEvent {@AddressCreatedEvent}.", addressCreatedEvent);
                throw;
            }
        }

        public async Task Handle(AddressDeletedEvent addressDeletedEvent, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin HandleAddressDeletedEvent {@AddressDeletedEvent}.", addressDeletedEvent);

                await addressRepository.DeleteByIdAsync(addressDeletedEvent.Id, cancellationToken);

                logger.LogInformation("Done HandleAddressDeletedEvent {@AddressDeletedEvent}.", addressDeletedEvent);
            }
            catch (OperationCanceledException ex)
            {
                logger.LogError(ex, "Canceled HandleAddressDeletedEvent {@AddressDeletedEvent}.", addressDeletedEvent);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error HandleAddressDeletedEvent {@AddressDeletedEvent}.", addressDeletedEvent);
                throw;
            }
        }

        public async Task Handle(AddressUpdatedEvent addressUpdatedEvent, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin HandleAddressUpdatedEvent {@AddressUpdatedEvent}.", addressUpdatedEvent);

                var addressDto = mapper.Map<AddressDto>(addressUpdatedEvent);

                await addressRepository.UpdateAsync(addressDto, cancellationToken);

                logger.LogInformation("Done HandleAddressUpdatedEvent {@AddressUpdatedEvent}.", addressUpdatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                logger.LogError(ex, "Canceled HandleAddressUpdatedEvent {@AddressUpdatedEvent}.", addressUpdatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error HandleAddressUpdatedEvent {@AddressUpdatedEvent}.", addressUpdatedEvent);
                throw;
            }
        }
    }
}
