using Ductus.FluentDocker.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuokkaDev.Templates.Application;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.IntegrationTests.Fixtures.Utilities;
using QuokkaDev.Templates.Persistence.Ef;
using QuokkaDev.Templates.Persistence.Ef.Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace QuokkaDev.Templates.IntegrationTests.Fixtures
{
    /// <summary>
    /// Fixture for using Application layer and persistence
    /// </summary>
    public sealed class ApplicationFixture<TEntryPoint> : WebApplicationFactory<Program>, IDisposable
        where TEntryPoint : Program
    {
        private static readonly object _lock = new object();
        private readonly List<IServiceScope> scopes = new List<IServiceScope>();

        internal IContainerService SqlContainer { get; private set; }
        internal IContainerService SeqContainer { get; private set; }
        //internal IContainerService SmtpContainer { get; private set; }
        //internal IContainerService BlobContainer { get; private set; }

        internal IServiceProvider? ServiceProvider { get; private set; } = null;
        internal IConfiguration Configuration { get; private set; }
        internal ApplicationDbContext ApplicationDbContext { get; private set; }



        internal int SmtpServerPort { get; private set; }
        internal string DockerSqlConnectionString { get; private set; } = "";
        internal string DockerBlobConnectionString { get; private set; } = "";

        public readonly bool IsRunLocalSqlServer = false;
        public readonly bool IsContinousIntegrationEnvironment;

        [SetsRequiredMembers]
        public ApplicationFixture()
        {
            IsContinousIntegrationEnvironment = ConfigurationUtilities.IsContinousIntegrationEnvironment();

            Console.WriteLine($"Is continous integration environment? {IsContinousIntegrationEnvironment}");

            StartDockerInfrastructure();

            Configuration = ConfigurationUtilities.GetConfiguration(
                IsContinousIntegrationEnvironment,
                true,
                DockerSqlConnectionString,
                DockerBlobConnectionString
            );

            CreateClient();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextOptionDescriptors = services.Where(s => s.ServiceType.IsGenericType &&
                    s.ServiceType.GetGenericTypeDefinition().IsAssignableTo(typeof(DbContextOptions<>)))
                    .ToList();

                foreach (var dbContextOptionDescriptor in dbContextOptionDescriptors)
                {
                    services.Remove(dbContextOptionDescriptor);
                }

                var applicationDbContextDescriptors = services.Where(s => s.ServiceType.IsAssignableTo(typeof(IUnitOfWork))).ToList();
                foreach (var applicationDbContextDescriptor in applicationDbContextDescriptors)
                {
                    services.Remove(applicationDbContextDescriptor);
                }

                services.AddApplicationServices(Configuration)
                    .AutoRegisterApplicationservicesServices()
                    .AddCommandAndQueries()
                    .AddAutoMapper()
                    .AddDomainEventsDispatchment()
                    .RegisterBatches();


                string connectionString = "" + Configuration.GetConnectionString("Default");

                services.AddDataAccess(connectionString)
                    .AddSpannedTransactions();
                //services.AddQueriesDataAccess(connectionString);

                services.AddSingleton<ICurrentUserAccessor>(MockUtilities.GetICurrentUserAccessorMock().Object);


                var currentUserAccessorDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(ICurrentUserAccessor));
                if (currentUserAccessorDescriptor != null)
                {
                    services.Remove(currentUserAccessorDescriptor);
                }
                services.AddSingleton<ICurrentUserAccessor>(MockUtilities.GetICurrentUserAccessorMock().Object);


                if (ServiceProvider == null)
                {
                    lock (_lock)
                    {
                        if (ServiceProvider == null) //Double check lock
                        {
                            ServiceProvider = services.BuildServiceProvider();
                            ApplicationDbContext = (ApplicationDbContext)ServiceProvider.GetRequiredService<IUnitOfWork>();
                            InitializeDatabases();
                        }
                    }
                }
            });
        }

        internal ApplicationDbContext GetScopedApplicationDbContext()
        {
            var scope = Services.CreateScope();
            scopes.Add(scope);

            return (ApplicationDbContext)scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        }



        public void Dispose()
        {
            ApplicationDbContext.Dispose();
            if (IsContinousIntegrationEnvironment)
            {
                SqlContainer.Dispose();
                SeqContainer.Dispose();
                //SmtpContainer.Dispose();
                //BlobContainer.Dispose();
            }

            foreach (var scope in scopes)
            {
                try
                {
                    scope.Dispose();
                }
                catch (Exception)
                {
                }
            }
        }

        private void StartDockerInfrastructure()
        {
            (SqlContainer, int sqlServerPort) = DockerUtilities.SetupSqlServerContainer(DatabaseUtilities.DB_PASSWORD, IsContinousIntegrationEnvironment);
            (SeqContainer, int logPort) = DockerUtilities.SetupLogContainer(IsContinousIntegrationEnvironment);

            //(BlobContainer, int blobPort) = DockerUtilities.SetupBlobContainer(IsContinousIntegrationEnvironment);
            //(SmtpContainer, SmtpServerPort) = DockerUtilities.SetupSmtpServerContainer(IsContinousIntegrationEnvironment);

            DockerSqlConnectionString = DatabaseUtilities.GetApplicationSqlConnectionString(sqlServerPort.ToString());
            //DockerBlobConnectionString = BlobUtilities.GetBlobConnectionString(blobPort.ToString());
        }

        private void InitializeDatabases()
        {
            ApplicationDbContext.Database.EnsureDeleted();
            ApplicationDbContext.Database.Migrate();

            DatabaseTestDataSeeder.SeedAsync<TEntryPoint>(ApplicationDbContext, this).GetAwaiter().GetResult();
        }
    }

    [CollectionDefinition("ApplicationFixture")]
    public class ApplicationFixtureCollection : ICollectionFixture<ApplicationFixture<Program>>
    {
    }
}
