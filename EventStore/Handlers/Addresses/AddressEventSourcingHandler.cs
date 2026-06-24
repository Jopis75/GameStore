using Application.Aggregates.Addresses;
using Application.Interfaces.EventStore;

namespace EventStore.Handlers.Addresses
{
    public class AddressEventSourcingHandler(IEventStoreService eventStoreService) : IEventSourcingHandler<AddressAggregate>
    {
        public Task<AddressAggregate> GetByIdAsync(Guid aggregateId)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(AddressAggregate aggregate)
        {
            throw new NotImplementedException();
        }
    }
}
