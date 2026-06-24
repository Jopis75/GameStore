using Application.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models.Event
{
    public class EventModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        public DateTime TimeStamp { get; set; }

        public Guid AggregateId { get; set; }

        public string AggregateType { get; set; } = String.Empty;

        public int AggregateVersion { get; set; }

        public string EventType { get; set; } = String.Empty;

        public EventBase Event { get; set; } = default!;
    }
}
