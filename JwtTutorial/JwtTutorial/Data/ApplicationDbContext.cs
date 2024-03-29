using JwtLibrary;
using JwtLibrary.Data;
using JwtLibrary.Entities;
using JwtTutorial.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtTutorial.Data
{
    public class ApplicationDbContext : LoginPortalDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);
        }
        private void SeedUsers(ModelBuilder builder)
        {
            Customer user = new Customer()
            {
                FirstName = "Raj", LastName = "Take", Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                Email = "admin@gmail.com",
                NormalizedUserName = "Admin",
                NormalizedEmail = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
            };
            LoginPortalServiceExtension.SeedUserWithHashedPassword<Customer>(builder, user, "Admin@123");
            Customer user1 = new Customer() { FirstName = "User", LastName = "Surname", Id = "F7A13C3E-EB62-4193-9653-CB3BB571DADF", UserName = "User", Email = "user@gmail.com", NormalizedUserName = "User", NormalizedEmail = "user@gmail.com", LockoutEnabled = false, PhoneNumber = "1234567890", };
            LoginPortalServiceExtension.SeedUserWithHashedPassword<Customer>(builder, user1, "User@123"); 
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<ApplicationRoles>().HasData(
                new ApplicationRoles() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name= "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new ApplicationRoles() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" });
        }
        private void SeedUserRoles (ModelBuilder builder)
        {
            builder.Entity<ApplicationRoles>().HasData(
                new ApplicatonUserRole() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new ApplicatonUserRole() { RoleId = "c7b013f0-5201-4317-abd8-c211f91b7330", UserId = "F7A13C3E-EB62-4193-9653-CB3BB571DADF" });
        }
    }
}
