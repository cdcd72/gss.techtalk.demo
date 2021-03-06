using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtAuth.Demo.Configuration;
using JwtAuth.Demo.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuth.Demo.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IMemoryCache memoryCache;
        private readonly IJwtSettingsResolved jwtSettings;
        private readonly byte[] secret;

        #region Constructor

        public JwtHelper(IMemoryCache memoryCache, IJwtSettingsResolved jwtSettings)
        {
            this.memoryCache = memoryCache;
            this.jwtSettings = jwtSettings;
            secret = Encoding.UTF8.GetBytes(jwtSettings.Secret);
        }

        #endregion

        /// <summary>
        /// 產生權杖資訊
        /// </summary>
        /// <param name="username">使用者名稱</param>
        /// <param name="claims">一些宣告</param>
        /// <param name="now">當前時間</param>
        /// <returns></returns>
        public JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            var jwtToken = new JwtSecurityToken(
                jwtSettings.Issuer,
                shouldAddAudienceClaim ? jwtSettings.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(jwtSettings.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                UserName = username,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = now.AddMinutes(jwtSettings.RefreshTokenExpiration)
            };

            // 刷新權杖存取機制可以拉到資料庫或其他分散式快取處理，這邊只是範例放在記憶體快取中...
            // 刷新權杖管理機制可視情況調整，這邊僅示範一名使用者只能持有一組有時限的刷新權杖...
            memoryCache.Set(refreshToken.UserName, refreshToken);

            return new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        /// <summary>
        /// 刷新權杖
        /// </summary>
        /// <param name="refreshToken">刷新權杖</param>
        /// <param name="accessToken">存取權杖</param>
        /// <param name="now">當前時間</param>
        /// <returns></returns>
        public JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now)
        {
            var (principal, jwtToken) = DecodeJwtToken(accessToken);

            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                throw new SecurityTokenException("Invalid token");

            var userName = principal.Identity.Name;

            if (!memoryCache.TryGetValue(userName, out RefreshToken existingRefreshToken))
                throw new SecurityTokenException("Invalid token");

            // 確認是否為刷新權杖的持有者 or 是否過期 or 是否為持有者當下所擁有刷新權杖
            if (existingRefreshToken.UserName != userName || existingRefreshToken.ExpireAt < now || existingRefreshToken.TokenString != refreshToken)
                throw new SecurityTokenException("Invalid token");

            return GenerateTokens(userName, principal.Claims.ToArray(), now);
        }

        /// <summary>
        /// 刪除刷新權杖(依據使用者名稱)
        /// </summary>
        /// <param name="userName">使用者名稱</param>
        public void RemoveRefreshTokenByUserName(string userName)
        {
            var exist = memoryCache.TryGetValue(userName, out RefreshToken existingRefreshToken);

            if (exist)
                memoryCache.Remove(existingRefreshToken.UserName);
        }

        /// <summary>
        /// 解碼 JWT
        /// </summary>
        /// <param name="accessToken">存取權杖</param>
        /// <returns></returns>
        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new SecurityTokenException("Invalid token");

            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(accessToken,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidAudience = jwtSettings.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    },
                    out var validatedToken);

            return (principal, validatedToken as JwtSecurityToken);
        }

        #region Private Method

        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        #endregion
    }
}
