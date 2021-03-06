using System;

namespace JwtAuth.Demo.Configuration
{
    /// <summary>
    /// 設定驗證器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISettingsValidator<TSettings>
    {
        /// <summary>
        /// 嘗試驗證 appsettings.json 內 DbSettings 區段
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="validationExceptions"></param>
        /// <returns></returns>
        bool TryValidate(TSettings settings, out AggregateException validationExceptions);
    }
}
