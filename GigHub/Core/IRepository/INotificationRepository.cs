using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.IRepository
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotifications(string userId);
    }
}