namespace JwtAuth.Demo.Model
{
    public class JwtAuthResult
    {
        /// <summary>
        /// 存取權杖
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新權杖
        /// </summary>
        public RefreshToken RefreshToken { get; set; }
    }
}
