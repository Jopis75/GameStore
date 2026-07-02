using Application.Events;

namespace Application.Interfaces.EventSourcing
{
    public interface IEventStoreService
    {
        Task<IEnumerable<EventBase>> ReadByAggregateIdAsync(int aggregateId);

        Task SaveAsync(int aggregateId, IEnumerable<EventBase> events, int expectedVersion);
    }
}
