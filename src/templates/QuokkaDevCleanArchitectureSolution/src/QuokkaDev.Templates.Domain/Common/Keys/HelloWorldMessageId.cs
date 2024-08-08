namespace QuokkaDev.Templates.Domain.Common.Keys
{
    /// <summary>
    /// Specific key based on a Guid
    /// </summary>
    public struct HelloWorldMessageId
    {
        /// <summary>
        /// Inner value of the key
        /// </summary>
        public Guid Value { get; }

        private HelloWorldMessageId(Guid g) => Value = g;

        /// <summary>
        /// Create new HelloWorldMessageId
        /// </summary>
        /// <returns></returns>
        public static HelloWorldMessageId NewId()
        {
            return new HelloWorldMessageId(Guid.NewGuid());
        }

        public static implicit operator Guid(HelloWorldMessageId key) => key.Value;
        public static explicit operator HelloWorldMessageId(Guid g) => new HelloWorldMessageId(g);
    }
}
