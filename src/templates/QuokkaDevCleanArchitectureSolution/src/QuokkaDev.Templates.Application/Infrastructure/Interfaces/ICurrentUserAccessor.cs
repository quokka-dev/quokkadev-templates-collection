namespace QuokkaDev.Templates.Application.Infrastructure.Interfaces
{
    public interface ICurrentUserAccessor
    {
        Task<string> GetCurrentUserNameAsync();
    }
}
