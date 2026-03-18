using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopMaster.Data;
using ShopMaster.Models;
using ShopMaster.ViewModels;

namespace ShopMaster.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        // دالة Index واحدة فقط مع باراميترات اختيارية
        public async Task<IActionResult> Index(
            int? status,
            int? paymentStatus,
            string customerName = "",
            bool isAjax = false)
        {
            IQueryable<Order> query = _context.Orders
                .Include(o => o.Customer)
                .AsQueryable();

            // تطبيق الفلاتر إذا وجدت
            if (status.HasValue)
            {
                query = query.Where(o => (int)o.Status == status.Value);
            }

            if (paymentStatus.HasValue)
            {
                query = query.Where(o => (int)o.PaymentStatus == paymentStatus.Value);
            }

            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(o =>
                    o.Customer.FullName.Contains(customerName) ||
                    o.Customer.Email.Contains(customerName) ||
                    o.Customer.Phone.Contains(customerName));
            }

            var orders = await query
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            // التحقق إذا كان طلب AJAX
            bool isAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjaxRequest)
            {
                return PartialView("_OrdersList", orders);
            }

            return View(orders);
        }

        // GET: Orders/FilterOrders
        // دالة منفصلة للتصفية عبر AJAX
        [HttpGet]
        public async Task<IActionResult> FilterOrders(
            int? status,
            int? paymentStatus,
            string customerName = "")
        {
            IQueryable<Order> query = _context.Orders
                .Include(o => o.Customer)
                .AsQueryable();

            // تصفية بحالة الطلب
            if (status.HasValue)
            {
                query = query.Where(o => (int)o.Status == status.Value);
            }

            // تصفية بحالة الدفع
            if (paymentStatus.HasValue)
            {
                query = query.Where(o => (int)o.PaymentStatus == paymentStatus.Value);
            }

            // تصفية باسم العميل
            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(o =>
                    o.Customer.FullName.Contains(customerName) ||
                    o.Customer.Email.Contains(customerName) ||
                    o.Customer.Phone.Contains(customerName));
            }

            // ترتيب حسب التاريخ (الأحدث أولاً)
            query = query.OrderByDescending(o => o.OrderDate);

            var orders = await query.ToListAsync();

            // إرجاع Partial View
            return PartialView("_OrdersList", orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Shipment)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(
                _context.Customers.Where(c => c.IsActive),
                "Id",
                "FullName"
            );

            ViewData["Products"] = _context.Products
                .Where(p => p.IsActive && p.StockQuantity > 0)
                .Include(p => p.Category)
                .ToList();

            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateViewModel model)
        {
            ModelState.Remove("Order.Customer");
            ModelState.Remove("Order.OrderItems");
            ModelState.Remove("Order.Shipment");

            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    OrderNumber = GenerateOrderNumber(),
                    CustomerId = model.CustomerId,
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Pending,
                    PaymentStatus = PaymentStatus.Pending,
                    ShippingCost = model.ShippingCost,
                    DiscountAmount = model.DiscountAmount,
                    Notes = model.Notes
                };

                decimal totalAmount = 0;

                // إضافة عناصر الطلب
                foreach (var item in model.Items.Where(i => i.ProductId > 0 && i.Quantity > 0))
                {
                    var product = await _context.Products.FindAsync(item.ProductId);

                    if (product != null && product.StockQuantity >= item.Quantity)
                    {
                        var unitPrice = product.DiscountPrice ?? product.Price;
                        var itemTotal = unitPrice * item.Quantity;

                        var orderItem = new OrderItem
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = unitPrice,
                            Discount = 0,
                            TotalPrice = itemTotal
                        };

                        order.OrderItems.Add(orderItem);
                        totalAmount += itemTotal;

                        // تقليل المخزون
                        product.StockQuantity -= item.Quantity;
                    }
                }

                if (order.OrderItems.Count == 0)
                {
                    ModelState.AddModelError("", "يجب إضافة منتج واحد على الأقل للطلب");
                    ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.IsActive), "Id", "FullName", model.CustomerId);
                    ViewData["Products"] = _context.Products.Where(p => p.IsActive && p.StockQuantity > 0).Include(p => p.Category).ToList();
                    return View(model);
                }

                order.TotalAmount = totalAmount + order.ShippingCost - order.DiscountAmount;

                _context.Add(order);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"تم إنشاء الطلب {order.OrderNumber} بنجاح!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.IsActive), "Id", "FullName", model.CustomerId);
            ViewData["Products"] = _context.Products.Where(p => p.IsActive && p.StockQuantity > 0).Include(p => p.Category).ToList();
            return View(model);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", order.CustomerId);

            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,OrderNumber,OrderDate,TotalAmount,ShippingCost,DiscountAmount,Status,PaymentStatus,Notes,CustomerId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            // CRITICAL: إزالة الـ validation للـ navigation properties
            ModelState.Remove("Customer");
            ModelState.Remove("OrderItems");
            ModelState.Remove("Shipment");

            // Debug: عرض الأخطاء
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"تم تحديث الطلب {order.OrderNumber} بنجاح!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"خطأ في الحفظ: {ex.Message}");
                }
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order != null)
            {
                // إرجاع المخزون
                foreach (var item in order.OrderItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product != null)
                    {
                        product.StockQuantity += item.Quantity;
                    }
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "تم حذف الطلب بنجاح!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";
        }
    }
}