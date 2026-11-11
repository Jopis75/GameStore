using GameStore.Application.Events;

namespace GameStore.Application.Interfaces.EventSourcing.Producers
{
    public interface IEventProducer
    {
        Task ProduceAsync<TEvent>(string topic, TEvent @event) where TEvent : EventBase;
    }
}
