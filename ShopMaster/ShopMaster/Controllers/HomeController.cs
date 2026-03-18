using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMaster.ViewModels;
using ShopMaster.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using EcommerceERP.ViewModels;
using ShopMaster.Data;

namespace ShopMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var viewModel = new DashboardViewModel
            {
                // إجمالي العدادات
                TotalOrders = await _context.Orders.CountAsync(),
                TotalCustomers = await _context.Customers.CountAsync(),
                TotalProducts = await _context.Products.CountAsync(),

                // إجمالي الإيرادات
                TotalRevenue = await _context.Orders
                    .Where(o => o.PaymentStatus == PaymentStatus.Paid)
                    .SumAsync(o => (decimal?)o.TotalAmount) ?? 0,

                // إيرادات اليوم
                TodayRevenue = await _context.Orders
                    .Where(o => o.OrderDate >= today && o.PaymentStatus == PaymentStatus.Paid)
                    .SumAsync(o => (decimal?)o.TotalAmount) ?? 0,

                // إيرادات الشهر
                MonthRevenue = await _context.Orders
                    .Where(o => o.OrderDate >= startOfMonth && o.PaymentStatus == PaymentStatus.Paid)
                    .SumAsync(o => (decimal?)o.TotalAmount) ?? 0,

                // طلبات قيد الانتظار
                PendingOrders = await _context.Orders
                    .Where(o => o.Status == OrderStatus.Pending)
                    .CountAsync(),

                // طلبات تم توصيلها
                DeliveredOrders = await _context.Orders
                    .Where(o => o.Status == OrderStatus.Delivered)
                    .CountAsync(),

                // آخر الطلبات - هنا المشكلة!
                RecentOrders = await _context.Orders
                    .Include(o => o.Customer)  // مهم جداً!
                    .OrderByDescending(o => o.OrderDate)
                    .Take(10)
                    .ToListAsync() ?? new List<Order>(),  // إذا null، نرجع list فاضي

                // المنتجات الأكثر مبيعاً
                TopSellingProducts = await _context.OrderItems
                    .Include(oi => oi.Product)
                        .ThenInclude(p => p.Category)  // مهم جداً!
                    .GroupBy(oi => oi.Product)
                    .Select(g => new ProductSalesViewModel
                    {
                        ProductName = g.Key.NameAr,
                        TotalQuantity = g.Sum(oi => oi.Quantity),
                        TotalRevenue = g.Sum(oi => oi.TotalPrice)
                    })
                    .OrderByDescending(p => p.TotalQuantity)
                    .Take(10)
                    .ToListAsync() ?? new List<ProductSalesViewModel>(),

                // المنتجات منخفضة المخزون
                LowStockProducts = await _context.Products
                    .Include(p => p.Category)  // مهم جداً!
                    .Where(p => p.StockQuantity < 10 && p.IsActive)
                    .OrderBy(p => p.StockQuantity)
                    .Take(10)
                    .ToListAsync() ?? new List<Product>()
            };

            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}