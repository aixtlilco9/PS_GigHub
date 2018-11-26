using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.IRepository;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserNotification> GetUserNotification(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();//if we make it .ToList then it must be IEnumerable else is its Iqueryable
        }
    }
}