namespace VehicleShowroom.Models
{
    public class Helmets
    {
       
            public int Id { get; set; }

            public string Brand { get; set; }   // اسم الماركة
            public string Color { get; set; }   // اللون
            public decimal Price { get; set; }  // السعر
            public string ImageUrl { get; set; } // رابط الصورة داخل wwwroot/images/helmets
        }
   }
