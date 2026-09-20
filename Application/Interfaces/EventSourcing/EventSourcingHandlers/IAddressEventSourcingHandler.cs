using Application.Aggregates;

namespace Application.Interfaces.EventSourcing.EventSourcingHandlers
{
    public interface IAddressEventSourcingHandler : IEventSourcingHandlerBase<AddressAggregate>
    {
    }
}
