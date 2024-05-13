using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data.Entities;
using MovieManager.Data.Entities.Auth;

namespace MovieManager.Data.Context
{
    public class MovieManagerContext : IdentityDbContext<ApplicationUser>
    {
        public MovieManagerContext(DbContextOptions<MovieManagerContext> options) : base(options) { }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
