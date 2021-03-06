using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtAuth.Demo.Model;

namespace JwtAuth.Demo.Helpers
{
    public interface IJwtHelper
    {
        /// <summary>
        /// 產生權杖資訊
        /// </summary>
        /// <param name="username">使用者名稱</param>
        /// <param name="claims">一些宣告</param>
        /// <param name="now">當前時間</param>
        /// <returns></returns>
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);

        /// <summary>
        /// 刷新權杖
        /// </summary>
        /// <param name="refreshToken">刷新權杖</param>
        /// <param name="accessToken">存取權杖</param>
        /// <param name="now">當前時間</param>
        /// <returns></returns>
        JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);

        /// <summary>
        /// 刪除刷新權杖(依據使用者名稱)
        /// </summary>
        /// <param name="userName">使用者名稱</param>
        void RemoveRefreshTokenByUserName(string userName);

        /// <summary>
        /// 解碼 JWT
        /// </summary>
        /// <param name="accessToken">存取權杖</param>
        /// <returns></returns>
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string accessToken);
    }
}
