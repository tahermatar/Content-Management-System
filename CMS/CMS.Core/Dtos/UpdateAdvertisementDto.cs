using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Dtos
{
    public class UpdateAdvertisementDto
    {
        public int Id { get; set; }

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
    }
}
