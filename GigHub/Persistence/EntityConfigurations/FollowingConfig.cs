using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class FollowingConfig : EntityTypeConfiguration<Following>
    {
        public FollowingConfig()
        {
            HasKey(f => new {f.FollowerId, f.FolloweeId});
        }
    }
}