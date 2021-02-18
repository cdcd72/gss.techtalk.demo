using Microsoft.Extensions.DependencyInjection;
using System;

namespace GenericTypeAsAFactory.Demo.Utility
{
    /// <summary>
    /// 泛型服務工廠
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public class ServiceFactory<TService> : IServiceFactory<TService>
    {
        public TService Service { get; }

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            // 1. IServiceFactory<TService> 可從 DI 容器取得已註冊之 TService
            // 2. 若 TService 不存在於 DI 容器，則可以創建 TService 實例出來
            Service = (TService)serviceProvider.GetService(typeof(TService)) ?? ActivatorUtilities.CreateInstance<TService>(serviceProvider);
        }
    }
}
