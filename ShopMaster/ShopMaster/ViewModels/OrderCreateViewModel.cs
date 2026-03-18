namespace ShopMaster.ViewModels
{
    public class OrderCreateViewModel
    {
        public int CustomerId { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Notes { get; set; }
        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}
