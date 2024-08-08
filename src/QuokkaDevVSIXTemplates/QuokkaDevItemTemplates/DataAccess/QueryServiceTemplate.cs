namespace $rootnamespace$
{
    internal class $safeitemname$ : I$safeitemname$
    {
        private readonly string connectionString = string.Empty;

public $safeitemname$(DataAccessQuerySettings settings)
                {
                    connectionString = !string.IsNullOrWhiteSpace(settings.ConnectionString) ? settings.ConnectionString : throw new ArgumentNullException(nameof(settings.ConnectionString));
}

/// <summary>
/// Implement I$safeitemname$
/// </summary>
public async Task<dynamic> GetAsync(int id)
{
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();

        var result = await connection.QueryAsync<dynamic>(
           @"select * FROM MyTable t                        
                                WHERE t.Id=@id"
                , new { id }
            );

        return result;
    }
}
    }
}
