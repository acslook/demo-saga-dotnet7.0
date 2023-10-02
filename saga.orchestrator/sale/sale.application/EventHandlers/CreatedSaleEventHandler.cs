using MediatR;
using Microsoft.Extensions.Logging;
using sale.application.Producers;
using sale.domain.Events;

namespace sale.application.EventHandlers
{
    public class CreatedSaleEventHandler : INotificationHandler<CreatedSaleEvent>
    {
        private const string TP_SAGA_ORCHESTRATOR = "tp-saga-orchestrator";

        private readonly ILogger<CreatedSaleEventHandler> _logger;
        private readonly IEventProducer _eventProducer;

        public CreatedSaleEventHandler(
            ILogger<CreatedSaleEventHandler> logger,
            IEventProducer eventProducer)
        {
            _logger = logger;
            _eventProducer = eventProducer;
        }

        public async Task Handle(CreatedSaleEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

            await _eventProducer.ProduceAsync(TP_SAGA_ORCHESTRATOR, notification);
        }
    }
}
