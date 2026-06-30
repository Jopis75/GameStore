using Application.Events.Addresses;
using Application.Interfaces.EventSourcing.Handlers.Addresses;

namespace EventSourcing.Handlers.Addresses
{
    public class AddressEventHandler : IAddressEventHandler
    {
        public Task On(AddressCreatedEvent addressCreatedEvent)
        {
            throw new NotImplementedException();
        }

        public Task On(AddressDeletedEvent addressDeletedEvent)
        {
            throw new NotImplementedException();
        }

        public Task On(AddressUpdatedEvent addressUpdatedEvent)
        {
            throw new NotImplementedException();
        }
    }
}
