using Autofac;
using QuokkaDev.Templates.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.DataAccess.Queries.DI
{
    public class DataAccessQueriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register all Query Service
            builder.RegisterAssemblyTypes(typeof(DataAccessQueriesModule).Assembly)
                .Where(t => t.IsInNamespace("QuokkaDev.Templates.DataAccess.Queries") &&
                    t.IsAssignableTo(typeof(IQueryService)) &&
                    t.Name.EndsWith("QueryService"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Register(componentContext =>
            {
                var configuration = componentContext.Resolve<IConfiguration>();

                string connectionString = configuration.GetConnectionString("Default");
                DataAccessQuerySettings settings = new DataAccessQuerySettings(connectionString);

                return settings;

            }).As<DataAccessQuerySettings>().SingleInstance();
        }
    }
}
