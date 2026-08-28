using Application.Aggregates;
using Application.Interfaces.EventSourcing;
using Application.Interfaces.EventSourcing.EventSourcingHandlers;

namespace EventSourcing.EventSourcingHandlers
{
    public class AddressEventSourcingHandler(IEventStoreService eventStoreService) : EventSourcingHandlerBase<AddressAggregate>(eventStoreService), IAddressEventSourcingHandler
    {
    }
}
