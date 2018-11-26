using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.Core.IRepository
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followerId, string followeeId);
        //Following GetFollowingDto(FollowingDto dto, string userId);
        void Add(Following following);
        void Remove(Following following);

    }

}