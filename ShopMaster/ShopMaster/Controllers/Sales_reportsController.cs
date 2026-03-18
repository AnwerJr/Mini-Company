using EcommerceERP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMaster.Data;
using ShopMaster.Models;
using ShopMaster.ViewModels;

namespace ShopMaster.Controllers
{
    public class Sales_reportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Sales_reportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales_reports
        public async Task<IActionResult> Index(int? year, int? month)
        {
            int selectedYear = year ?? DateTime.Now.Year;
            int? selectedMonth = month;

            var ordersQuery = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Customer)
                .Where(o => o.OrderDate.Year == selectedYear && o.Status != OrderStatus.Cancelled);

            if (selectedMonth.HasValue)
                ordersQuery = ordersQuery.Where(o => o.OrderDate.Month == selectedMonth.Value);

            var orders = await ordersQuery.ToListAsync();

            // ===== ملخص عام =====
            var totalRevenue = orders.Sum(o => o.TotalAmount);
            var totalOrders = orders.Count;
            var totalItemsSold = orders.SelectMany(o => o.OrderItems).Sum(oi => oi.Quantity);
            var avgOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;

            // ===== مبيعات شهرية =====
            var monthlySales = orders
                .GroupBy(o => o.OrderDate.Month)
                .Select(g => new MonthlySalesViewModel
                {
                    Month = g.Key,
                    MonthName = new DateTime(selectedYear, g.Key, 1).ToString("MMMM"),
                    TotalRevenue = g.Sum(o => o.TotalAmount),
                    TotalOrders = g.Count(),
                    TotalItemsSold = g.SelectMany(o => o.OrderItems).Sum(oi => oi.Quantity)
                })
                .OrderBy(m => m.Month)
                .ToList();

            // ===== أفضل المنتجات مبيعاً =====
            var topProducts = orders
                .SelectMany(o => o.OrderItems)
                .GroupBy(oi => new { oi.ProductId, Name = oi.Product.NameEn, oi.Product.Price })
                .Select(g => new ProductSalesViewModel
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    UnitPrice = g.Key.Price,
                    QuantitySold = g.Sum(oi => oi.Quantity),
                    TotalRevenue = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .OrderByDescending(p => p.TotalRevenue)
                .Take(10)
                .ToList();

            // ===== أفضل العملاء =====
            var topCustomers = orders
                .GroupBy(o => new { o.CustomerId, o.Customer.FullName })
                .Select(g => new CustomerSalesViewModel
                {
                    CustomerId = g.Key.CustomerId,
                    CustomerName = g.Key.FullName,
                    TotalOrders = g.Count(),
                    TotalSpent = g.Sum(o => o.TotalAmount)
                })
                .OrderByDescending(c => c.TotalSpent)
                .Take(10)
                .ToList();

            // ===== مبيعات حسب الفئة =====
            var categorySales = orders
                .SelectMany(o => o.OrderItems)
                .Where(oi => oi.Product.Category != null)
                .GroupBy(oi => new { oi.Product.CategoryId, CategoryName = oi.Product.Category.NameEn })
                .Select(g => new CategorySalesViewModel
                {
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.CategoryName,
                    TotalRevenue = g.Sum(oi => oi.Quantity * oi.UnitPrice),
                    QuantitySold = g.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(c => c.TotalRevenue)
                .ToList();

            var viewModel = new SalesReportViewModel
            {
                SelectedYear = selectedYear,
                SelectedMonth = selectedMonth,
                TotalRevenue = totalRevenue,
                TotalOrders = totalOrders,
                TotalItemsSold = totalItemsSold,
                AverageOrderValue = avgOrderValue,
                MonthlySales = monthlySales,
                TopProducts = topProducts,
                TopCustomers = topCustomers,
                CategorySales = categorySales,
                AvailableYears = await _context.Orders
                    .Select(o => o.OrderDate.Year)
                    .Distinct()
                    .OrderByDescending(y => y)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Sales_reports/Details - تفاصيل يوم محدد
        public async Task<IActionResult> DailyDetails(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);

            var orders = await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.Customer)
                .Where(o => o.OrderDate.Date == date.Date && o.Status != OrderStatus.Cancelled)
                .OrderBy(o => o.OrderDate)
                .ToListAsync();

            ViewBag.Date = date;
            ViewBag.TotalRevenue = orders.Sum(o => o.TotalAmount);
            ViewBag.TotalOrders = orders.Count;

            return View(orders);
        }
    }
}