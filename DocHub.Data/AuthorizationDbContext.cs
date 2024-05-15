using DocHub.Common.DTO.Users;
using DocHub.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Data
{
    public class AuthorizationDbContext:IdentityDbContext<ApplicationUser>
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var hasher = new PasswordHasher<ApplicationUser>();

            var adminEmail = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SiteSettings")["AdminEmail"];
            var adminPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SiteSettings")["AdminPassword"];

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "80c8b6b1-e2b6-45e8-b044-8f2178a90111", // primary key
                    UserName = "admin",
                    NormalizedUserName = adminEmail.ToUpper(),
                    PasswordHash = hasher.HashPassword(null, adminPassword),
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    Role=RoleEnumDto.Admin,
                    Token=""
                }
            );
        }
    }
}
