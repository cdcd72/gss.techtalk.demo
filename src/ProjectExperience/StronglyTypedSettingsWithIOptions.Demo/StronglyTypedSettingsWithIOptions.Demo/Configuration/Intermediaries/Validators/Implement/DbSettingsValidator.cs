using System;
using System.Collections.Generic;
using System.Linq;

namespace StronglyTypedSettingsWithIOptions.Demo.Configuration
{
    /// <summary>
    /// 資料庫設定驗證器
    /// </summary>
    public class DbSettingsValidator : ISettingsValidator<DbSettings>
    {
        /// <summary>
        /// 嘗試驗證 appsettings.json 內 DbSettings 區段
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="validationExceptions"></param>
        /// <returns></returns>
        public bool TryValidate(DbSettings settings, out AggregateException validationExceptions)
        {
            var exceptions = new List<Exception>();

            #region 自定義驗證區段

            // 若 DbSettings:ConnectionType 沒有特別輸入
            if (string.IsNullOrWhiteSpace(settings.ConnectionType))
                exceptions.Add(new ArgumentNullException(nameof(settings.ConnectionType)));

            #endregion

            validationExceptions = new AggregateException(exceptions);

            return !exceptions.Any();
        }
    }
}
