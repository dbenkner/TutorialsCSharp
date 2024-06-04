using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using jwtAuth.Models;
using System.Security.Cryptography;
using System.Text;

public class jwtDbContext : DbContext
    {
    public jwtDbContext (DbContextOptions<jwtDbContext> options) : base(options){}

    public DbSet<jwtAuth.Models.Role> Roles { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<UserToRole> UsersToRoles { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<UserToRole>().HasKey(ur => new {ur.RoleId, ur.UserId}); 
        using var hmac = new HMACSHA256();
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Email="test@gmail.com", Username="admin", Name="admin", PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("adminpassword")), PasswordSalt = hmac.Key}
            );
        modelBuilder.Entity<Role>().HasData(new Role { Id = 1, RoleName = "user" });
        modelBuilder.Entity<Role>().HasData(new Role { Id = 2, RoleName = "admin" });
        modelBuilder.Entity<UserToRole>().HasData(new UserToRole { Id = 1, UserId = 1, RoleId = 1 });
        modelBuilder.Entity<UserToRole>().HasData(new UserToRole { Id = 2, UserId = 1, RoleId = 2 });
    }
}
