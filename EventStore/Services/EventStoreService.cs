using Application.Aggregates.Addresses;
using Application.Events;
using Application.Interfaces.EventSourcing;
using Application.Models.Event;

namespace EventSourcing.Services
{
    public class EventStoreService(IEventStoreRepository eventStoreRepository) : IEventStoreService
    {
        public async Task<IEnumerable<EventBase>> ReadByAggregateIdAsync(Guid aggregateId)
        {
            var eventModels = await eventStoreRepository
                .ReadByAggregateIdAsync(aggregateId)
                .ConfigureAwait(false);

            return eventModels
                .OrderBy(eventModel => eventModel.Version)
                .Select(eventModel => eventModel.Event);
        }

        public async Task SaveAsync(Guid aggregateId, IEnumerable<EventBase> events, int expectedVersion)
        {
            if (expectedVersion != -1 && events.Last().Version != expectedVersion)
            {
                throw new InvalidOperationException("Concurrency conflict detected.");
            }

            var version = expectedVersion;

            foreach (var @event in events)
            {
                version++;

                @event.Version = version;

                var eventModel = new EventModel
                {
                    AggregateId = aggregateId,
                    AggregateType = nameof(AddressAggregate),
                    Version = version,
                    EventType = @event.GetType().Name,
                    Event = @event,
                    TimeStamp = DateTime.Now
                };

                await eventStoreRepository
                    .SaveAsync(eventModel)
                    .ConfigureAwait(false);
            }
        }
    }
}
