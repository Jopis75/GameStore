using Application.Events;
using Application.Events.Addresses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventSourcing.Converters
{
    public class EventJsonConverter : JsonConverter<EventBase>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(EventBase));
        }

        public override EventBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var document))
            {
                throw new JsonException($"Failed to parse JSON document: {nameof (JsonDocument)}");
            }

            if (!document.RootElement.TryGetProperty("Type", out var type))
            {
                throw new JsonException("Missing 'Type' property in JSON.");
            }

            var json = document.RootElement.GetRawText();
            var typeName = type.GetString();

            return typeName switch
            {
                "AddressCreatedEvent" => JsonSerializer.Deserialize<AddressCreatedEvent>(json, options),
                "AddressDeletedEvent" => JsonSerializer.Deserialize<AddressDeletedEvent>(json, options),
                "AddressUpdatedEvent" => JsonSerializer.Deserialize<AddressUpdatedEvent>(json, options),
                _ => throw new JsonException($"Unknown event type: {typeName}")
            };
        }

        public override void Write(Utf8JsonWriter writer, EventBase value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
