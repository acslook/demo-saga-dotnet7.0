using Microsoft.EntityFrameworkCore;
using sale.domain.Entities;

namespace sale.application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<Sale> Sales { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
