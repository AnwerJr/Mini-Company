using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMaster.Data;
using ShopMaster.Models;
using ShopMaster.ViewModels;

namespace ShopMaster.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnalysisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Analysis - تحليل الأداء الشامل
        public async Task<IActionResult> Index(int? year)
        {
            int selectedYear = year ?? DateTime.Now.Year;
            int previousYear = selectedYear - 1;

            var currentYearOrders = await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.Customer)
                .Where(o => o.OrderDate.Year == selectedYear && o.Status != OrderStatus.Cancelled)
                .ToListAsync();

            var previousYearOrders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.OrderDate.Year == previousYear && o.Status != OrderStatus.Cancelled)
                .ToListAsync();

            // ===== مقارنة سنوية =====
            decimal currentRevenue = currentYearOrders.Sum(o => o.TotalAmount);
            decimal previousRevenue = previousYearOrders.Sum(o => o.TotalAmount);
            decimal revenueGrowth = previousRevenue > 0
                ? ((currentRevenue - previousRevenue) / previousRevenue) * 100 : 0;

            int currentOrderCount = currentYearOrders.Count;
            int previousOrderCount = previousYearOrders.Count;
            decimal ordersGrowth = previousOrderCount > 0
                ? ((currentOrderCount - previousOrderCount) / (decimal)previousOrderCount) * 100 : 0;

            // ===== أداء شهري مقارن =====
            var monthlyComparison = Enumerable.Range(1, 12).Select(m => new MonthlyComparisonViewModel
            {
                Month = m,
                MonthName = new DateTime(selectedYear, m, 1).ToString("MMM"),
                CurrentYearRevenue = currentYearOrders
                    .Where(o => o.OrderDate.Month == m)
                    .Sum(o => o.TotalAmount),
                PreviousYearRevenue = previousYearOrders
                    .Where(o => o.OrderDate.Month == m)
                    .Sum(o => o.TotalAmount),
                CurrentYearOrders = currentYearOrders.Count(o => o.OrderDate.Month == m),
                PreviousYearOrders = previousYearOrders.Count(o => o.OrderDate.Month == m),
            }).ToList();

            // ===== تحليل المنتجات =====
            var productAnalysis = currentYearOrders
                .SelectMany(o => o.OrderItems)
                .GroupBy(oi => new { oi.ProductId, oi.Product.NameEn, oi.Product.StockQuantity, oi.Product.Price })
                .Select(g => new ProductAnalysisViewModel
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.NameEn,
                    UnitPrice = g.Key.Price,
                    CurrentStock = g.Key.StockQuantity,
                    QuantitySold = g.Sum(oi => oi.Quantity),
                    Revenue = g.Sum(oi => oi.Quantity * oi.UnitPrice),
                    OrdersCount = g.Select(oi => oi.OrderId).Distinct().Count()
                })
                .OrderByDescending(p => p.Revenue)
                .ToList();

            // ===== تحليل العملاء =====
            var customerAnalysis = currentYearOrders
                .GroupBy(o => new { o.CustomerId, o.Customer?.FullName, o.Customer?.Email })
                .Select(g => new CustomerAnalysisViewModel
                {
                    CustomerId = g.Key.CustomerId,
                    CustomerName = g.Key.FullName ?? "غير معروف",
                    CustomerEmail = g.Key.Email ?? "",
                    TotalOrders = g.Count(),
                    TotalSpent = g.Sum(o => o.TotalAmount),
                    AverageOrderValue = g.Average(o => o.TotalAmount),
                    FirstOrder = g.Min(o => o.OrderDate),
                    LastOrder = g.Max(o => o.OrderDate)
                })
                .OrderByDescending(c => c.TotalSpent)
                .Take(20)
                .ToList();

            // ===== معدل التحويل / الإلغاء =====
            var totalOrders = await _context.Orders.CountAsync(o => o.OrderDate.Year == selectedYear);
            var cancelledOrders = await _context.Orders.CountAsync(o => o.OrderDate.Year == selectedYear && o.Status == OrderStatus.Cancelled);
            decimal cancellationRate = totalOrders > 0 ? (cancelledOrders / (decimal)totalOrders) * 100 : 0;

            // ===== توزيع الطلبات على أيام الأسبوع =====
            var ordersByDayOfWeek = currentYearOrders
                .GroupBy(o => o.OrderDate.DayOfWeek)
                .Select(g => new DayOfWeekAnalysisViewModel
                {
                    DayOfWeek = g.Key,
                    DayName = g.Key.ToString(),
                    OrderCount = g.Count(),
                    Revenue = g.Sum(o => o.TotalAmount)
                })
                .OrderBy(d => d.DayOfWeek)
                .ToList();

            // ===== منتجات منخفضة المخزون =====
            var lowStockProducts = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.StockQuantity <= 10)
                .OrderBy(p => p.StockQuantity)
                .Take(10)
                .ToListAsync();

            var viewModel = new AnalysisViewModel
            {
                SelectedYear = selectedYear,
                PreviousYear = previousYear,
                CurrentYearRevenue = currentRevenue,
                PreviousYearRevenue = previousRevenue,
                RevenueGrowthPercent = revenueGrowth,

                CurrentYearOrders = currentOrderCount,
                PreviousYearOrders = previousOrderCount,
                OrdersGrowthPercent = ordersGrowth,

                CancellationRate = cancellationRate,

                MonthlyComparison = monthlyComparison,
                ProductAnalysis = productAnalysis,
                CustomerAnalysis = customerAnalysis,
                OrdersByDayOfWeek = ordersByDayOfWeek,
                LowStockProducts = lowStockProducts,

                AvailableYears = await _context.Orders
                    .Select(o => o.OrderDate.Year)
                    .Distinct()
                    .OrderByDescending(y => y)
                    .ToListAsync()
            };

            return View(viewModel);
        }
    }
}