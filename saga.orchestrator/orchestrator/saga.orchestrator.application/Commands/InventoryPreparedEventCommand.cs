using MediatR;
using Microsoft.Extensions.Logging;
using saga.orchestrator.application.Producers;
using saga.orchestrator.domain.Events;

namespace saga.orchestrator.application.Commands
{
    public record InventoryPreparedEventCommand(InventoryPreparedEvent InventoryPreparedEvent) : IRequest;

    public class InventoryPreparedEventCommandHandler : IRequestHandler<InventoryPreparedEventCommand>
    {
        private const string TP_SAGA_INVENTORY = "tp-saga-payment";
        private readonly ILogger<CreatedSaleEventCommandHandler> _logger;
        private readonly IEventProducer _eventProducer;

        public InventoryPreparedEventCommandHandler(
            ILogger<CreatedSaleEventCommandHandler> logger,
            IEventProducer eventProducer)
        {
            _logger = logger;
            _eventProducer = eventProducer;
        }

        public async Task Handle(InventoryPreparedEventCommand request, CancellationToken cancellationToken)
        {
            await _eventProducer.ProduceAsync(TP_SAGA_INVENTORY, request.InventoryPreparedEvent);
        }
    }
}
