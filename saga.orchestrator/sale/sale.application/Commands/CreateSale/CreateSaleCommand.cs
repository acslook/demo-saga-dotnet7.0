using MediatR;
using sale.application.Common;
using sale.domain.Entities;
using sale.domain.Enums;
using sale.domain.Events;

namespace sale.application.Commands.CreateSale
{
    public record CreateSaleCommand : IRequest<long>
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public long UserId { get; set; }

        public decimal Value { get; set; }

        public SaleStatus Status { get; set; }

        public int Quantity { get; set; }
    }

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, long>
    {        
        private readonly IApplicationDbContext _context;        

        public CreateSaleCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var entity = new Sale
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Status = SaleStatus.PENDING,
                UserId = request.UserId,
                Value = request.Value
            };

            entity.AddDomainEvent(new CreatedSaleEvent(entity));

            _context.Sales.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
