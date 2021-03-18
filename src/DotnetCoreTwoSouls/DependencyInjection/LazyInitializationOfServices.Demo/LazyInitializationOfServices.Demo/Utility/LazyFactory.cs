using System;
using Microsoft.Extensions.DependencyInjection;

namespace LazyInitializationOfServices.Demo.Utility
{
    /// <summary>
    /// Lazy Initialization Factory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LazyFactory<T> : ILazyFactory<T>
    {
        private readonly Lazy<T> lazy;

        public LazyFactory(IServiceProvider serviceProvider) =>
            // 延遲初始實例
            // PS. GetRequiredService() 若從 DI 容器取不到已註冊服務會直接拋出例外...
            lazy = new Lazy<T>(() => serviceProvider.GetRequiredService<T>());

        public T Value => lazy.Value;
    }
}
