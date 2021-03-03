using System;
using Microsoft.Extensions.DependencyInjection;

namespace CreatingInstancesFromIServiceProvider.Demo.Utility
{
    /// <summary>
    /// 相依工廠 (Note: Disposable instances created with this factory will not be disposed by the DI container.)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DependencyFactory<T>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ObjectFactory factory;

        public DependencyFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.factory = ActivatorUtilities.CreateFactory(typeof(T), Type.EmptyTypes);
        }

        /// <summary>
        /// 取得實例
        /// </summary>
        /// <returns></returns>
        public T GetInstance() => (T)factory(serviceProvider, null);
    }
}
