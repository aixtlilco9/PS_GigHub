using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.ApplicationInsights.Web;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;

namespace GigHub.Controllers.Api
{
    [Authorize]//bcuz it should only be avialable to logged in users
    public class NotificationsController : ApiController
    {
        //private ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public NotificationsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var notifications = _unitOfWork.Notification.GetNewNotifications(User.Identity.GetUserId());

            
            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
            //it appears the above line works and the below return which is noisey is no longer needed but here just incase 

            //return notifications.Select(n => new NotificationDto()
            //{
            //    DateTime = n.DateTime,
            //    Gig = new GigDto()
            //    {
            //        Artist = new UserDto()
            //        {
            //            Id = n.Gig.Artist.Id,
            //            Name = n.Gig.Artist.Name
            //        },
            //        DateTime = n.Gig.DateTime,
            //        Id = n.Gig.Id,
            //        IsCanceled = n.Gig.IsCanceled,
            //        Venue = n.Gig.Venue
            //    },
            //    OriginalDateTime = n.OriginalDateTime,
            //    OriginalVenue = n.OriginalVenue,
            //    Type = n.Type

            //});
            
        }

        [HttpPost]//post because from a restful api design point of view this action has the symatic of a new request.
        public IHttpActionResult MarkAsRead()
        {
            var notifications = _unitOfWork.UserNotification.GetUserNotification(User.Identity.GetUserId());

            notifications.ForEach(n => n.Read());
            //_context.SaveChanges();
            _unitOfWork.Complete();

            return Ok();

        }
    }
}
