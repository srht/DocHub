using DocHub.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Data.Abstracts
{
    public interface IUsersRepository
    {
        Task<ApplicationUser> GetUser(Guid id);
        Task<ApplicationUser> GetUser(string email);
        Task<IdentityResult> Create(ApplicationUser applicationUser,string password);
        Task<IdentityResult> Delete(Guid id);
        Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password);

        Task<IdentityResult> SaveUser(ApplicationUser applicationUser);
    }
}
