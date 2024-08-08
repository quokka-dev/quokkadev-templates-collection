using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace QuokkaDev.Templates.IntegrationTests.Fixtures.Utilities
{
    internal static class ConfigurationUtilities
    {
        public static IConfiguration GetConfiguration(bool isContinousIntegrationEnvironment, bool addInMemoryValues = false, string dockerSqlConnectionString = "", string dockerBlobConnectionString = "")
        {
            var builder = new ConfigurationBuilder();

            SetConfiguration(builder, isContinousIntegrationEnvironment, addInMemoryValues, dockerSqlConnectionString, dockerBlobConnectionString);

            return builder.Build();
        }

        public static void SetConfiguration(IConfigurationBuilder builder, bool isContinousIntegrationEnvironment,
            bool addInMemoryValues = false, string dockerSqlConnectionString = "", string dockerBlobConnectionString = "")
        {
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!isContinousIntegrationEnvironment)
            {
                builder.AddUserSecrets(assembly: typeof(ConfigurationUtilities).Assembly, optional: true, reloadOnChange: true);
            }

            if (addInMemoryValues)
            {
                builder.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "ConnectionStrings:Default", dockerSqlConnectionString },
                    { "BlobStorageConnectionString", dockerBlobConnectionString }
                });
            }

            builder.AddEnvironmentVariables();
        }


        public static IConfiguration GetBaseConfiguration()
        {
            var builder = new ConfigurationBuilder();

            builder.AddEnvironmentVariables();

            return builder.Build();
        }

        public static bool IsContinousIntegrationEnvironment()
        {
            var tempConfig = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            return tempConfig.GetValue("CONTINOUS_INTEGRATION_ENV", false);
        }
    }
}
