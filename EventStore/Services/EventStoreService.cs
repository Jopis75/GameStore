using Application.Aggregates;
using Application.Events;
using Application.Interfaces.EventSourcing;
using Application.Interfaces.EventSourcing.Producers;
using Application.Models.Event;

namespace EventSourcing.Services
{
    public class EventStoreService(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer) : IEventStoreService
    {
        public async Task<IEnumerable<EventBase>> ReadByAggregateIdAsync(int aggregateId)
        {
            var eventModels = await eventStoreRepository
                .ReadByAggregateIdAsync(aggregateId)
                .ConfigureAwait(false);

            return eventModels
                .OrderBy(eventModel => eventModel.Version)
                .Select(eventModel => eventModel.Event);
        }

        public async Task SaveAsync<TAggregate>(string topic, int aggregateId, IEnumerable<EventBase> events, int expectedVersion)
            where TAggregate : AggregateRoot
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
                    AggregateType = typeof(TAggregate).Name,
                    Version = version,
                    EventType = @event.GetType().Name,
                    Event = @event,
                    TimeStamp = DateTime.Now
                };

                await eventStoreRepository
                    .SaveAsync(eventModel)
                    .ConfigureAwait(false);

                await eventProducer
                    .ProduceAsync(topic, @event)
                    .ConfigureAwait(false);
            }
        }
    }
}
