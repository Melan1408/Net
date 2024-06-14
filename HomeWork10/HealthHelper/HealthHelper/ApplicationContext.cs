using HealthHelper.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthHelper
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<FamilyMember> FamilyMembers { get; set; } = null!;

        public DbSet<Tablet> Tablets { get; set; } = null!;

        public DbSet<CourseOfTablet> CourseOfTablets { get; set; } = null!;
    }
}
