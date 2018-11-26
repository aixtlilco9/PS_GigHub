using System.Data.Entity;
using GigHub.Core.Models;
using GigHub.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        //below was added to function with the attendance model.
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //includeded the below code to prevent cascade delete from gigs to attendees... artist to attendess is still on cascade delte watch mosh: implementing a use case from top to bottom
        //overriding Cf conventiosn using fluent api (override onMC +tab)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserConfig());
            modelBuilder.Configurations.Add(new AttendanceConfig());
            modelBuilder.Configurations.Add(new FollowingConfig());
            modelBuilder.Configurations.Add(new GenreConfig());
            modelBuilder.Configurations.Add(new GigConfig());
            modelBuilder.Configurations.Add(new NotificationConfig());
            modelBuilder.Configurations.Add(new UserNotificationConfig());
          
            //was rewritten in Gigconfig
            //modelBuilder.Entity<Attendance>()
            //    .HasRequired(a => a.Gig)
            //    .WithMany(g => g.Attendances)
            //    .WillCascadeOnDelete(false);


            // will use fluent api to turn off cascade delte from one of the relationships
            //this message was for the usernotification user/usernotification

            base.OnModelCreating(modelBuilder);
        }
    }
}