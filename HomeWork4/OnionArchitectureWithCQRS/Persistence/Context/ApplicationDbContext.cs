using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
