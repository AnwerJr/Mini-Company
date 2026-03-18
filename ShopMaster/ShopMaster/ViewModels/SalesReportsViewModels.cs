public class SalesReportViewModel
{
    public int SelectedYear { get; set; }
    public int? SelectedMonth { get; set; }

    // إجماليات
    public decimal TotalRevenue { get; set; }
    public int TotalOrders { get; set; }
    public int TotalItemsSold { get; set; }
    public decimal AverageOrderValue { get; set; }

    // بيانات تفصيلية
    public List<MonthlySalesViewModel> MonthlySales { get; set; } = new();
    public List<ProductSalesViewModel> TopProducts { get; set; } = new();
    public List<CustomerSalesViewModel> TopCustomers { get; set; } = new();
    public List<CategorySalesViewModel> CategorySales { get; set; } = new();

    public List<int> AvailableYears { get; set; } = new();
}

public class MonthlySalesViewModel
{
    public int Month { get; set; }
    public string MonthName { get; set; } = "";
    public decimal TotalRevenue { get; set; }
    public int TotalOrders { get; set; }
    public int TotalItemsSold { get; set; }
}

public class ProductSalesViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int QuantitySold { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalQuantity { get; internal set; }
}

public class CustomerSalesViewModel
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = "";
    public int TotalOrders { get; set; }
    public decimal TotalSpent { get; set; }
}

public class CategorySalesViewModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
    public decimal TotalRevenue { get; set; }
    public int QuantitySold { get; set; }
}