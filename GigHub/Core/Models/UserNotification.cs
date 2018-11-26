using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    public class UserNotification
    {
        //usernotification is the assosition class between notificaation and user
       
        public string UserId { get; private set; }

        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }

        protected UserNotification()
        {// for entity framework bcuz when you create a custom constructor you should always create a a default cons because entity framework cannot call this con to create an instance on usernotification          
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (notification == null)
            {
                throw new ArgumentNullException("user");
            }

            User = user;
            Notification = notification;
        }


        public void Read()
        {//created this method instead of directly changing isread in notification controller because
            //objects are about behavior they are not just property bags
            //behaviors of each domain model?? not sure what mosh said
            IsRead = true;
        }
    }
}