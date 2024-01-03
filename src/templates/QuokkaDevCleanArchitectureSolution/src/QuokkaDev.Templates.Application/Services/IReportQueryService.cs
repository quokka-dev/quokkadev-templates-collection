namespace QuokkaDev.Templates.Application.Services
{
    public interface IReportQueryService : IQueryService
    {
        Task<dynamic> GetReportAsync(int id);
    }
}
