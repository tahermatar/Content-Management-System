﻿using CMS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Models
{
    public class Post : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Body { get; set; }
        public int TimeInMinute { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? AuthorId { get; set; }
        public ContentStatus Status { get; set; }
        public User? Author { get; set; }
        public List<PostAttachment>? Attachments { get; set; }
    }
}
