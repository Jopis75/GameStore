using GameStore.Application.Aggregates;

namespace GameStore.Application.Interfaces.EventSourcing.EventSourcingHandlers
{
    public interface IEventSourcingHandlerBase<TAggregate>
        where TAggregate : AggregateRoot
    {
        Task<TAggregate> ReadByAggregateIdAsync(int aggregateId);

        Task SaveAsync(string topic, TAggregate aggregate);
    }
}
