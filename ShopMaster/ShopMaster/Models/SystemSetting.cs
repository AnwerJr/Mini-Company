using System;
using System.ComponentModel.DataAnnotations;

namespace ShopMaster.Models
{
    public class SystemSetting
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "المفتاح")]
        public string Key { get; set; }

        [Required]
        [Display(Name = "القيمة")]
        public string Value { get; set; }

        [StringLength(500)]
        [Display(Name = "الوصف")]
        public string Description { get; set; }

        [Display(Name = "تاريخ التعديل")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}