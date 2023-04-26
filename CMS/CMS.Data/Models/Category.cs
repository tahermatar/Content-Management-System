﻿using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Track>? Tracks { get; set; }
    }
}
