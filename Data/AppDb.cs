using Microsoft.EntityFrameworkCore;
using Coba_Net.Models;

namespace Coba_Net.Data
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
    }
}
