using Application.Events.Addresses;

namespace Application.Interfaces.EventSourcing.Handlers.Addresses
{
    public interface IAddressEventHandler
    {
        Task On(AddressCreatedEvent addressCreatedEvent);

        Task On(AddressDeletedEvent addressDeletedEvent);

        Task On(AddressUpdatedEvent addressUpdatedEvent);
    }
}
