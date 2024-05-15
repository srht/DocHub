using DocHub.Core.Entities.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service.Abstracts.Users
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
