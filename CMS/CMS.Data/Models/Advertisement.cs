using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        public string? WebsiteUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Price { get; set; }
        public string? OwnerId { get; set; }
        public User? Owner { get; set; }
    }
}
