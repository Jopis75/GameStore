using GameStore.Application.Aggregates;
using GameStore.Application.Interfaces.EventSourcing;
using GameStore.Application.Interfaces.EventSourcing.EventSourcingHandlers;

namespace GameStore.EventSourcing.EventSourcingHandlers
{
    public class AddressEventSourcingHandler(IEventStoreService eventStoreService) : EventSourcingHandlerBase<AddressAggregate>(eventStoreService), IAddressEventSourcingHandler
    {
    }
}
