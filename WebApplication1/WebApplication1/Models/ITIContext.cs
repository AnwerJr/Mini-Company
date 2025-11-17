using Microsoft.EntityFrameworkCore;
using WebApplication1.csproj.Models;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class ITIContext : DbContext
    {
        public ITIContext(DbContextOptions<ITIContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}



//using Microsoft.EntityFrameworkCore;
//using WebApplication1.csproj.Models;

//namespace WebApplication1.Models
//{
//    public class ITIContext : DbContext
//    {
//        public DbSet<Employee> Employees { get; set; }
//        public DbSet<Department> Departments { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Mini_Company;Integrated Security=True;Encrypt=False");
//        }
//    }
//}
