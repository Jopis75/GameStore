using Application.Events.Addresses;
using Application.Interfaces.EventSourcing.Handlers.Addresses;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;

namespace EventSourcing.Handlers.Addresses
{
    public class AddressEventHandler(IAddressRepository addressRepository, IMapper mapper) : IAddressEventHandler
    {
        public async Task On(AddressCreatedEvent addressCreatedEvent/*, CancellationToken cancellationToken*/)
        {
            var addressDto = mapper.Map<AddressDto>(addressCreatedEvent);

            await addressRepository.CreateAsync(addressDto, CancellationToken.None);
        }

        public async Task On(AddressDeletedEvent addressDeletedEvent/*, CancellationToken cancellationToken*/)
        {
            await addressRepository.DeleteByIdAsync(addressDeletedEvent.Id, new CancellationToken());
        }

        public async Task On(AddressUpdatedEvent addressUpdatedEvent/*, CancellationToken cancellationToken*/)
        {
            var addressDto = mapper.Map<AddressDto>(addressUpdatedEvent);

            await addressRepository.UpdateAsync(addressDto, new CancellationToken());
        }
    }
}
