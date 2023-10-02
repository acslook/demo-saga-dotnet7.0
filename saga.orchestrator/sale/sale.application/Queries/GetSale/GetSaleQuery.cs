using MediatR;
using Microsoft.EntityFrameworkCore;
using sale.application.Common;
using sale.domain.Entities;

namespace sale.application.Queries.GetSale
{
    public record GetSaleQuery : IRequest<Sale>
    {
        public long Id { get; init; }
    }

    public class GetSaleQueryHandler : IRequestHandler<GetSaleQuery, Sale>
    {
        private readonly IApplicationDbContext _context;

        public GetSaleQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Sale?> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            return await _context.Sales.FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken);    
        }
    }
}
