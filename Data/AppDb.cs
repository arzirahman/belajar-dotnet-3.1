using Microsoft.EntityFrameworkCore;
using Coba_Net.Models;

namespace Coba_Net.Data
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
