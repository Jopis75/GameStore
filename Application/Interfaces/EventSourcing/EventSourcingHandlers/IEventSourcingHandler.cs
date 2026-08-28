using Application.Aggregates;

namespace Application.Interfaces.EventSourcing.EventSourcingHandlers
{
    public interface IEventSourcingHandler<TAggregate>
        where TAggregate : AggregateRoot
    {
        Task<TAggregate> ReadByAggregateIdAsync(int aggregateId);

        Task SaveAsync(string topic, TAggregate aggregate);
    }
}
