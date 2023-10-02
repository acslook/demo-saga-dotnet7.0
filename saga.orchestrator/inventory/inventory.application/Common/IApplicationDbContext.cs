using inventory.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace inventory.application.Common
{
    public interface IApplicationDbContext
    {
        DbSet<Inventory> Inventories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
