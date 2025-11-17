using Microsoft.EntityFrameworkCore;

namespace VehicleShowroom.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Moto> Motorcycles { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<Helmets> Helmet { get; set; }


    }
}
