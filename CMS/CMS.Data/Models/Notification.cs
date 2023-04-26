﻿using CMS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data.Models
{
    public class Notification
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Message { get; set; }
        public NotificationAction Action { get; set; }
        public string? ActionId { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public DateTime SendAt { get; set; }
    }
}
