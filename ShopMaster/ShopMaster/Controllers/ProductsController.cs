using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopMaster.Data;
using ShopMaster.Models;

namespace ShopMaster.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(
                _context.Categories.Where(c => c.IsActive),
                "Id",
                "NameAr"
            );
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameAr,NameEn,DescriptionAr,DescriptionEn,Price,DiscountPrice,StockQuantity,SKU,ImageUrl,IsActive,IsFeatured,CategoryId")] Product product)
        {
            // إزالة الحقول اللي مش مطلوبة من الـ Validation
            ModelState.Remove("CreatedAt");
            ModelState.Remove("UpdatedAt");
            ModelState.Remove("Category");
            ModelState.Remove("OrderItems");

            if (ModelState.IsValid)
            {
                // تعيين التواريخ تلقائياً
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = null;

                _context.Add(product);
                await _context.SaveChangesAsync();

                // رسالة نجاح
                TempData["SuccessMessage"] = "تم إضافة المنتج بنجاح!";

                return RedirectToAction(nameof(Index));
            }

            // في حالة الخطأ، أعد عرض الفئات
            ViewData["CategoryId"] = new SelectList(
                _context.Categories.Where(c => c.IsActive),
                "Id",
                "NameAr",
                product.CategoryId
            );
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(
                _context.Categories,
                "Id",
                "NameAr",
                product.CategoryId
            );
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameAr,NameEn,DescriptionAr,DescriptionEn,Price,DiscountPrice,StockQuantity,SKU,ImageUrl,IsActive,IsFeatured,CategoryId,CreatedAt")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            ModelState.Remove("UpdatedAt");
            ModelState.Remove("Category");
            ModelState.Remove("OrderItems");

            if (ModelState.IsValid)
            {
                try
                {
                    product.UpdatedAt = DateTime.Now;
                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "تم تحديث المنتج بنجاح!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(
                _context.Categories,
                "Id",
                "NameAr",
                product.CategoryId
            );
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "تم حذف المنتج بنجاح!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}