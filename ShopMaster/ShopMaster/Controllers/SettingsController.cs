using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMaster.Data;
using ShopMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMaster.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings
        public async Task<IActionResult> Index()
        {
            var settings = await _context.SystemSettings
                .OrderBy(s => s.Key)
                .ToListAsync();

            return View(settings);
        }

        // POST: Settings/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Dictionary<string, string> settings)
        {
            try
            {
                foreach (var setting in settings)
                {
                    var dbSetting = await _context.SystemSettings
                        .FirstOrDefaultAsync(s => s.Key == setting.Key);

                    if (dbSetting != null)
                    {
                        dbSetting.Value = setting.Value;
                        dbSetting.UpdatedAt = DateTime.Now;
                    }
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "تم حفظ الإعدادات بنجاح!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء حفظ الإعدادات: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Settings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,Value,Description")] SystemSetting setting)
        {
            if (ModelState.IsValid)
            {
                if (await _context.SystemSettings.AnyAsync(s => s.Key == setting.Key))
                {
                    ModelState.AddModelError("Key", "هذا المفتاح موجود بالفعل");
                    return View(setting);
                }

                setting.UpdatedAt = DateTime.Now;
                _context.Add(setting);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "تم إضافة الإعداد بنجاح!";
                return RedirectToAction(nameof(Index));
            }

            return View(setting);
        }

        // GET: Settings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.SystemSettings.FirstOrDefaultAsync(m => m.Id == id);

            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var setting = await _context.SystemSettings.FindAsync(id);

            if (setting != null)
            {
                _context.SystemSettings.Remove(setting);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "تم حذف الإعداد بنجاح!";
            }

            return RedirectToAction(nameof(Index));
        }

        // Helper method to get setting value
        public static async Task<string> GetSettingValue(ApplicationDbContext context, string key, string defaultValue = "")
        {
            var setting = await context.SystemSettings
                .FirstOrDefaultAsync(s => s.Key == key);

            return setting?.Value ?? defaultValue;
        }
    }
}