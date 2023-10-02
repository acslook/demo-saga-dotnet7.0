﻿using System.Text.Json.Serialization;
using System.Text.Json;
using saga.orchestrator.domain.Common;
using saga.orchestrator.domain.Events;

namespace saga.orchestrator.infrastructure.Consumers.Converters
{
    public class EventJsonConverter : JsonConverter<BaseEvent>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
        }

        public override BaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var doc))
            {
                throw new JsonException($"Failed to parse {nameof(JsonDocument)}");
            }

            if (!doc.RootElement.TryGetProperty("EventName", out var type))
            {
                throw new JsonException("Could not detect the EventName discriminator property!");
            }

            var typeDiscriminator = type.GetString();
            var json = doc.RootElement.GetRawText();

            return typeDiscriminator switch
            {
                nameof(CreatedSaleEvent) => JsonSerializer.Deserialize<CreatedSaleEvent>(json, options),
                _ => throw new JsonException($"{typeDiscriminator} is not support yet!")
            };
        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
