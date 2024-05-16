using DocHub.Common.DTO.Users;
using DocHub.Common.DTO.UserService;
using DocHub.Common.ResultModels;
using DocHub.Data;
using DocHub.Service.Abstracts.Users;
using DocHub.Service.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DocHub.WebApi.Controllers
{
    //https://markjames.dev/blog/jwt-authorization-asp-net-core
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserService UserService { get; }

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            UserService = userService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await UserService.CreateUser(request);

            if (result.Succeeded)
            {
                request.Password = "";
                return CreatedAtAction(nameof(Register), new { email = request.Email, role = request.Role }, request);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponseModel>> Authenticate([FromBody] AuthRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var authResult=await UserService.Authenticate(request);
            var cookieOptions = new CookieOptions
            {
                SameSite = SameSiteMode.Strict,
                HttpOnly = true,
                Secure = true
            };
            HttpContext.Response.Cookies.Append("token", authResult.Token, cookieOptions);
            //authResult.Token = "";
            return Ok(authResult);
        }
    }
}
