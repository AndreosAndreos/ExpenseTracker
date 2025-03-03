﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ExpanseTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>      // while creating Identities add <AppUser> so it knows what data does it neet for authentication
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<IdentityRole>().HasData(             // craeting roles in the database (initialization of the database)
                new IdentityRole 
                {
                    Id = "5a3a3f9a-889b-47fe-98f5-15d876e93f7b",
                    Name = "User",
                    NormalizedName = "NAME"
                },
                new IdentityRole 
                {
                    Id = "0d319d52-8efa-41d3-a0e2-10aebf0551f8",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole 
                {
                    Id = "96910574-f9b8-4be8-a3ff-27e86906746e",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                }

            );

            var hasher = new PasswordHasher<AppUser>();        // so the password being passed to the database will be saver as hashed

            builder.Entity<AppUser>().HasData(             // new user - ADMINISTRATOR
                new AppUser
                {
                Id = "d2f29953-2efe-4b26-9a6b-1824e9163511",
                Email = "admin@locahost.com",
                NormalizedEmail = "ADMIN@LOCAHOST.COM",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                UserName = "admin@localhost.com",
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                EmailConfirmed = true,
                FirstName="Default",
                LastName="Admin",
                DateOfBirth=new DateOnly(1950,12,19)
            });

            builder.Entity<IdentityUserRole<string>>().HasData(     //binding the user with a role (admin)
                new IdentityUserRole<string>
                {
                    RoleId = "0d319d52-8efa-41d3-a0e2-10aebf0551f8",
                    UserId = "d2f29953-2efe-4b26-9a6b-1824e9163511"
                }
            );

            builder.Entity<Budget>()
                .Property(b => b.Amount)
                .HasPrecision(18, 2);

            builder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Budget> Budgets {  get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
