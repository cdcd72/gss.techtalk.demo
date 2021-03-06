using System;
using System.Data.SqlClient;
using JwtAuth.Demo.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace JwtAuth.Demo.Configuration
{
    /// <summary>
    /// 橋接 DbSettings
    /// PS. 已透過 DI 將組態 DbSettings 區段綁定至 DbSettings 物件情況下，對其物件進行其它操作(ex. 驗證、解密...)
    /// </summary>
    public class DbSettingsBridge : IDbSettingsResolved
    {
        private readonly IOptions<DbSettings> dbSettings;
        private readonly IDataProtector protector;

        #region Constructor

        public DbSettingsBridge(IOptionsSnapshot<DbSettings> dbSettings, ISettingsValidator<DbSettings> validator, IDataProtectionProvider dataProtectionProvider)
        {
            this.dbSettings = dbSettings ?? throw new ArgumentNullException(nameof(dbSettings));

            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            if (!validator.TryValidate(dbSettings.Value, out var validationException))
                throw validationException;

            protector = dataProtectionProvider.CreateProtector(DataProtectionPurposeStrings.DbPassword);
        }

        #endregion

        /// <summary>
        /// 連線類型
        /// </summary>
        public DbType ConnectionType => GetConnectionType();

        /// <summary>
        /// 連線字串
        /// </summary>
        public string ConnectionString => GetConnectionString(ConnectionType);

        #region Private Method

        /// <summary>
        /// 取得連線類型
        /// </summary>
        /// <returns></returns>
        private DbType GetConnectionType()
        {
            Enum.TryParse(dbSettings.Value.ConnectionType, out DbType connectionType);

            return connectionType;
        }

        /// <summary>
        /// 取得連線字串
        /// </summary>
        /// <param name="connectionType">連線類型</param>
        /// <returns></returns>
        private string GetConnectionString(DbType connectionType)
        {
            var connectionString = connectionType switch
            {
                DbType.Oracle => GetOracleConnectionString(),
                DbType.MSSQLServer => GetMsSqlServerConnectionString(),
                _ => string.Empty,
            };

            return connectionString;
        }

        /// <summary>
        /// 取得 Oracle 連線字串
        /// </summary>
        /// <returns></returns>
        private string GetOracleConnectionString()
        {
            string connectionString;

            var serviceName = dbSettings.Value.Oracle.ServiceName;
            var server = dbSettings.Value.Server;
            var port = dbSettings.Value.Port;
            var userId = dbSettings.Value.UserId;
            var password = protector.Unprotect(dbSettings.Value.Password);

            connectionString = $"Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = {server})(PORT = {port})))(CONNECT_DATA =(SERVICE_NAME = {serviceName})));User Id={userId};Password={password};";

            return connectionString;
        }

        /// <summary>
        /// 取得 MSSQLServer 連線字串
        /// </summary>
        /// <returns></returns>
        private string GetMsSqlServerConnectionString()
        {
            string connectionString;

            var builder = new SqlConnectionStringBuilder
            {
                InitialCatalog = dbSettings.Value.MSSQLServer.InitialCatalog,
                DataSource = dbSettings.Value.Server,
                UserID = dbSettings.Value.UserId,
                Password = protector.Unprotect(dbSettings.Value.Password),
                MultipleActiveResultSets = true
            };

            connectionString = $"{builder}";

            return connectionString;
        }

        #endregion
    }
}
