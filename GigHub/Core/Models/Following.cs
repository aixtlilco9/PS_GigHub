using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Core.Models
{
    //Step 1 in implementing follow artist create model which is this
    //2) add icollection props to Application user
    //3)added dbset to dbcontext add migration and update
    //4) create webapi controller followings
    //5) create dto following
    //alternatively, this class could be called relationships
    public class Following
    {
 
        public string FollowerId { get; set; }

        public string FolloweeId { get; set; }

        //similar to attendance class
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
    }
}