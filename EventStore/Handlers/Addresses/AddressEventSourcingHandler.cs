using Application.Aggregates;
using Application.Interfaces.EventSourcing;
using Application.Interfaces.EventSourcing.Handlers;

namespace EventSourcing.Handlers.Addresses
{
    public class AddressEventSourcingHandler(IEventStoreService eventStoreService) : IEventSourcingHandler<AddressAggregate>
    {
        public async Task<AddressAggregate> ReadByAggregateIdAsync(int aggregateId)
        {
            var addressAggregate = new AddressAggregate();

            var events = await eventStoreService
                .ReadByAggregateIdAsync(aggregateId)
                .ConfigureAwait(false);

            if (events == null || !events.Any())
            {
                return addressAggregate;
            }

            addressAggregate.ReplayEvents(events);
            addressAggregate.Version = events.Max(@event => @event.Version);

            return addressAggregate;
        }

        public async Task SaveAsync(string topic, AddressAggregate addressAggregate)
        {
            await eventStoreService
                .SaveAsync<AddressAggregate>(topic, addressAggregate.Id, addressAggregate.GetUncommittedChanges(), addressAggregate.Version)
                .ConfigureAwait(false);

            addressAggregate.MarkChangesAsCommitted();
        }
    }
}
