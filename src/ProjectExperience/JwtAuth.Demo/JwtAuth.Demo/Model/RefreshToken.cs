using System;

namespace JwtAuth.Demo.Model
{
    public class RefreshToken
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string UserName { get; set; } // 可視情況用於追蹤

        // 可以補上一些資訊欄位(ex. 使用者裝置、IP 位址...等等)

        /// <summary>
        /// 刷新權杖
        /// </summary>
        public string TokenString { get; set; }

        /// <summary>
        /// 過期時間
        /// </summary>
        public DateTime ExpireAt { get; set; }
    }
}
