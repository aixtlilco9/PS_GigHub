using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string OriginalVenue { get; private set; }

        public Gig Gig { get; private set; } //becuz each notification if for one and only one gig

        protected Notification()
        {

        }

        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
            {
                throw new ArgumentNullException("gig");
            }

            Gig = gig;
            Type = type;
            DateTime = DateTime.Now;


        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }
       public static Notification GigUpdated(Gig newGig, DateTime originalDataTime, string originalVenue)
        {
           var notification = new Notification(NotificationType.GigUpdated, newGig);
            notification.OriginalDateTime = originalDataTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}