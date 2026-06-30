using Application.Interfaces.EventSourcing;
using Application.Models.Event;
using EventSourcing.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EventSourcing.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<EventModel> _mongoCollection;

        public EventStoreRepository(IOptions<MongoDbConfig> mongoDbConfig)
        {
            var mongoClient = new MongoClient(mongoDbConfig.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbConfig.Value.Database);
            _mongoCollection = mongoDatabase.GetCollection<EventModel>(mongoDbConfig.Value.Collection);
        }
        public async Task<IEnumerable<EventModel>> ReadByAggregateIdAsync(Guid aggregateId)
        {
            var eventModels = await _mongoCollection
                .Find(eventModel => eventModel.AggregateId == aggregateId)
                .ToListAsync()
                .ConfigureAwait(false);

            if (eventModels is null)
            {
                return [];
            }

            return eventModels;
        }

        public async Task SaveAsync(EventModel eventModel)
        {
            await _mongoCollection
                .InsertOneAsync(eventModel)
                .ConfigureAwait(false);
        }
    }
}
