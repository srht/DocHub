using DocHub.Core.Entities.Users;
using DocHub.Data.Abstracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using System.Data;

namespace DocHub.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public UsersRepository(UserManager<ApplicationUser> userManager, AuthorizationDbContext context)
        {
            UserManager = userManager;
            Context = context;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public AuthorizationDbContext Context { get; }

        public async Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password)
        {
            bool isTrue= await UserManager.CheckPasswordAsync(applicationUser, password);
            return isTrue;
        }

        public async Task<IdentityResult> Create(ApplicationUser applicationUser,string password)
        {
            var result = await UserManager.CreateAsync(applicationUser,password!);

            return result;
        }
        public async Task<IdentityResult> Delete(Guid id)
        {
            var foundUser = await GetUser(id);
            var result = await UserManager.DeleteAsync(foundUser);
            return result;
        }

        public async Task<ApplicationUser> GetUser(Guid id)
        {
            var foundUser = await UserManager.FindByIdAsync(id.ToString());
            return foundUser;
        }

        public async Task<ApplicationUser> GetUser(string email)
        {
            var foundUser = await UserManager.FindByEmailAsync(email);
            return foundUser;
        }

        public async Task<IdentityResult> SaveUser(ApplicationUser applicationUser)
        {
            var result= await UserManager.UpdateAsync(applicationUser);
            return result;
        }
    }
}
