namespace JwtAuth.Demo.Configuration
{
    public interface IJwtSettings
    {
        /// <summary>
        /// 秘密 (不會讓 Client 端知道)
        /// </summary>
        public string Secret { get; }

        /// <summary>
        /// JWT 簽發者
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// JWT 接收者 (簽給誰)
        /// </summary>
        public string Audience { get; }

        /// <summary>
        /// 存取權杖過期時間
        /// </summary>
        public int AccessTokenExpiration { get; }

        /// <summary>
        /// 刷新權杖過期時間
        /// </summary>
        public int RefreshTokenExpiration { get; }
    }
}
