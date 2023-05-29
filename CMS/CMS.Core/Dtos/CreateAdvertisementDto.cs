using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CMS.Core.Dtos
{
    public class CreateAdvertisementDto
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "عنوان الإعلان")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "صورة الإعلان")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الموقع")]
        [Url]
        public string? WebsiteUrl { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "البداية")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "النهاية")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "السعر")]
        public float Price { get; set; }
        [Display(Name = "صاحب الإعلان")]
        public string? OwnerId { get; set; } = null;
        public CreateUserDto? Owner { get; set; }
    }
}
