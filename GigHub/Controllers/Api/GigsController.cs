using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistence;

namespace GigHub.Controllers.Api
{
    [Authorize]
    //to only be accesible by authenticated users
    public class GigsController : ApiController
    {
        //private ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            //TODO:bug if gig == null this pertain to 
            //unit testing so null may be return for a gigId and the following if crashes however it has been addres below
            if (gig == null || gig.IsCanceled)
            {
                return NotFound();
            }

            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return Unauthorized();
            }
            gig.Cancel();

            _unitOfWork.Complete();
            //_context.SaveChanges();

            return Ok();
        }

       
    }
}
