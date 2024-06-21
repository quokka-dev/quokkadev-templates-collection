using Asp.Versioning;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using QuokkaDev.Templates.Api.Infrastructure.ApiDocumentation;
using QuokkaDev.Templates.Api.Infrastructure.Filters;
using QuokkaDev.Templates.Api.Infrastructure.Middlewares;
using QuokkaDev.Templates.Api.Infrastructure.Services;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using Serilog;

namespace QuokkaDev.Templates.Api.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiDocumentationAndVersioning(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
            }).AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.FormatGroupName = (group, version) => $"{group} - {version}";
            });

            return services;
        }

        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            // Uncomment for configure authentication
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            // Uncomment for add Authorization
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("MyPolicy",
            //        policy =>
            //        {
            //            policy.AuthenticationSchemes = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
            //            policy.RequireAuthenticatedUser();
            //            // Add a new Requirement: 'policy.Requirements.Add(new MyCustomRequirement());'
            //        });
            //});

            return services;
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddResponseCompression();

            //Data protection
            services.AddDataProtection().UseCryptographicAlgorithms(
                new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                }
            );

            services.AddControllers().AddMvcOptions(opts =>
            {
                opts.Filters.Add(typeof(GeneralExceptionFilter));
                opts.Filters.Add(typeof(ValidateModelFilter));
            });

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserAccessor, HttpContextUserAccessor>();

            services.AddCors(options =>
            {
                options.AddPolicy("Default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .Build();
                });
            });

            services.AddScoped<CorrelationService>();

            return services;
        }

        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) =>
            {
                if (hostingContext.HostingEnvironment.IsDevelopment())
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                }
                else
                {
                    loggerConfiguration.WriteTo.ApplicationInsights(
                        services.GetRequiredService<TelemetryConfiguration>(),
                        TelemetryConverter.Traces
                    );
                }
            });

            builder.Services.AddApplicationInsightsTelemetry();

            return builder;
        }

        public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {
            var hcBuilder = services.AddHealthChecks();

            // Change Default with the name of the SQL Server connection string to check
            hcBuilder.AddSqlServer(
                    connectionString,
                    name: "DB-check",
                    tags: new string[] { "sqldb" });

            return services;
        }
    }
}
