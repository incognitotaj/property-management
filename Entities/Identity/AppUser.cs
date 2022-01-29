using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class AppUser : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }

        public AppUser()
        {
            UserLogins = new HashSet<UserLogin>();
            UserClaims = new HashSet<UserClaim>();
            UserRoles = new HashSet<UserRole>();
            UserTokens = new HashSet<UserToken>();
        }
    }
}
