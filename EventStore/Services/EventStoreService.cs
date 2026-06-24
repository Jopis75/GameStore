using Application.Events;
using Application.Interfaces.EventStore;

namespace EventStore.Services
{
    public class EventStoreService(IEventStoreRepository eventStoreRepository) : IEventStoreService
    {
        public async Task<IEnumerable<EventBase>> ReadByAggregateIdAsync(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Guid aggregateId, IEnumerable<EventBase> events, int expectedVersion)
        {
            throw new NotImplementedException();
        }
    }
}
