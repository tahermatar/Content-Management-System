using CMS.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Dtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "اسم المستخدم")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Phone]
        [Display(Name = "رقم الجوال")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "الصورة")]
        public IFormFile? Image { get; set; }
        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [Required]
        [Display(Name = "نوع المستخدم")]
        public UserType UserType { get; set; }
    }
}
