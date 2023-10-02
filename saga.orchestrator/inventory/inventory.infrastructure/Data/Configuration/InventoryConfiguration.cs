using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using inventory.domain.Entities;

namespace inventory.infrastructure.Data.Configuration
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.Property(t => t.ProductId)
                .IsRequired();

            builder.Property(t => t.Quantity)
                .IsRequired();
        }
    }
}
