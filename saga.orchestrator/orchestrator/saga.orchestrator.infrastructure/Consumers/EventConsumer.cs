using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using saga.orchestrator.application.Commands;
using saga.orchestrator.application.Consumers;
using saga.orchestrator.domain.Common;
using saga.orchestrator.domain.Events;
using saga.orchestrator.infrastructure.Consumers.Converters;
using System.ComponentModel.Design;
using System.Text.Json;

namespace saga.orchestrator.infrastructure.Consumers
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly IConsumerEventHandler _eventHandler;
        private readonly IMediator _mediator;

        public EventConsumer(IOptions<ConsumerConfig> config, IConsumerEventHandler eventHandler, IMediator mediator)
        {
            _config = config.Value;
            _eventHandler = eventHandler;
            _mediator = mediator;
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
