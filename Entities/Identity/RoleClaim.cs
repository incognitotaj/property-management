using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public virtual AppRole Role { get; set; }

    }
}
