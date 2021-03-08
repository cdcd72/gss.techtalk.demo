using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JwtAuth.Demo.Dto
{
    public class LoginRequest
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Required]
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
