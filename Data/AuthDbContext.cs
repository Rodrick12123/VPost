using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //seed roles
            var roleIdAdmin = "6822041f-07fe-466f-a56b-87601e7425df";
            var superRoleId = "05ba2f52-1aab-4cb7-bc53-41ea851a6eee";
            var userRoleId = "c5b03c78-f44d-4e6e-91a5-60afdb7f15cf";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    //generate guid for a unique identifier
                    //Console.WriteLine(Guid.NewGuid())
                    Id = roleIdAdmin,
                    ConcurrencyStamp = roleIdAdmin
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superRoleId,
                    ConcurrencyStamp = superRoleId

                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };
            //insert roles into the DB
            builder.Entity<IdentityRole>().HasData(roles);

            //seed SuperAdmin
            var superAdminId = "0b275ad8-842d-4423-ba91-a27ebbc0f3e7";
            var superAdminUser = new IdentityUser
            {
                UserName = "superAdmin@vpost.com",
                Email = "superAdmin@vpost.com",
                NormalizedEmail = "superAdmin@vpost.com".ToUpper(),
                NormalizedUserName = "superAdmin@vpost.com".ToUpper(),
                Id = superAdminId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "superAdmin12123");
            //insert user into the DB
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Give roles to user
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = roleIdAdmin,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superRoleId,
                    UserId = superAdminId
                }
            };
            //insert user role(s) into the DB
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}