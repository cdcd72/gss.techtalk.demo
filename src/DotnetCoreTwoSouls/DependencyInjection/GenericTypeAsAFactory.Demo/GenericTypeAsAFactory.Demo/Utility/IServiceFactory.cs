namespace GenericTypeAsAFactory.Demo.Utility
{
    /// <summary>
    /// 泛型服務工廠介面
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public interface IServiceFactory<TService>
    {
        TService Service { get; }
    }
}
