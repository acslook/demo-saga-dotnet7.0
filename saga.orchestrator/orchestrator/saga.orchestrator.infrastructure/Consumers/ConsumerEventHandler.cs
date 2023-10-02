using MediatR;
using saga.orchestrator.application.Commands;
using saga.orchestrator.application.Consumers;
using saga.orchestrator.domain.Events;

namespace saga.orchestrator.infrastructure.Consumers
{
    public class ConsumerEventHandler : IConsumerEventHandler
    {
        private readonly IMediator _mediator;

        public ConsumerEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task On(CreatedSaleEvent @event) => 
                await _mediator.Send(new CreatedSaleEventCommand(@event));
    }
}
