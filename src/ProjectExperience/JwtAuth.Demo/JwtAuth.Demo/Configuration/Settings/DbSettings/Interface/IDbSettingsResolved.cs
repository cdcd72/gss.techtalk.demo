namespace JwtAuth.Demo.Configuration
{
    /// <summary>
    /// 解析後的資料庫設定
    /// </summary>
    public interface IDbSettingsResolved
    {
        /// <summary>
        /// 連線類型
        /// </summary>
        DbType ConnectionType { get; }

        /// <summary>
        /// 連線字串
        /// </summary>
        string ConnectionString { get; }
    }
}
