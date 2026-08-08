using Application.Events.Addresses;

namespace Application.Interfaces.EventSourcing.Handlers.Addresses
{
    public interface IAddressEventHandler
    {
        Task Handle(AddressCreatedEvent addressCreatedEvent, CancellationToken cancellationToken);

        Task Handle(AddressDeletedEvent addressDeletedEvent, CancellationToken cancellationToken);

        Task Handle(AddressUpdatedEvent addressUpdatedEvent, CancellationToken cancellationToken);
    }
}
