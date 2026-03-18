using System;
using System.ComponentModel.DataAnnotations;

namespace ShopMaster.Models
{
    public enum UserRole
    {
        Admin = 1,
        Manager = 2,
        Employee = 3
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }  // ✅ شلنا [Required]

        public string? Avatar { get; set; }  // ✅ خلّيناها nullable بـ ?

        [Required]
        public UserRole Role { get; set; } = UserRole.Employee;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastLogin { get; set; }
    }
}