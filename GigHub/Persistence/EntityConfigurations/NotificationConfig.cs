using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class NotificationConfig :EntityTypeConfiguration<Notification>
    {
        public NotificationConfig()
        {
            //Property(g => g.Gig)
            //    .IsRequired();

            HasRequired(n => n.Gig);
        }
    }
}