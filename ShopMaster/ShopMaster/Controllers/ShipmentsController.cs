using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopMaster.Data;
using ShopMaster.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMaster.Controllers
{
    public class ShipmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShipmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shipments
        public async Task<IActionResult> Index()
        {
            var shipments = await _context.Shipments
                .Include(s => s.Order)
                    .ThenInclude(o => o.Customer)
                .OrderByDescending(s => s.ShippedDate)
                .ToListAsync();

            return View(shipments);
        }

        // GET: Shipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.Order)
                    .ThenInclude(o => o.Customer)
                .Include(s => s.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // GET: Shipments/Create
        public IActionResult Create()
        {
            // جلب الطلبات التي ليس لها شحنة بعد
            ViewData["OrderId"] = new SelectList(
                _context.Orders
                    .Include(o => o.Customer)
                    .Where(o => o.Shipment == null && o.Status != OrderStatus.Cancelled)
                    .Select(o => new
                    {
                        o.Id,
                        DisplayText = o.OrderNumber + " - " + o.Customer.FullName
                    }),
                "Id",
                "DisplayText"
            );

            return View();
        }

        // POST: Shipments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackingNumber,ShippingCompany,ShippedDate,DeliveryDate,EstimatedDelivery,Status,ShippingAddress,Notes,OrderId")] Shipment shipment)
        {
            // إزالة الـ validation للـ navigation property
            ModelState.Remove("Order");

            if (ModelState.IsValid)
            {
                try
                {
                    // التحقق من أن الطلب ليس له شحنة بالفعل
                    var existingShipment = await _context.Shipments
                        .FirstOrDefaultAsync(s => s.OrderId == shipment.OrderId);

                    if (existingShipment != null)
                    {
                        ModelState.AddModelError("OrderId", "هذا الطلب لديه شحنة بالفعل!");
                        ViewData["OrderId"] = new SelectList(
                            _context.Orders
                                .Include(o => o.Customer)
                                .Where(o => o.Shipment == null)
                                .Select(o => new
                                {
                                    o.Id,
                                    DisplayText = o.OrderNumber + " - " + o.Customer.FullName
                                }),
                            "Id",
                            "DisplayText",
                            shipment.OrderId
                        );
                        return View(shipment);
                    }

                    // إذا تم إدخال تاريخ الشحن ولم يتم إدخاله من قبل
                    if (shipment.ShippedDate == null && shipment.Status != ShipmentStatus.Pending)
                    {
                        shipment.ShippedDate = DateTime.Now;
                    }

                    // إذا تم التوصيل، حدث حالة الطلب
                    if (shipment.Status == ShipmentStatus.Delivered)
                    {
                        var order = await _context.Orders.FindAsync(shipment.OrderId);
                        if (order != null)
                        {
                            order.Status = OrderStatus.Delivered;
                            shipment.DeliveryDate = DateTime.Now;
                        }
                    }

                    _context.Add(shipment);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "تم إضافة الشحنة بنجاح!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "حدث خطأ أثناء حفظ الشحنة: " + ex.Message);
                }
            }

            // في حالة الخطأ، أعد عرض القائمة
            ViewData["OrderId"] = new SelectList(
                _context.Orders
                    .Include(o => o.Customer)
                    .Where(o => o.Shipment == null)
                    .Select(o => new
                    {
                        o.Id,
                        DisplayText = o.OrderNumber + " - " + o.Customer.FullName
                    }),
                "Id",
                "DisplayText",
                shipment.OrderId
            );

            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.Order)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (shipment == null)
            {
                return NotFound();
            }

            ViewData["OrderId"] = new SelectList(
                _context.Orders.Include(o => o.Customer)
                    .Select(o => new
                    {
                        o.Id,
                        DisplayText = o.OrderNumber + " - " + o.Customer.FullName
                    }),
                "Id",
                "DisplayText",
                shipment.OrderId
            );

            return View(shipment);
        }

        // POST: Shipments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrackingNumber,ShippingCompany,ShippedDate,DeliveryDate,EstimatedDelivery,Status,ShippingAddress,Notes,OrderId")] Shipment shipment)
        {
            if (id != shipment.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Order");

            if (ModelState.IsValid)
            {
                try
                {
                    // إذا تم تغيير الحالة إلى Delivered
                    if (shipment.Status == ShipmentStatus.Delivered && shipment.DeliveryDate == null)
                    {
                        shipment.DeliveryDate = DateTime.Now;

                        // تحديث حالة الطلب
                        var order = await _context.Orders.FindAsync(shipment.OrderId);
                        if (order != null)
                        {
                            order.Status = OrderStatus.Delivered;
                        }
                    }

                    _context.Update(shipment);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "تم تحديث الشحنة بنجاح!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentExists(shipment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["OrderId"] = new SelectList(
                _context.Orders.Include(o => o.Customer)
                    .Select(o => new
                    {
                        o.Id,
                        DisplayText = o.OrderNumber + " - " + o.Customer.FullName
                    }),
                "Id",
                "DisplayText",
                shipment.OrderId
            );

            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.Order)
                    .ThenInclude(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);

            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "تم حذف الشحنة بنجاح!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(e => e.Id == id);
        }
    }
}