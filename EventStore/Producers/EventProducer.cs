using Application.Events;
using Application.Interfaces.EventSourcing.Producers;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EventSourcing.Producers
{
    public class EventProducer(IOptions<ProducerConfig> producerConfig) : IEventProducer
    {
        public async Task ProduceAsync<TEvent>(string topic, TEvent @event)
            where TEvent : EventBase
        {
            using var producerBuilder = new ProducerBuilder<string, string>(producerConfig.Value)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .Build();

            var message = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(@event, @event.GetType())
            };

            var deliveryResult = await producerBuilder.ProduceAsync(topic, message);

            if (deliveryResult.Status != PersistenceStatus.Persisted)
            {
                throw new Exception($"Failed to produce event to topic {topic}. Status: {deliveryResult.Status}. Message: {deliveryResult.Message}");
            }
        }
    }
}
