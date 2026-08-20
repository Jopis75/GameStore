using Application.Events;
using Application.Interfaces.EventSourcing.Consumers;
using Application.Interfaces.EventSourcing.Handlers;
using Confluent.Kafka;
using EventSourcing.Converters;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EventSourcing.Consumers
{
    public class AddressEventConsumer(IAddressEventHandler addressEventHandler, IOptions<ConsumerConfig> consumerConfig) : IAddressEventConsumer
    {
        public void Consume(string topic, CancellationToken cancellationToken)
        {
            using var consumerBuilder = new ConsumerBuilder<string, string>(consumerConfig.Value)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build();

            consumerBuilder.Subscribe(topic);

            while (true)
            {
                var consumeResult = consumerBuilder.Consume();

                if (consumeResult.Message != null)
                {
                    var jsonSerializerOptions = new JsonSerializerOptions
                    {
                        Converters =
                        {
                            new EventJsonConverter()
                        }
                    };

                    var @event = JsonSerializer.Deserialize<EventBase>(consumeResult.Message.Value, jsonSerializerOptions);

                    if (@event == null)
                    {
                        throw new ArgumentNullException(nameof(@event), "Could not deserialize event.");
                    }

                    var eventHandlerMethod = addressEventHandler.GetType().GetMethod("Handle", [@event!.GetType(), cancellationToken.GetType()]);

                    if (eventHandlerMethod == null)
                    {
                        throw new ArgumentNullException(nameof(eventHandlerMethod), "Could not find event handler method.");
                    }

                    eventHandlerMethod.Invoke(addressEventHandler, [@event, cancellationToken]);

                    consumerBuilder.Commit(consumeResult);
                }
            }
        }
    }
}
