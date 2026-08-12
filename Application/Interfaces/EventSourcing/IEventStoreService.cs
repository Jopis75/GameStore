using Application.Aggregates;
using Application.Events;

namespace Application.Interfaces.EventSourcing
{
    public interface IEventStoreService
    {
        Task<IEnumerable<EventBase>> ReadByAggregateIdAsync(int aggregateId);

        Task SaveAsync<TAggregate>(string topic, int aggregateId, IEnumerable<EventBase> events, int expectedVersion)
            where TAggregate : AggregateRoot;
    }
}
