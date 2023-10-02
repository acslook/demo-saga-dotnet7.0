using inventory.application.Common;
using inventory.domain.Entities;
using inventory.infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace inventory.infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        //private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Inventory> Inventories => Set<Inventory>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        //}

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var numberOfentriesInDatabase = await base.SaveChangesAsync(cancellationToken);

            await _mediator.DispatchDomainEvents(this);

            return numberOfentriesInDatabase;
        }
    }
}
