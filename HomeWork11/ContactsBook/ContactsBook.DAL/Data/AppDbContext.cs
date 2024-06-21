using ContactsBook.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsBook.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ContactItem> ContactsList { get; set; }
    }
}
