using Application.Aggregates;

namespace Application.Interfaces.EventStore
{
    public interface IEventSourcingHandler<TAggregate> where TAggregate : AggregateRoot
    {
        Task<TAggregate> GetByIdAsync(Guid aggregateId);

        Task SaveAsync(TAggregate aggregate);
    }
}
