using Application.Models.Event;

namespace Application.Interfaces.EventStore
{
    public interface IEventStoreRepository
    {
        Task<IEnumerable<EventModel>> ReadByAggregateIdAsync(Guid aggregateId);

        Task SaveAsync(EventModel eventModel);
    }
}
