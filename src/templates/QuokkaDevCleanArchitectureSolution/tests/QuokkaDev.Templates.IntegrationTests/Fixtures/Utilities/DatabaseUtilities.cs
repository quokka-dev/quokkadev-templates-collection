using Microsoft.EntityFrameworkCore;

namespace QuokkaDev.Templates.IntegrationTests.Fixtures.Utilities
{
    /// <summary>
    /// Utilities for database setup
    /// </summary>
    internal static class DatabaseUtilities
    {
        /// <summary>
        /// The database password used for integration tests
        /// It is used only in integration tests made with temp docker container, no need to protect this password
        /// </summary>
        public const string DB_PASSWORD = "MyS@P@55w0rd!";

        /// <summary>
        /// Get the connection string for the application database
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string GetApplicationSqlConnectionString(string port)
        {
            return GetSqlConnectionString(port, "LocalIntegrationDatabase");
        }

        /// <summary>
        /// Get the connection string for master database.
        /// It is used in container setup for check the container is ready to accept connection.
        /// In container setup the applicative database does not exists, so master is used to check connection
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string GetMasterSqlConnectionString(string port)
        {
            return GetSqlConnectionString(port, "master");
        }

        private static string GetSqlConnectionString(string port, string databaseName)
        {
            return $"server=localhost,{port};" +
                $"Initial Catalog={databaseName};" +
                "Encrypt=false;" +
                "User ID=SA;" +
                $"Password={DB_PASSWORD};";
        }
    }
}
