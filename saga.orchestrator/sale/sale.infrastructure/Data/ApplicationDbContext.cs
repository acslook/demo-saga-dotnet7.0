using MediatR;
using System.Reflection;
using sale.application.Common;
using Microsoft.EntityFrameworkCore;
using sale.domain.Entities;
using sale.infrastructure.Common;

namespace sale.infrastructure.Data
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

        public DbSet<Sale> Sales => Set<Sale>();

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
