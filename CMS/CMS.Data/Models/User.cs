using CMS.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
        public DateTime? DOB { get; set; }
        public string? ImageUrl { get; set; }
        public UserType UserType { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public List<Post>? Posts { get; set; }
        public List<Track>? Tracks { get; set; }
        public List<Advertisement>? Advertisements { get; set; }
        public List<Email>? Emails { get; set; }
        public List<Notification>? Notifications { get; set; }

        // To Resolve nullable warnings

        //public User(string fullName, string imageUrl, string createdBy)
        //{
        //    FullName = fullName;
        //    ImageUrl = imageUrl;
        //    CreatedBy = createdBy;
        //}

        // or public required string FullName { get; set; }
    }
}
