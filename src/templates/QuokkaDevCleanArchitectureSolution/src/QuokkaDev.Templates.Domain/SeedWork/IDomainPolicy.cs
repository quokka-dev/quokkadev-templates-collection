namespace QuokkaDev.Templates.Domain.SeedWork
{
    public interface IDomainPolicy
    {
        bool Evaluate();
        void ThrowIfNotPassed();
    }
}
