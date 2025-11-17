using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; } // Car or Motorcycle

        [Required]

        public string Brand { get; set; }

        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }
    }
}
