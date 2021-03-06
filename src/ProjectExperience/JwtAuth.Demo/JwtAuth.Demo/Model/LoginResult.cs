namespace JwtAuth.Demo.Model
{
    public class LoginResult
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 使用者角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 存取權杖
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新權杖
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
