using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLibrary.Entities
{
    public class ApplicatonUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser? User { get; set; }
        public virtual ApplicationRoles? Role { get; set; }
    }
}
