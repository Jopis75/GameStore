using Application.Events.Addresses;
using Application.Interfaces.EventSourcing.Handlers.Addresses;
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
                logger.LogInformation("Begin HandleCreatedAddressEvent {@AddressCreatedEvent}.", addressCreatedEvent);

                var addressDto = mapper.Map<AddressDto>(addressCreatedEvent);

                await addressRepository.CreateAsync(addressDto, cancellationToken);

                logger.LogInformation("Done HandleCreatedAddressEvent {@AddressCreatedEvent}.", addressCreatedEvent);
            }
            catch (OperationCanceledException ex)
            {
                logger.LogError(ex, "Canceled HandleCreatedAddressEvent {@AddressCreatedEvent}.", addressCreatedEvent);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error HandleCreatedAddressEvent {@AddressCreatedEvent}.", addressCreatedEvent);
                throw;
            }
        }

        public async Task Handle(AddressDeletedEvent addressDeletedEvent, CancellationToken cancellationToken)
        {
            await addressRepository.DeleteByIdAsync(addressDeletedEvent.Id, cancellationToken);
        }

        public async Task Handle(AddressUpdatedEvent addressUpdatedEvent, CancellationToken cancellationToken)
        {
            var addressDto = mapper.Map<AddressDto>(addressUpdatedEvent);

            await addressRepository.UpdateAsync(addressDto, cancellationToken);
        }
    }
}
