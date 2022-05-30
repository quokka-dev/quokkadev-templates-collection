using Autofac;
using QuokkaDev.Templates.Application.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.PlatformAbstractions;
using System.Reflection;

namespace QuokkaDev.Templates.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            var hcBuilder = services.AddHealthChecks();

            // Change Default with the name of the SQL Server connection string to check
            hcBuilder.AddSqlServer(
                    Configuration.GetConnectionString("Default"),
                    name: "DB-check",
                    tags: new string[] { "sqldb" });

            // Uncomment for add Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MyPolicy",
                    policy =>
                    {
                        policy.AuthenticationSchemes = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
                        policy.RequireAuthenticatedUser();
                        // Add a new Requirement: 'policy.Requirements.Add(new MyCustomRequirement());'
                    });
            });

            services.AddApplicationServices();
            services.AddHttpContextAccessor();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var applicationPath = PlatformServices.Default.Application.ApplicationBasePath;
            var assemblies = Directory.GetFiles(applicationPath, "*QuokkaDev.Templates.*.dll").Select(dll => Assembly.LoadFile(dll)).ToArray();
            builder.RegisterAssemblyModules(assemblies);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.InjectJavascript("/ui.js", "text/javascript"));
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true
                });
            });
        }
    }
}
