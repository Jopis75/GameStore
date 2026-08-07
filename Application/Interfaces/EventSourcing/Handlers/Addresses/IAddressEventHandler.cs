using Application.Events.Addresses;

namespace Application.Interfaces.EventSourcing.Handlers.Addresses
{
    public interface IAddressEventHandler
    {
        Task On(AddressCreatedEvent addressCreatedEvent/*, CancellationToken cancellationToken*/);

        Task On(AddressDeletedEvent addressDeletedEvent/*, CancellationToken cancellationToken*/);

        Task On(AddressUpdatedEvent addressUpdatedEvent/*, CancellationToken cancellationToken*/);
    }
}
