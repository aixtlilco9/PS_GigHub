using System;
using GigHub.Core.Models;

namespace GigHub.Core.Dtos
{
    public class NotificationDto
    {
        //public int Id { get; private set; }

        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }

        //[Required]
        public GigDto Gig { get; set; } //becuz each notification if for one and only one gig
    }
}