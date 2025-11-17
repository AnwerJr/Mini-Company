using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    internal class ApplicationDbContext : DbContext
    {
        // كود خاص بتكوين قاعدة البيانات والاتصال بها 
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Database=EFCoreDB;Trusted_Connection=True;");
        }
        public DbSet<Blog> Blogs { get; set; }
    }
}
