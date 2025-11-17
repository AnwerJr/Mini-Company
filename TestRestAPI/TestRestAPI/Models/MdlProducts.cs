namespace TestRestAPI.Models
{
    public class MdlProducts
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        // Change ImageUrl type from IFormFile to string to store the image path
        public IFormFile ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
