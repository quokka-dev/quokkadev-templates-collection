using Autofac;
using QuokkaDev.Templates.DataAccess.Commands.Services;
using QuokkaDev.Templates.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace QuokkaDev.Templates.DataAccess.Commands.DI
{
    public class DataAccessCommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventDispatcher>().As<IDomainEventsDispatcher>().InstancePerLifetimeScope();
            builder.AddDbContext<CommandsDbContext>("Default");

            builder.RegisterAssemblyTypes(typeof(DataAccessCommandsModule).Assembly)
                .Where(t => t.IsInNamespace("QuokkaDev.Templates.DataAccess.Commands.Repositories") &&
                    t.Name != "BaseRepository" &&
                    t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }

    public static class ContainerBuilderExtensions
    {
        public static void AddDbContext<TContext>(this ContainerBuilder builder, string connectionString)
        where TContext : DbContext
        {
            builder.Register(componentContext =>
            {
                var serviceProvider = componentContext.Resolve<IServiceProvider>();
                var configuration = componentContext.Resolve<IConfiguration>();
                var dbContextOptions = new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions)
                    .UseApplicationServiceProvider(serviceProvider);

                bool useInMemoryDb = Boolean.Parse(configuration.GetSection("UseInMemoryDb").Value);

                if (!useInMemoryDb)
                {
                    optionsBuilder = optionsBuilder.UseSqlServer(configuration.GetConnectionString(connectionString),
                        serverOptions => serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null));
                }
                else
                {
                    optionsBuilder = optionsBuilder.UseInMemoryDatabase("myDataBase");
                }

                return optionsBuilder.Options;
            }).As<DbContextOptions<TContext>>().InstancePerLifetimeScope();


            builder.Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>().InstancePerLifetimeScope();

            builder.RegisterType<TContext>().AsSelf().InstancePerLifetimeScope();
        }
    }
}
