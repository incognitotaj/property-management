using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class UserLogin : IdentityUserLogin<string>
    {
        public virtual AppUser User { get; set; }

        
    }
}
