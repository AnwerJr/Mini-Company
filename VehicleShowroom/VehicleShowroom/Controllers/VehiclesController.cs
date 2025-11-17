using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Models;

namespace VehicleShowroom.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Cars()
        {
            // جلب جميع السيارات من قاعدة البيانات
            var cars = await _context.Cars.ToListAsync();

            // تمرير القائمة للـ View
            return View(cars);

        }

        public async Task <IActionResult> CarsByBrand(string brand)
        {
            var cars = _context.Cars
              .Where(c => c.Brand == brand)
              .ToList();

            return View(cars);
        }

        public async Task<IActionResult> BikesByBrand(string brand)
        {
            var bikes = _context.Motorcycles
                        .Where(m => m.Brand == brand)
                        .ToList();

            return View("BikesByBrand", bikes);
        }





        public async Task<IActionResult> Bikes()
        {
            var moto = await _context.Motorcycles.ToListAsync();
            return View(moto);
        }

        public async Task<IActionResult> CarDetails(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);

            if (car == null) { 
                return NotFound();
            }
            else
            {
                return View(car);
            }
        }

        public async Task<IActionResult> BikeDetails(int id)
        {
            var bike = _context.Motorcycles.FirstOrDefault(b => b.Id == id);
            if (bike == null)
            {
                return NotFound();
            }
            else
            {
                return View(bike);
            }
        }
        
    }
}
