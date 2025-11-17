using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Models;

namespace VehicleShowroom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllersWithViews();

            builder.Services.AddSession();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // مهم عشان الصور و CSS تشتغل

            app.UseRouting();

            app.UseSession(); // لازم قبل Authorization

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!context.Cars.Any())
                {
                    context.Cars.AddRange(
                        new Car { Name = "Civic", Model = "2024", Brand = "Honda", Price = 500000, FuelType = "Petrol", NumberOfDoors = 4, ImageUrl = "/images/car1.jpg" },
                        new Car { Name = "Corolla", Model = "2023", Brand = "Toyota", Price = 480000, FuelType = "Petrol", NumberOfDoors = 4, ImageUrl = "/images/car2.jpg" }
                    );
                    context.SaveChanges();
                }
            }

        }
    }
}
