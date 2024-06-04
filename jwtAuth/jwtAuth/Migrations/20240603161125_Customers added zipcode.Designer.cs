﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace jwtAuth.Migrations
{
    [DbContext(typeof(jwtDbContext))]
    [Migration("20240603161125_Customers added zipcode")]
    partial class Customersaddedzipcode
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("jwtAuth.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("RepFirstName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RepLastName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<decimal>("SalesTotal")
                        .HasColumnType("Decimal(11,2)");

                    b.Property<string>("StateCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("jwtAuth.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "user"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "admin"
                        });
                });

            modelBuilder.Entity("jwtAuth.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varbinary(255)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varbinary(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "test@gmail.com",
                            Name = "admin",
                            PasswordHash = new byte[] { 213, 136, 211, 168, 215, 52, 54, 27, 219, 217, 148, 222, 161, 83, 132, 243, 180, 252, 129, 244, 29, 176, 203, 2, 19, 72, 184, 193, 236, 205, 209, 209 },
                            PasswordSalt = new byte[] { 109, 58, 35, 104, 147, 35, 9, 250, 14, 38, 128, 21, 168, 114, 215, 62, 173, 211, 151, 199, 30, 125, 95, 216, 218, 243, 36, 177, 208, 154, 177, 98, 239, 58, 212, 131, 196, 147, 123, 11, 100, 124, 250, 209, 25, 78, 159, 153, 71, 80, 204, 147, 19, 86, 251, 150, 100, 80, 85, 54, 110, 9, 66, 53 },
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("jwtAuth.Models.UserToRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersToRoles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            UserId = 1,
                            Id = 1
                        },
                        new
                        {
                            RoleId = 2,
                            UserId = 1,
                            Id = 2
                        });
                });

            modelBuilder.Entity("jwtAuth.Models.UserToRole", b =>
                {
                    b.HasOne("jwtAuth.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("jwtAuth.Models.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("jwtAuth.Models.User", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}