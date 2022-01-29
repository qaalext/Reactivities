using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class UserFollowing
    {
        public string ObserverId { get; set; } 
        public AppUser Observer { get; set; } // person which follows another user
        public string TargetId { get; set; } 
        public AppUser Target { get; set; } // person which is followed by another user
    }
}