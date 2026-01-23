using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VehicleShowroom;
using VehicleShowroom.Models;


using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Models;
using Newtonsoft.Json;

namespace VehicleShowroom.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id, string type)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart")
                       ?? new List<CartItem>();

            var item = cart.FirstOrDefault(x => x.Id == id && x.Type == type);

            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                if (type == "Car")
                {
                    var car = _context.Cars.FirstOrDefault(c => c.Id == id);
                    if (car != null)
                    {
                        cart.Add(new CartItem
                        {
                            Id = car.Id,
                            Name = car.Name,
                            Type = "Car",
                            ImageUrl = car.ImageUrl,
                            Price = car.Price,
                            Quantity = 1
                        });
                    }
                }
                else if (type == "Bike")
                {
                    var bike = _context.Motorcycles.FirstOrDefault(b => b.Id == id);
                    if (bike != null)
                    {
                        cart.Add(new CartItem
                        {
                            Id = bike.Id,
                            Name = bike.Name,
                            Type = "Bike",
                            ImageUrl = bike.ImageUrl,
                            Price = bike.Price,
                            Quantity = 1
                        });
                    }
                }
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }



        public IActionResult Index()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            List<CartItem> cart = string.IsNullOrEmpty(cartJson)
                                  ? new List<CartItem>()
                                  : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);

            return View(cart);
        }

        public IActionResult Increase(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            var item = cart?.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                item.Quantity++;
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Decrease(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            var item = cart?.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                    cart.Remove(item);

                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {

            var item = _context.Carts.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Carts.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public IActionResult AddHelmet(int id)
        {
            var helmet = _context.Helmet.FirstOrDefault(h => h.Id == id);
            if (helmet == null)
                return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var item = cart.FirstOrDefault(x => x.Id == id && x.Type == "Helmet");

            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    Id = helmet.Id,
                    Name = helmet.Brand, // ممكن تضيف اسم اللون لو تحب
                    Type = "Helmet",
                    ImageUrl = helmet.ImageUrl,
                    Price = helmet.Price,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }

    }
}


    


   



