using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestRestAPI.Model;

namespace TestRestAPI.Data
{
    public class AppDbContext:IdentityDbContext<User>
    {
        // 🔹 الـ Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // 🔹 تعريف الـ DbSet (الجداول)

        public DbSet<Category> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<User> User { get; set; }

    }
}
