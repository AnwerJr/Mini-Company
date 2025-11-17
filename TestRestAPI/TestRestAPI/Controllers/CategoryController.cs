using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRestAPI.Data;
using TestRestAPI.Model;

namespace TestRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/category ( GET ALL CATEGORY)
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }


        // GET : ONE ITEM OF CATEGORIES
        [HttpGet("{id}")]

        public async Task<IActionResult> GetCategoryById(int id)
        {
            var categoury = await _context.Categories.SingleOrDefaultAsync(x=>x.CategoryId == id);
            if(categoury == null) {

                return NotFound($"Category with Id = {id} not found.");
            }
            else
            {
                return Ok(categoury);
            }

        }



        // Post :لو عاوز اضيف حاجه للdatabase
        [HttpPost]
        public async Task<IActionResult> AddCAtegory(String CategoryName )
        {
            Category category = new () { Name = CategoryName };
            await _context.Categories.AddAsync(category);
            _context.SaveChanges();
            return Ok(category);
          
        }

        // Put في حالة لو عاوز اعمل update لحاجه

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            var c = await _context.Categories.SingleOrDefaultAsync(x => x.CategoryId == category.CategoryId);

            if (c == null)
            {
                return NotFound($"Category Id {category.CategoryId} Not Exists");
            }

            // ✅ هنا نحدث القيم فعليًا
            c.Name = category.Name;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Category updated successfully",
                data = c
            });
        }



        // **  زي ال PUT ولكن تختلف في حجات بسيطه


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategoryPatch([FromBody]JsonPatchDocument <Category> category,[FromRoute] int id)
        {
            var c = await _context.Categories.SingleOrDefaultAsync(x => x.CategoryId == id);

            if (c == null)
            {
                return NotFound($"Category Id {id} Not Exists");
            }


            category.ApplyTo(c);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Category updated successfully",
                data = c
            });
        }



        // Delete

        [HttpDelete("{id}") ]
        public async Task<IActionResult> DeleteCAtegory(int id)
        {
            // 🔹 نحاول نجيب الكاتيجوري اللي عايزين نمسحه
            var category = await _context.Categories.SingleOrDefaultAsync(x=> x.CategoryId ==id);

            if (category == null)
            {
                return NotFound($" Category with id: {id} Not Found");
            }
            else {
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


