using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class AppRole : IdentityRole<string>
    {
        public DateTime Created { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        public AppRole()
        {
            UserRoles = new HashSet<UserRole>();
            RoleClaims = new HashSet<RoleClaim>();
        }
    }
}
