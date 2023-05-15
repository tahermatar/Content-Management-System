﻿using CMS.Core.Enums;

namespace CMS.Core.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? ImageUrl { get; set; }
        public string? UserType { get; set; }
        //public UserType UserType { get; set; } 
        public DateTime CreatedAt { get; set; } 
    }
}
