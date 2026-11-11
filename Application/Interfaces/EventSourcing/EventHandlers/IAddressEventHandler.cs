using GameStore.Application.Events.Addresses;

namespace GameStore.Application.Interfaces.EventSourcing.EventHandlers
{
    public interface IAddressEventHandler
    {
        Task Handle(AddressCreatedEvent addressCreatedEvent, CancellationToken cancellationToken);

        Task Handle(AddressDeletedEvent addressDeletedEvent, CancellationToken cancellationToken);

        Task Handle(AddressUpdatedEvent addressUpdatedEvent, CancellationToken cancellationToken);
    }
}
