﻿using CMS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Models
{
    public class Track : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public int TimeInMinute { get; set; }
        [Required]
        public string? TrackUrl { get; set; }
        [Required]
        public string? OwnerName { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? PublishedById { get; set; }
        public User? PublishedBy { get; set; }
        public ContentStatus Status { get; set; }
    }
}
