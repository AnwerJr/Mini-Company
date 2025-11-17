
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestRestAPI.Data;
using TestRestAPI.Model;

namespace TestRestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ربط DbContext بقاعدة البيانات من appsettings.json
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer(); // مهم للـ Swagger
            builder.Services.AddSwaggerGen();           // مهم للـ Swagger

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();      // تفعيل Swagger
            app.UseSwaggerUI();    // واجهة المستخدم الخاصة بـ Swagger

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
