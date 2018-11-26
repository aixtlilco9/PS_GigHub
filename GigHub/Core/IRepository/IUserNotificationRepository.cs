using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.IRepository
{
    public interface IUserNotificationRepository
    {
        IEnumerable<UserNotification> GetUserNotification(string userId);
    }
}