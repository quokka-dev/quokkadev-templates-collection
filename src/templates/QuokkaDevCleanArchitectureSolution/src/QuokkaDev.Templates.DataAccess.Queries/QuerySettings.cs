namespace QuokkaDev.Templates.DataAccess.Queries
{
    public class QuerySettings
    {
        public string ConnectionString { get; }

        public QuerySettings(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}
