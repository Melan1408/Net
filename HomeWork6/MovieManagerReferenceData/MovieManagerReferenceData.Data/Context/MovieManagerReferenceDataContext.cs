using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Data.Entites;

namespace MovieManagerReferenceData.Data.Context
{
    public class MovieManagerReferenceDataContext : DbContext
    {
        public MovieManagerReferenceDataContext(DbContextOptions<MovieManagerReferenceDataContext> options) : base(options) { }

        public virtual DbSet<Actor> Actors { get; set; }

        public virtual DbSet<Director> Directors { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

    }
}
