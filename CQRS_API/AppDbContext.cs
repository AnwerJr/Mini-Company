using Microsoft.EntityFrameworkCore;

namespace CQRS_API
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Define DbSet<T> properties for your entities here
        // Example:
        // public DbSet<MyEntity> MyEntities { get; set; }
    }
}