using System.Text.Json.Serialization;

namespace JwtAuth.Demo.Dto
{
    public class LoginResult
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 使用者角色
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// 存取權杖
        /// </summary>
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新權杖
        /// </summary>
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
