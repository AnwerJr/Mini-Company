using ShopMaster.Models;

namespace ShopMaster.ViewModels
{
    public class DashboardViewModel
    {
        // العدادات
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }

        // الإيرادات
        public decimal TotalRevenue { get; set; }
        public decimal TodayRevenue { get; set; }
        public decimal MonthRevenue { get; set; }

        // حالات الطلبات
        public int PendingOrders { get; set; }
        public int DeliveredOrders { get; set; }

        // القوائم
        public List<Order> RecentOrders { get; set; }  // هذا موجود
        public List<ProductSalesViewModel> TopSellingProducts { get; set; }  // هذا موجود
        public List<Product> LowStockProducts { get; set; }  // هذا موجود
    }
}