using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Movie> Movies { get; set; }

        DbSet<Session> Sessions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
