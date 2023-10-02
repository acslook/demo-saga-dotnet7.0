using Confluent.Kafka;
using inventory.application.Consumers;
using inventory.domain.Common;
using inventory.infrastructure.Converters;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace inventory.infrastructure.Consumers
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly IConsumerEventHandler _eventHandler;

        public EventConsumer(IOptions<ConsumerConfig> config, IConsumerEventHandler eventHandler)
        {
            _config = config.Value;
            _eventHandler = eventHandler;
        }
        public void Consume(string topic)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config)
                        .SetKeyDeserializer(Deserializers.Utf8)
                        .SetValueDeserializer(Deserializers.Utf8)
                        .Build();

            consumer.Subscribe(topic);

            while (true)
            {
                var consumerResult = consumer.Consume();

                if (consumerResult?.Message == null) continue;

                var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
                var @event = JsonSerializer.Deserialize<BaseEvent>(consumerResult.Message.Value, options);
                var handlerMethod = _eventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });

                if (handlerMethod == null)
                {
                    throw new ArgumentNullException(nameof(handlerMethod), "Could not find event handler method!");
                }

                handlerMethod.Invoke(_eventHandler, new object[] { @event });
                consumer.Commit(consumerResult);
            }
        }
    }
}
