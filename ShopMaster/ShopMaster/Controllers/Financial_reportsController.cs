using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMaster.Data;
using ShopMaster.Models;
using ShopMaster.ViewModels;

namespace ShopMaster.Controllers
{
    public class Financial_reportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Financial_reportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Financial_reports
        public async Task<IActionResult> Index(int? year, int? month)
        {
            int selectedYear = year ?? DateTime.Now.Year;
            int? selectedMonth = month;

            // ===== جلب بيانات الطلبات =====
            var ordersQuery = _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Where(o => o.OrderDate.Year == selectedYear);

            if (selectedMonth.HasValue)
                ordersQuery = ordersQuery.Where(o => o.OrderDate.Month == selectedMonth.Value);

            var orders = await ordersQuery.ToListAsync();

            // Replace string comparisons with enum comparisons for OrderStatus
            var completedOrders = orders.Where(o => o.Status == OrderStatus.Delivered).ToList();
            var cancelledOrders = orders.Where(o => o.Status == OrderStatus.Cancelled).ToList();
            var pendingOrders = orders.Where(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Processing).ToList();

            // ===== إجماليات مالية =====
            decimal grossRevenue = completedOrders.Sum(o => o.TotalAmount);
            decimal cancelledValue = cancelledOrders.Sum(o => o.TotalAmount);
            decimal pendingValue = pendingOrders.Sum(o => o.TotalAmount);

            // تكلفة البضاعة المباعة (COGS) - افتراض 60% من الإيراد
            decimal cogs = grossRevenue * 0.60m;
            decimal grossProfit = grossRevenue - cogs;
            decimal grossProfitMargin = grossRevenue > 0 ? (grossProfit / grossRevenue) * 100 : 0;

            // ===== مؤشرات شهرية =====
            var monthlyFinancials = completedOrders
                .GroupBy(o => o.OrderDate.Month)
                .Select(g => new MonthlyFinancialViewModel
                {
                    Month = g.Key,
                    MonthName = new DateTime(selectedYear, g.Key, 1).ToString("MMMM"),
                    GrossRevenue = g.Sum(o => o.TotalAmount),
                    OrderCount = g.Count(),
                    COGS = g.Sum(o => o.TotalAmount) * 0.60m,
                    GrossProfit = g.Sum(o => o.TotalAmount) * 0.40m,
                })
                .OrderBy(m => m.Month)
                .ToList();

            // ===== تحليل الإيرادات حسب الفئة =====
            var revenueByCategory = completedOrders
                .SelectMany(o => o.OrderItems)
                .Where(oi => oi.Product?.Category != null)
                .GroupBy(oi => new { oi.Product.CategoryId, oi.Product.Category.NameEn })
                .Select(g => new CategoryFinancialViewModel
                {
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.NameEn,
                    Revenue = g.Sum(oi => oi.Quantity * oi.UnitPrice),
                    ItemsSold = g.Sum(oi => oi.Quantity),
                    RevenueShare = 0 // يُحسب بعد ذلك
                })
                .OrderByDescending(c => c.Revenue)
                .ToList();

            // احسب نسبة الإيراد لكل فئة
            if (grossRevenue > 0)
            {
                foreach (var cat in revenueByCategory)
                    cat.RevenueShare = (cat.Revenue / grossRevenue) * 100;
            }

            // ===== أعلى 5 طلبات قيمة =====
            var topOrders = completedOrders
                .OrderByDescending(o => o.TotalAmount)
                .Take(5)
                .Select(o => new TopOrderViewModel
                {
                    OrderId = o.Id,
                    CustomerName = o.Customer?.FullName ?? "غير معروف",
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    ItemCount = o.OrderItems.Count
                })
                .ToList();

            var viewModel = new FinancialReportViewModel
            {
                SelectedYear = selectedYear,
                SelectedMonth = selectedMonth,

                GrossRevenue = grossRevenue,
                CancelledValue = cancelledValue,
                PendingValue = pendingValue,
                COGS = cogs,
                GrossProfit = grossProfit,
                GrossProfitMargin = grossProfitMargin,

                TotalCompletedOrders = completedOrders.Count,
                TotalCancelledOrders = cancelledOrders.Count,
                TotalPendingOrders = pendingOrders.Count,

                MonthlyFinancials = monthlyFinancials,
                RevenueByCategory = revenueByCategory,
                TopOrders = topOrders,

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