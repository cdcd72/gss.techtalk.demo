namespace LazyInitializationOfServices.Demo.Utility
{
    /// <summary>
    /// Lazy Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILazy<T>
    {
        T Value { get; }
    }
}
