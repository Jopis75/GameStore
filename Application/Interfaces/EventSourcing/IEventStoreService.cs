using Application.Events;

namespace Application.Interfaces.EventSourcing
{
    public interface IEventStoreService
    {
        Task<IEnumerable<EventBase>> ReadByAggregateIdAsync(Guid aggregateId);

        Task SaveAsync(Guid aggregateId, IEnumerable<EventBase> events, int expectedVersion);
    }
}
