using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public virtual AppUser User { get; set; }
    }
}
