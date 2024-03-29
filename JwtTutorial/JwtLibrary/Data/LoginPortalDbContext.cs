using JwtLibrary.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLibrary.Data
{
    public class LoginPortalDbContext<T> : IdentityDbContext<ApplicationUser, ApplicationRoles, string, ApplicationUserClaim, ApplicatonUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public LoginPortalDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>( b =>
            {
                b.HasMany(e => e.Claims).WithOne(e => e.User).HasForeignKey(ul => ul.UserId).IsRequired();

                b.HasMany(e => e.Logins).WithOne(e => e.User).HasForeignKey(ut => ut.UserId).IsRequired();
            });

            builder.Entity<ApplicationRoles>(b =>
            {
                b.HasMany(e => e.UserRoles).WithOne(e => e.Role).HasForeignKey(ur => ur.RoleId).IsRequired();   
                b.HasMany(e => e.RoleClaims).WithOne(e => e.Role).HasForeignKey(rc => rc.RoleId).IsRequired();
            });
        }
    }
}
