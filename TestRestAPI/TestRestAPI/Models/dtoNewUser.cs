using System.ComponentModel.DataAnnotations;

namespace TestRestAPI.Models
{
    public class dtoNewUser
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string phoneNumber { get; set; }

    }
}
