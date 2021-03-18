namespace LazyInitializationOfServices.Demo.Utility
{
    /// <summary>
    /// Lazy Initialization Factory Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILazyFactory<T>
    {
        T Value { get; }
    }
}
