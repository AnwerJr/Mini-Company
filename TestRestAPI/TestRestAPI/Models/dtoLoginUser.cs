using System.ComponentModel.DataAnnotations;

namespace TestRestAPI.Models
{
    public class dtoLoginUser
    {
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
