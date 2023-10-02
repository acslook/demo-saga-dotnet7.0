using MediatR;
using Microsoft.Extensions.Logging;
using saga.orchestrator.application.Producers;
using saga.orchestrator.domain.Events;

namespace saga.orchestrator.application.Commands
{
    public record CreatedSaleEventCommand(CreatedSaleEvent CreatedSaleEvent) : IRequest;

    public class CreatedSaleEventCommandHandler : IRequestHandler<CreatedSaleEventCommand>
    {
        private const string TP_SAGA_INVENTORY = "tp-saga-inventory";
        private readonly ILogger<CreatedSaleEventCommandHandler> _logger;
        private readonly IEventProducer _eventProducer;

        public CreatedSaleEventCommandHandler(
            ILogger<CreatedSaleEventCommandHandler> logger,
            IEventProducer eventProducer)
        {
            _logger = logger;
            _eventProducer = eventProducer;
        }

        public async Task Handle(CreatedSaleEventCommand request, CancellationToken cancellationToken)
        {
            await _eventProducer.ProduceAsync(TP_SAGA_INVENTORY, request.CreatedSaleEvent);
        }
    }
}
