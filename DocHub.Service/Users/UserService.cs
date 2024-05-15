using Azure.Core;
using DocHub.Common.DTO.Users;
using DocHub.Common.DTO.UserService;
using DocHub.Common.ResultModels;
using DocHub.Core.Entities.Users;
using DocHub.Data.Abstracts;
using DocHub.Service.Abstracts.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service.Users
{
    public class UserService : IUserService
    {
        public UserService(IUsersRepository usersRepository, ITokenService tokenService)
        {
            UsersRepository = usersRepository;
            TokenService = tokenService;
        }

        public IUsersRepository UsersRepository { get; }
        public ITokenService TokenService { get; }

        public async Task<AuthResponseModel> Authenticate(AuthRequestDto authRequestDto)
        {
            var foundUser = await UsersRepository.GetUser(authRequestDto.Email);
            bool result= await UsersRepository.CheckPasswordAsync(foundUser, authRequestDto.Password);
            if(!result)
            {
                return new AuthResponseModel
                {
                    ResultMessage = "Error occured",
                    Succeeded = false
                };
            }

            var accessToken = TokenService.CreateToken(foundUser);
            foundUser.Token = accessToken;
            var tokenSaveResult= await UsersRepository.SaveUser(foundUser);
            if (tokenSaveResult.Succeeded)
                return new AuthResponseModel
                {
                    Username = foundUser.UserName,
                    Email = foundUser.Email,
                    Token = accessToken,
                    Succeeded = true
                };
            return new AuthResponseModel
            {
                ResultMessage = "Error occured",Succeeded=false,
                Errors = tokenSaveResult.Errors.Select(i => new UserErrorModel { Code = i.Code, Description = i.Description })
            };
        }

        public async Task<RegisterResponseModel> CreateUser(RegisterDto registerDto)
        {
            var result = await UsersRepository.Create(new ApplicationUser { UserName = registerDto.Email, Email = registerDto.Email, Role = RoleEnumDto.User },
            registerDto.Password!);
            if (result.Succeeded)
            {
                return new RegisterResponseModel
                {
                    Email=registerDto.Email, Role=RoleEnumDto.User.ToString(), ResultMessage="",
                    Succeeded=true,
                    Errors=result.Errors.Select(i=> new UserErrorModel { Code= i.Code, Description= i.Description })
                };
            }

            return new RegisterResponseModel
            {
                ResultMessage = "Register error ",
                Succeeded = false,
                Errors = result.Errors.Select(i => new UserErrorModel { Code = i.Code, Description = i.Description })
            };
        }
    }
}
