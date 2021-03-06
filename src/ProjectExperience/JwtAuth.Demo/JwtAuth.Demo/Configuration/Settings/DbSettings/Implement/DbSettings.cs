namespace JwtAuth.Demo.Configuration
{
    /// <summary>
    /// 資料庫設定
    /// </summary>
    public class DbSettings : IDbSettings
    {
        public const string SectionName = "DbSettings";

        /// <summary>
        /// 連線類型
        /// </summary>
        public string ConnectionType { get; set; }

        /// <summary>
        /// Oracle 連線屬性
        /// </summary>
        public OracleConnectionProperties Oracle { get; set; }

        /// <summary>
        /// MSSQLServer 連線屬性
        /// </summary>
        public MsSqlServerConnectionProperties MSSQLServer { get; set; }

        /// <summary>
        /// 伺服器位址
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 連接埠
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 使用者 ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// Oracle 連線屬性
    /// </summary>
    public class OracleConnectionProperties
    {
        /// <summary>
        /// 服務名稱
        /// </summary>
        public string ServiceName { get; set; }
    }

    /// <summary>
    /// MsSqlServer 連線屬性
    /// </summary>
    public class MsSqlServerConnectionProperties
    {
        /// <summary>
        /// 資料庫名稱
        /// </summary>
        public string InitialCatalog { get; set; }
    }
}
