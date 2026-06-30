using Application.Aggregates;

namespace Application.Interfaces.EventSourcing
{
    public interface IEventSourcingHandler<TAggregate> where TAggregate : AggregateRoot
    {
        Task<TAggregate> ReadByAggregateIdAsync(Guid aggregateId);

        Task SaveAsync(TAggregate aggregate);
    }
}
