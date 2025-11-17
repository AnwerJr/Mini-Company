using System.ComponentModel.DataAnnotations;

namespace TestRestAPI.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [MaxLength(50)]
        public String Name { get; set; }
        public String? Notes { get; set; }
    }
}
