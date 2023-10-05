using inventory.application.Common;
using inventory.domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace inventory.application.Commands.DebitInventory
{
    public record DebitInventoryCommand(CreatedSaleEvent CreatedSaleEvent) : IRequest
    {
    }

    public class DebitInventoryCommandHandler : IRequestHandler<DebitInventoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DebitInventoryCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DebitInventoryCommand request, CancellationToken cancellationToken)
        {

            var inventory = await _context.Inventories.FirstOrDefaultAsync(inventory => inventory.ProductId == request.CreatedSaleEvent.Sale.ProductId);

            if (inventory == null)
                throw new Exception($"Estoque não encontrado para o produto ID: {request.CreatedSaleEvent.Sale.ProductId}");

            if (inventory.Quantity < request.CreatedSaleEvent.Sale.Quantity)
            {
                throw new ApplicationException("Estoque insuficiente.");
            }

            inventory.DebitQuantity(request.CreatedSaleEvent.Sale.Quantity);

            inventory.AddDomainEvent(new InventoryPreparedEvent(request.CreatedSaleEvent.Sale));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
