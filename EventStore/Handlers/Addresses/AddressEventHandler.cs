using Application.Events.Addresses;
using Application.Interfaces.EventSourcing.Handlers.Addresses;
using Application.Interfaces.Persistance;
using Domain.Dtos;

namespace EventSourcing.Handlers.Addresses
{
    public class AddressEventHandler(IAddressRepository addressRepository) : IAddressEventHandler
    {
        public async Task On(AddressCreatedEvent addressCreatedEvent, CancellationToken cancellationToken)
        {
            var addressDto = new AddressDto
            {
                StreetAddress = addressCreatedEvent.StreetAddress,
                PostalCode = addressCreatedEvent.PostalCode,
                City = addressCreatedEvent.City,
                State = addressCreatedEvent.State,
                Country = addressCreatedEvent.Country
            };

            await addressRepository.CreateAsync(addressDto, cancellationToken);
        }

        public async Task On(AddressDeletedEvent addressDeletedEvent, CancellationToken cancellationToken)
        {
            await addressRepository.DeleteByIdAsync(addressDeletedEvent.Id, cancellationToken);
        }

        public async Task On(AddressUpdatedEvent addressUpdatedEvent, CancellationToken cancellationToken)
        {
            var addressDto = new AddressDto
            {
                Id = addressUpdatedEvent.Id,
                StreetAddress = addressUpdatedEvent.StreetAddress,
                PostalCode = addressUpdatedEvent.PostalCode,
                City = addressUpdatedEvent.City,
                State = addressUpdatedEvent.State,
                Country = addressUpdatedEvent.Country
            };

            await addressRepository.UpdateAsync(addressDto, cancellationToken);
        }
    }
}
