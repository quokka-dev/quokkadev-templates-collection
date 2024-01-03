namespace QuokkaDev.Templates.Query.Dapper
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
