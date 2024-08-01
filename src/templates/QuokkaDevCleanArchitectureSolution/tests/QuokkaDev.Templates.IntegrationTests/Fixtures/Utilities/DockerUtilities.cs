using Ductus.FluentDocker.Services.Extensions;
using Ductus.FluentDocker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Extensions;
using Microsoft.Data.SqlClient;
using Ductus.FluentDocker.Model.Builders;
using Ductus.FluentDocker.Builders;

namespace QuokkaDev.Templates.IntegrationTests.Fixtures.Utilities
{
    internal static class DockerUtilities
    {
        public static (IContainerService Container, int Port) SetupSqlServerContainer(string dbPassword, bool isContinousIntegrationEnvironment)
        {
            var builder = new Builder();
            ContainerBuilder cb = builder.UseContainer()
                .UseImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithEnvironment("ACCEPT_EULA=Y", $"SA_PASSWORD={dbPassword}")
                .WaitForPort("1433/tcp", 180000, "127.0.0.1");

            if (!isContinousIntegrationEnvironment)
            {
                cb.WithName("quokkadevtemplates_integration_sql")
                    .ExposePort(1433, 1433)
                    .ReuseIfExists()
                    .KeepRunning();
            }
            else
            {
                cb.ExposePort(1433);
            }

            var container = cb.Build().Start();

            var endpoint = container.ToHostExposedEndpoint("1433/tcp");

            WaitForConnectionAsync(DatabaseUtilities.GetMasterSqlConnectionString(endpoint.Port.ToString()), 30).Wait();

            return (container, endpoint.Port);
        }

        public static (IContainerService Container, int Port) SetupSmtpServerContainer(bool isContinousIntegrationEnvironment)
        {
            var builder = new Builder();
            ContainerBuilder cb =
            new Builder().UseContainer()
                .UseImage("rnwood/smtp4dev:latest")
                .WithLabel("latest")
                .WithName("quokkadevtemplates_integration_smtp")
                .ExposePort(25)
                .WaitForPort("25/tcp", 180000, "127.0.0.1")
                .RemoveVolumesOnDispose();

            if (!isContinousIntegrationEnvironment)
            {
                cb.ReuseIfExists()
                .ExposePort(5001, 80)
                .KeepRunning();
            }

            var container = cb.Build().Start();

            var endpoint = container.ToHostExposedEndpoint("25/tcp");
            return (container, endpoint.Port);
        }

        public static (IContainerService Container, int Port) SetupBlobContainer(bool isContinousIntegrationEnvironment)
        {
            var builder = new Builder();

            ContainerBuilder cb =
            new Builder().UseContainer()
                .UseImage("mcr.microsoft.com/azure-storage/azurite")
                .WithLabel("latest")
                .WithName("quokkadevtemplates_integration_blob")
                .ExposePort(10000, 10000)
                .ExposePort(10001, 10001)
                .ExposePort(10002, 10002)
                .WaitForPort("10000/tcp", 180000, "127.0.0.1")
                .RemoveVolumesOnDispose();

            if (!isContinousIntegrationEnvironment)
            {
                cb.ReuseIfExists()
                .KeepRunning();
            }

            var container = cb.Build().Start();

            var endpoint = container.ToHostExposedEndpoint("10000/tcp");
            return (container, endpoint.Port);
        }

        public static (IContainerService Container, int Port) SetupLogContainer(bool isContinousIntegrationEnvironment)
        {
            var builder = new Builder();
            ContainerBuilder cb =
            new Builder().UseContainer()
                .UseImage("datalust/seq:latest")
                .WithLabel("latest")
                .WithName("quokkadevtemplates_integration_seq")
                .ExposePort(5341, 5341)
                .WaitForPort("5341/tcp", 180000, "127.0.0.1")
                .WithEnvironment("ACCEPT_EULA=Y")
                .RemoveVolumesOnDispose();

            if (!isContinousIntegrationEnvironment)
            {
                cb.ReuseIfExists()
                .ExposePort(5002, 80)
                .KeepRunning();
            }

            var container = cb.Build().Start();

            var endpoint = container.ToHostExposedEndpoint("5341/tcp");
            return (container, endpoint.Port);
        }

        private static async Task WaitForConnectionAsync(string connectionString, int maxWaitInSeconds)
        {
            bool connected = false;
            int counter = 0;
            while (!connected && counter < maxWaitInSeconds)
            {
                try
                {
                    SqlConnection.ClearAllPools();

                    await using var connection = new SqlConnection(connectionString);
                    await connection.OpenAsync();

                    connected = true;
                    Console.WriteLine("Sql server connection open.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Sql server not ready ({ex.Message})...");
                    await Task.Delay(1.Seconds());
                }
                finally
                {
                    counter++;
                }
            }

            if (connected)
            {
                Console.WriteLine("Sql server ready!!!");
            }
            else
            {
                throw new Exception($"Cannot open connection to SQL Server in {maxWaitInSeconds} seconds");
            }
        }
    }
}
