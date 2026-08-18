namespace Application.Interfaces.EventSourcing.Consumers
{
    public interface IAddressEventConsumer
    {
        void Consume(string topic, CancellationToken cancellationToken);
    }
}
