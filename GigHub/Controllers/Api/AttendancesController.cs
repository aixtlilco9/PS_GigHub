using System.Linq;
using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using WebGrease;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        //private ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_context = new ApplicationDbContext();
        }
        //below made a Dto to replace [FromBody]int gigId with AttendanceDto dto
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {//check if current user already has an attendance for current gig

            var userId = User.Identity.GetUserId();

            //had a variable called exist but did cntr+shift+r to do inline variableand combine with if(exist)
            if (_unitOfWork.Attendances.GetAttendance(dto.GigId, userId) != null)
                return BadRequest("You Have already registered to attend this event!");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
           _unitOfWork.Attendances.Add(attendance);
           _unitOfWork.Complete();
            // _context.Attendances.Add(attendance);
            // _context.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(id, userId);

            if (attendance == null)
            {
                return NotFound();
            }

            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
