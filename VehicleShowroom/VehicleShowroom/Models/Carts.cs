namespace VehicleShowroom.Models
{
    public class Carts
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }

        public Vehicle Vehicle { get; set; }
        public User User { get; set; }
    }
}
