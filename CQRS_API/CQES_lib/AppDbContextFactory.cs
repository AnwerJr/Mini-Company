using CQES_lib.Data;
using CQRS_lib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_lib
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // 🔹 حط هنا نفس connection string اللي في appsettings.json بتاع المشروع الرئيسي
            optionsBuilder.UseSqlServer("Server=.;Database=CQRS_DB;Trusted_Connection=True;Encrypt=False;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
