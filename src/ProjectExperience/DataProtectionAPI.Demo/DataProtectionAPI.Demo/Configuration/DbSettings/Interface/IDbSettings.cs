namespace DataProtectionAPI.Demo.Configuration
{
    /// <summary>
    /// 資料庫設定結構約束
    /// </summary>
    public interface IDbSettings
    {
        /// <summary>
        /// 連線類型
        /// </summary>
        string ConnectionType { get; }

        /// <summary>
        /// Oracle 連線屬性
        /// </summary>
        OracleConnectionProperties Oracle { get; }

        /// <summary>
        /// MsSqlServer 連線屬性
        /// </summary>
        MsSqlServerConnectionProperties MSSQLServer { get; }

        /// <summary>
        /// 伺服器位址
        /// </summary>
        string Server { get; }

        /// <summary>
        /// 連接埠
        /// </summary>
        string Port { get; }

        /// <summary>
        /// 使用者 ID
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// 密碼
        /// </summary>
        string Password { get; }
    }
}
