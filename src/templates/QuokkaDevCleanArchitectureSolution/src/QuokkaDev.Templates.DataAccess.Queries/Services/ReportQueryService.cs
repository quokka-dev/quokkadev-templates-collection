using Dapper;
using QuokkaDev.Templates.Application.Services;
using System.Data.SqlClient;

namespace QuokkaDev.Templates.DataAccess.Queries.Services
{
    internal class ReportQueryService : IReportQueryService
    {
        private readonly string connectionString = string.Empty;

        public ReportQueryService(DataAccessQuerySettings settings)
        {
            connectionString = !string.IsNullOrWhiteSpace(settings.ConnectionString) ? settings.ConnectionString : throw new ArgumentNullException(nameof(settings.ConnectionString));
        }

        public async Task<dynamic> GetReportAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                   @"select * FROM Reporting r                        
                        WHERE r.Id=@id"
                        , new { id }
                    );

                return result;
            }
        }
    }
}

