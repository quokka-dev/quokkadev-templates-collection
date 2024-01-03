namespace $rootnamespace$
{
    /// <summary>
    /// Specific key for Credit notes, based on a Guid
    /// </summary>
    public sealed record $safeitemname$ : EntityKey<Guid>
    {
        private $safeitemname$(Guid g) : base(g)
        {
        }

/// <summary>
/// Create new CreditNoteId
/// </summary>
/// <returns></returns>
public static $safeitemname$ NewId()
        {
            return new $safeitemname$(GuidProvider.NewGuid());
        }

        public static implicit operator Guid($safeitemname$ cid) => cid.Value;
public static explicit operator $safeitemname$(Guid g) => new $safeitemname$(g);
    }
}
