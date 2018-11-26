using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class UserNotificationConfig : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfig()
        {
            HasKey(u => new {u.UserId, u.NotificationId});

           HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}