using GameStore.Application.Interfaces.EventSourcing;
using GameStore.Application.Models.Event;
using GameStore.EventSourcing.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GameStore.EventSourcing.Repositories
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
        public async Task<IEnumerable<EventModel>> ReadByAggregateIdAsync(int aggregateId)
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
