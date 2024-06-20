namespace $rootnamespace$
{
    /// <summary>
    /// Specific key based on a Guid
    /// </summary>
    public struct $safeitemname$
    {
        /// <summary>
        /// Inner value of the key
        /// </summary>
        public Guid Value { get; }

        private $safeitemname$(Guid g) => Value = g;        

        /// <summary>
        /// Create new $safeitemname$
        /// </summary>
        /// <returns></returns>
        public static $safeitemname$ NewId()
        {
            return new $safeitemname$(Guid.NewGuid());
        }

        public static implicit operator Guid($safeitemname$ key) => key.Value;
        public static explicit operator $safeitemname$(Guid g) => new $safeitemname$(g);
    }
}
