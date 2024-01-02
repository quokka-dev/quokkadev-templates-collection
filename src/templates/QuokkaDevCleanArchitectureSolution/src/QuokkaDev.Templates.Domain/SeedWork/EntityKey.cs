namespace QuokkaDev.Templates.Domain.SeedWork
{
    /// <summary>
    /// Base type used as entity key.
    /// </summary>
    /// <typeparam name="T">The base type of the key</typeparam>
    public abstract record EntityKey<T> : IDefaultValue where T : notnull
    {
        /// <summary>
        /// Inner value of the key
        /// </summary>
        public T Value { get; private set; }

        protected EntityKey(T key) => Value = key;

        public virtual object? GetDefaultValue()
        {
            return this with { Value = default! };
        }
    }

    public interface IDefaultValue
    {
        /// <summary>
        /// Return the default value of a type
        /// </summary>
        /// <returns>The default value</returns>
        object? GetDefaultValue();
    }
}
