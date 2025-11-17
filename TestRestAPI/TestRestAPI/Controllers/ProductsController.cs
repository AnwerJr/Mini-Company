using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRestAPI.Data;
using TestRestAPI.Model;
using TestRestAPI.Models;

namespace TestRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }


        // Get All Products

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products
            .Include(p => p.Category) // دي بتربط البيانات من الجدول التاني
            .ToListAsync();
            return Ok(products);
        }


        // Get one Item OF Products
        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductById(int id)
        {
            var Products = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (Products == null)
            {
                return NotFound($"Product Id {id} not found");
            }
            else
            {
                return Ok(Products);
            }

        }


        [HttpGet("ProductWithCategory/{IdCategory}")]

        public async Task<IActionResult> GetProductWithCategory(int IdCategory)
        {
            var Products = await _context.Products.Where(p => p.CategoryId == IdCategory)
            .Include(p => p.Category) // عشان يظهر بيانات الكاتيجوري كمان
            .ToListAsync();

            if (Products == null)
            {
                return NotFound($"Cateegory Id: {IdCategory} has no Product");
            }
            else
            {
                return Ok(Products);
            }

        }





        // Fix for CS0029: Convert byte[] to Base64 string before assigning to ImageUrl

        [HttpPost]
        public async Task<IActionResult> AddProduct(MdlProducts mdlProducts)
        {
            using var stream = new MemoryStream();
            await mdlProducts.ImageUrl.CopyToAsync(stream);

            var product = new Products
            {
                Name = mdlProducts.Name,
                Price = mdlProducts.Price,
                CategoryId = mdlProducts.CategoryId,
                ImageUrl = Convert.ToBase64String(stream.ToArray()) // Convert byte[] to string
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromForm] Products updatedProduct)
        {
            // 🔹 نتحقق من إن الـ id في الرابط مطابق للـ id اللي داخل الـ body
            if (id != updatedProduct.Id)
            {
                return BadRequest($"Product ID:{id}  mismatch");
            }

            // 🔹 نجيب المنتج القديم من قاعدة البيانات
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with Id {id} not found");
            }

            // 🔹 نحدّث القيم الجديدة
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.CategoryId = updatedProduct.CategoryId;

            // 🔹 نحدث التغييرات في قاعدة البيانات
            await _context.SaveChangesAsync();

            // 🔹 نرجع المنتج بعد التعديل
            return Ok(new
            {
                message = "Product updated successfully",
                product = existingProduct
            });
        }






        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct
            (int id)
        {
            // 🔹 نحاول نجيب الكاتيجوري اللي عايزين نمسحه
            var category = await _context.Categories.SingleOrDefaultAsync(x => x.CategoryId == id);

            if (category == null)
            {
                return NotFound($" Category with id: {id} Not Found");
            }
            else
            {
                // 🔹 نحذف الكاتيجوري من الـ DbSet
                _context.Categories.Remove(category);

                // 🔹 نحفظ التغييرات في قاعدة البيانات
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Category deleted successfully",
                    deletedCategory = category
                });
            }

        }
    }
}
