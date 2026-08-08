namespace Application.Interfaces.EventSourcing.Consumers
{
    public interface IEventConsumer
    {
        void Consume(string topic, CancellationToken cancellationToken);
    }
}
