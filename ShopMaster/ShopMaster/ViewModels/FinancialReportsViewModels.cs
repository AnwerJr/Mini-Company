public class FinancialReportViewModel
{
    public int SelectedYear { get; set; }
    public int? SelectedMonth { get; set; }

    // ملخص مالي
    public decimal GrossRevenue { get; set; }
    public decimal COGS { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal GrossProfitMargin { get; set; }
    public decimal CancelledValue { get; set; }
    public decimal PendingValue { get; set; }

    // إحصاءات الطلبات
    public int TotalCompletedOrders { get; set; }
    public int TotalCancelledOrders { get; set; }
    public int TotalPendingOrders { get; set; }

    // تفاصيل
    public List<MonthlyFinancialViewModel> MonthlyFinancials { get; set; } = new();
    public List<CategoryFinancialViewModel> RevenueByCategory { get; set; } = new();
    public List<TopOrderViewModel> TopOrders { get; set; } = new();

    public List<int> AvailableYears { get; set; } = new();
}

public class MonthlyFinancialViewModel
{
    public int Month { get; set; }
    public string MonthName { get; set; } = "";
    public decimal GrossRevenue { get; set; }
    public decimal COGS { get; set; }
    public decimal GrossProfit { get; set; }
    public int OrderCount { get; set; }
}

public class CategoryFinancialViewModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
    public decimal Revenue { get; set; }
    public int ItemsSold { get; set; }
    public decimal RevenueShare { get; set; }
}

public class TopOrderViewModel
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; } = "";
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int ItemCount { get; set; }
}