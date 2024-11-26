using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public ICollection<ActivityAttendee> Activities { get; set; }
        
        public ICollection<Photo> Photos { get; set; }
        public ICollection<UserFollowing> Followings { get; set; } // current profiles of users following the user is following
        
        public ICollection<UserFollowing> Followers { get; set; } // current profiles of users that are following the user

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        
        
    }
}