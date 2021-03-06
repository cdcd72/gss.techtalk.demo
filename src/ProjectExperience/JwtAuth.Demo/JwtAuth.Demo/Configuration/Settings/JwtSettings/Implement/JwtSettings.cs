namespace JwtAuth.Demo.Configuration
{
    /// <summary>
    /// JWT 設定
    /// </summary>
    public class JwtSettings : IJwtSettings
    {
        public const string SectionName = "JwtSettings";

        /// <summary>
        /// 秘密 (不會讓 Client 端知道)
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// JWT 簽發者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// JWT 接收者 (簽給誰)
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 存取權杖過期時間
        /// </summary>
        public int AccessTokenExpiration { get; set; }

        /// <summary>
        /// 刷新權杖過期時間
        /// </summary>
        public int RefreshTokenExpiration { get; set; }
    }
}
