using System.Text.Json.Serialization;

namespace JwtAuth.Demo.Dto
{
    public class RefreshTokenRequest
    {
        /// <summary>
        /// 刷新權杖
        /// </summary>
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
