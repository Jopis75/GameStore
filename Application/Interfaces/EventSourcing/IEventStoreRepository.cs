using Application.Models.Event;

namespace Application.Interfaces.EventSourcing
{
    public interface IEventStoreRepository
    {
        Task<IEnumerable<EventModel>> ReadByAggregateIdAsync(Guid aggregateId);

        Task SaveAsync(EventModel eventModel);
    }
}
