public class AnalysisViewModel
{
    public int SelectedYear { get; set; }
    public int PreviousYear { get; set; }

    // مقارنة سنوية
    public decimal CurrentYearRevenue { get; set; }
    public decimal PreviousYearRevenue { get; set; }
    public decimal RevenueGrowthPercent { get; set; }

    public int CurrentYearOrders { get; set; }
    public int PreviousYearOrders { get; set; }
    public decimal OrdersGrowthPercent { get; set; }

    public decimal CancellationRate { get; set; }

    // بيانات تفصيلية
    public List<MonthlyComparisonViewModel> MonthlyComparison { get; set; } = new();
    public List<ProductAnalysisViewModel> ProductAnalysis { get; set; } = new();
    public List<CustomerAnalysisViewModel> CustomerAnalysis { get; set; } = new();
    public List<DayOfWeekAnalysisViewModel> OrdersByDayOfWeek { get; set; } = new();
    public List<ShopMaster.Models.Product> LowStockProducts { get; set; } = new();

    public List<int> AvailableYears { get; set; } = new();
}

public class MonthlyComparisonViewModel
{
    public int Month { get; set; }
    public string MonthName { get; set; } = "";
    public decimal CurrentYearRevenue { get; set; }
    public decimal PreviousYearRevenue { get; set; }
    public int CurrentYearOrders { get; set; }
    public int PreviousYearOrders { get; set; }
    public decimal GrowthPercent => PreviousYearRevenue > 0
        ? ((CurrentYearRevenue - PreviousYearRevenue) / PreviousYearRevenue) * 100 : 0;
}

public class ProductAnalysisViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int CurrentStock { get; set; }
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
    public int OrdersCount { get; set; }
}

public class CustomerAnalysisViewModel
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = "";
    public string CustomerEmail { get; set; } = "";
    public int TotalOrders { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal AverageOrderValue { get; set; }
    public DateTime FirstOrder { get; set; }
    public DateTime LastOrder { get; set; }
}

public class DayOfWeekAnalysisViewModel
{
    public DayOfWeek DayOfWeek { get; set; }
    public string DayName { get; set; } = "";
    public int OrderCount { get; set; }
    public decimal Revenue { get; set; }
}
