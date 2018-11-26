using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        //private ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public FollowingsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {//Follow(FollowingDto dto)
            var userId = User.Identity.GetUserId();

            if (_unitOfWork.Followings.GetFollowing(userId, dto.FolloweeId) != null)
                return BadRequest("Following already exist");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complete();

            return Ok();

        }

        

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _unitOfWork.Followings.GetFollowing(userId, id);//_context.Followings
            //    .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (following == null)
            {
                return NotFound();
            }

            _unitOfWork.Followings.Remove(following);
            _unitOfWork.Complete();

            return Ok(id);
        }

    }
}
