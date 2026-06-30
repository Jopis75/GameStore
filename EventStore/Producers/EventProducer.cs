using Application.Events;
using Application.Interfaces.EventSourcing.Producers;
using EventSourcing.Configurations;
using Microsoft.Extensions.Options;

namespace EventSourcing.Producers
{
    public class EventProducer(IOptions<ProducerConfig> producerConfig) : IEventProducer
    {
        public Task ProduceAsync<TEvent>(string topic, TEvent @event) where TEvent : EventBase
        {
            throw new NotImplementedException();
        }
    }
}
