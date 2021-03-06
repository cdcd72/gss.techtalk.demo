using System;
using Microsoft.Extensions.Options;

namespace JwtAuth.Demo.Configuration
{
    public class JwtSettingsBridge : IJwtSettingsResolved
    {
        private readonly IOptions<JwtSettings> jwtSettings;

        #region Constructor

        public JwtSettingsBridge(IOptionsSnapshot<JwtSettings> jwtSettings) =>
            this.jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));

        #endregion

        public string Secret => jwtSettings.Value.Secret;

        public string Issuer => jwtSettings.Value.Issuer;

        public string Audience => jwtSettings.Value.Audience;

        public int AccessTokenExpiration => jwtSettings.Value.AccessTokenExpiration;

        public int RefreshTokenExpiration => jwtSettings.Value.RefreshTokenExpiration;
    }
}
