using Application.Aggregates;

namespace Application.Interfaces.EventSourcing.Handlers
{
    public interface IEventSourcingHandler<TAggregate> where TAggregate : AggregateRoot
    {
        Task<TAggregate> ReadByAggregateIdAsync(int aggregateId);

        Task SaveAsync(TAggregate aggregate);
    }
}
