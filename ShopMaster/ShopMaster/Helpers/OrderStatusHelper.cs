namespace EcommerceERP.Helpers
{
    public static class OrderStatusHelper
    {
        public static string GetStatusColor(string status)
        {
            return status switch
            {
                "Pending" => "warning",
                "Confirmed" => "info",
                "Processing" => "primary",
                "Shipped" => "secondary",
                "Delivered" => "success",
                "Cancelled" => "danger",
                _ => "secondary"
            };
        }

        public static string GetStatusText(string status)
        {
            return status switch
            {
                "Pending" => "قيد الانتظار",
                "Confirmed" => "مؤكد",
                "Processing" => "قيد التجهيز",
                "Shipped" => "تم الشحن",
                "Delivered" => "تم التوصيل",
                "Cancelled" => "ملغي",
                _ => "غير معروف"
            };
        }
    }
}
