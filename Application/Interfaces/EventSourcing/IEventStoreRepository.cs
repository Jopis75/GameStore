using GameStore.Application.Models.Event;

namespace GameStore.Application.Interfaces.EventSourcing
{
    public interface IEventStoreRepository
    {
        Task<IEnumerable<EventModel>> ReadByAggregateIdAsync(int aggregateId);

        Task SaveAsync(EventModel eventModel);
    }
}
