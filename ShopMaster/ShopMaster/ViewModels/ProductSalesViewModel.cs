using ShopMaster.Models;

namespace EcommerceERP.ViewModels
{
    public class ProductSalesViewModel
    {
        public Product Product { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}