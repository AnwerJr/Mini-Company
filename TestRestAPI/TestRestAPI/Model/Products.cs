namespace TestRestAPI.Model
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        // Change ImageUrl type from IFormFile to string to store the image path
        public string? ImageUrl { get; set; } // ✅ أضف السطر ده
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
