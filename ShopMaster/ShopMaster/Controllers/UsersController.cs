using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMaster.Data;
using ShopMaster.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMaster.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //var users = await _context.Users
            //    .OrderByDescending(u => u.CreatedAt)
            //    .ToListAsync();
            //return View(users);
            var users = _context.Users
                        .Where(u => u.IsActive)   // مهم
                        .ToList();

            return View(users);
        }

     // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("FullName,Email,Username,Password,Phone,Role,IsActive")] User user)
        {
            ModelState.Remove("Avatar");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("LastLogin");

            if (ModelState.IsValid)
            {
                // التحقق من عدم تكرار البريد أو اسم المستخدم
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "البريد الإلكتروني مستخدم بالفعل");
                    return View(user);
                }

                if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "اسم المستخدم مستخدم بالفعل");
                    return View(user);
                }

                user.CreatedAt = DateTime.Now;
                // TODO: تشفير كلمة المرور
                // user.Password = HashPassword(user.Password);

                _context.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "تم إضافة المستخدم بنجاح!";
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Username,Phone,Role,IsActive,CreatedAt")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Password");
            ModelState.Remove("Avatar");
            ModelState.Remove("LastLogin");

            if (ModelState.IsValid)
            {
                try
                {
                    // الحصول على المستخدم الحالي للحفاظ على كلمة المرور
                    var existingUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
                    user.Password = existingUser.Password;
                    user.Avatar = existingUser.Avatar;
                    user.LastLogin = existingUser.LastLogin;

                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "تم تحديث بيانات المستخدم بنجاح!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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

            return View(user);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            // ❌ منع حذف Admin
            if (user.Role == UserRole.Admin)
            {
                TempData["Error"] = "لا يمكن حذف حساب Admin";
                return RedirectToAction("Index");
            }

            // ✅ Soft Delete
            user.IsActive = false;

            _context.SaveChanges();

            TempData["Success"] = "تم تعطيل المستخدم بنجاح";
            return RedirectToAction("Index");
        }


        // GET: Users/ChangePassword/5
        public async Task<IActionResult> ChangePassword(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/ChangePassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(int id, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
            {
                TempData["ErrorMessage"] = "كلمة المرور يجب أن تكون 6 أحرف على الأقل!";
                return RedirectToAction(nameof(ChangePassword), new { id });
            }

            if (newPassword != confirmPassword)
            {
                TempData["ErrorMessage"] = "كلمة المرور غير متطابقة!";
                return RedirectToAction(nameof(ChangePassword), new { id });
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // TODO: تشفير كلمة المرور
            user.Password = newPassword;

            _context.Update(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم تغيير كلمة المرور بنجاح!";
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
