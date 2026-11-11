using GameStore.Application.Aggregates;

namespace GameStore.Application.Interfaces.EventSourcing.EventSourcingHandlers
{
    public interface IAddressEventSourcingHandler : IEventSourcingHandlerBase<AddressAggregate>
    {
    }
}
