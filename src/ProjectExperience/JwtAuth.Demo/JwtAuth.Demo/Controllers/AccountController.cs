using System;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtAuth.Demo.Dto;
using JwtAuth.Demo.Helpers;
using JwtAuth.Demo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuth.Demo.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IJwtHelper jwtHelper;

        public AccountController(IUserService userService, IJwtHelper jwtHelper)
        {
            this.userService = userService;
            this.jwtHelper = jwtHelper;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!userService.IsValidUser(request.UserName, request.Password))
                return Unauthorized();

            var role = userService.GetUserRole(request.UserName);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtResult = jwtHelper.GenerateTokens(request.UserName, claims, DateTime.Now);

            return Ok(new LoginResult
            {
                UserName = request.UserName,
                Role = role,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }

        [HttpGet("[action]")]
        public ActionResult Logout()
        {
            var userName = User.Identity.Name;
            // TODO: 讓使用者現階段的存取權杖失效，可以嘗試將使用者當前的存取權杖加入到黑名單...
            //       (可參考: https://github.com/auth0/node-jsonwebtoken/issues/375)
            // 刪除使用者持有的刷新權杖
            jwtHelper.RemoveRefreshTokenByUserName(userName);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                    return BadRequest();

                var userName = User.Identity.Name;

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");

                var jwtResult = jwtHelper.Refresh(request.RefreshToken, accessToken, DateTime.Now);

                return Ok(new LoginResult
                {
                    UserName = userName,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpGet("user")]
        public ActionResult GetCurrentUser() => Ok(new LoginResult
        {
            UserName = User.Identity.Name,
            Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
        });
    }
}
