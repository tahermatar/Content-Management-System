﻿using CMS.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Dtos
{
    public class UpdateUserDto
    {
        public string? Id { get; set; }
        [Required]
        [Display(Name = "اسم المستخدم")]
        public string? FullName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string? Email { get; set; }
        [Required]
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
