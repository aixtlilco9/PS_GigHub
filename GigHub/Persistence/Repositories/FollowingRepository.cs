using System.Linq;
using GigHub.Core.Dtos;
using GigHub.Core.IRepository;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string followerId, string followeeId)
        {
           return _context.Followings
                .SingleOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == followerId);
        }

        //public Following GetFollowingDto(FollowingDto dto, string userId)
        //{
        //    return _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);
        //}

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }
        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}