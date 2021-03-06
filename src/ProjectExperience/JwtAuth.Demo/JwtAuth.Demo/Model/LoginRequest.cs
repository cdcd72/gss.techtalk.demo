using System.ComponentModel.DataAnnotations;

namespace JwtAuth.Demo.Model
{
    public class LoginRequest
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
