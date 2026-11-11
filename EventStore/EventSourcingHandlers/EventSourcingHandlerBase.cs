using GameStore.Application.Aggregates;
using GameStore.Application.Interfaces.EventSourcing;
using GameStore.Application.Interfaces.EventSourcing.EventSourcingHandlers;

namespace GameStore.EventSourcing.EventSourcingHandlers
{
    public abstract class EventSourcingHandlerBase<TAggregate>(IEventStoreService eventStoreService) : IEventSourcingHandlerBase<TAggregate>
        where TAggregate : AggregateRoot, new()
    {
        public async Task<TAggregate> ReadByAggregateIdAsync(int aggregateId)
        {
            var aggregate = new TAggregate();

            var events = await eventStoreService
                .ReadByAggregateIdAsync(aggregateId)
                .ConfigureAwait(false);

            if (events == null || !events.Any())
            {
                return aggregate;
            }

            aggregate.ReplayEvents(events);
            aggregate.Version = events.Max(@event => @event.Version);

            return aggregate;
        }

        public async Task SaveAsync(string topic, TAggregate aggregate)
        {
            await eventStoreService
                .SaveAsync<TAggregate>(topic, aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version)
                .ConfigureAwait(false);

            aggregate.MarkChangesAsCommitted();
        }
    }
}
