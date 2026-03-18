using System.ComponentModel.DataAnnotations;

namespace ShopMaster.Models
{
    public enum ShipmentStatus { Pending = 1, PickedUp, InTransit, OutForDelivery, Delivered, Failed, Returned }

    public class Shipment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TrackingNumber { get; set; }

        [StringLength(100)]
        public string ShippingCompany { get; set; }

        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? EstimatedDelivery { get; set; }

        [Required]
        public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;

        [StringLength(500)]
        public string ShippingAddress { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
