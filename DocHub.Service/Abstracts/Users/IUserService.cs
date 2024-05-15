using DocHub.Common.DTO.Users;
using DocHub.Common.DTO.UserService;
using DocHub.Common.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service.Abstracts.Users
{
    public interface IUserService
    {
        Task<RegisterResponseModel> CreateUser(RegisterDto registerDto);
        Task<AuthResponseModel> Authenticate(AuthRequestDto authRequestDto);
    }
}
