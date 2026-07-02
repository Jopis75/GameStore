using Application.Models.Event;

namespace Application.Interfaces.EventSourcing
{
    public interface IEventStoreRepository
    {
        Task<IEnumerable<EventModel>> ReadByAggregateIdAsync(int aggregateId);

        Task SaveAsync(EventModel eventModel);
    }
}
