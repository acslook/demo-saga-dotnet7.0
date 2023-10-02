using inventory.application.Producers;
using inventory.domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace inventory.application.EventHandlers
{
    public class InventoryPreparedEventHandler : INotificationHandler<InventoryPreparedEvent>
    {
        private const string TP_SAGA_ORCHESTRATOR = "tp-saga-orchestrator";

        private readonly ILogger<InventoryPreparedEventHandler> _logger;
        private readonly IEventProducer _eventProducer;

        public InventoryPreparedEventHandler(
            ILogger<InventoryPreparedEventHandler> logger,
            IEventProducer eventProducer)
        {
            _logger = logger;
            _eventProducer = eventProducer;
        }

        public async Task Handle(InventoryPreparedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

            await _eventProducer.ProduceAsync(TP_SAGA_ORCHESTRATOR, notification);
        }
    }
}
