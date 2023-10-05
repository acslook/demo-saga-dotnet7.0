using inventory.application.Commands.DebitInventory;
using inventory.application.Consumers;
using inventory.domain.Events;
using MediatR;

namespace inventory.infrastructure.Consumers
{
    public class ConsumerEventHandler : IConsumerEventHandler
    {
        private readonly IMediator _mediator;

        public ConsumerEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task On(CreatedSaleEvent @event) => await _mediator.Send(new DebitInventoryCommand(@event));
    }
}
