using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace QuokkaDev.Templates.Api.Infrastructure.ApiDocumentation
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IConfiguration _configuration;

        public ConfigureSwaggerOptions(
            IApiVersionDescriptionProvider provider,
            IConfiguration configuration
        )
        {
            _provider = provider;
            _configuration = configuration;
        }

        /// <summary>
        /// Configure each API discovered for Swagger Invoiceation
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            // add swagger invoice for every API version discovered
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));

                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
            }

            //Uncomment the following line to add API authentication
            //AddApiAuthentication(options);
        }

        /// <summary>
        /// Configure Swagger Options. Inherited from the Interface
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        /// <summary>
        /// Create information about the version of the API
        /// </summary>
        /// <param name="description"></param>
        /// <returns>Information about the API</returns>
        private OpenApiInfo CreateVersionInfo(
                ApiVersionDescription desc)
        {
            var info = new OpenApiInfo()
            {
                Title = $"{desc.GroupName}(.NET 8) Web API",
                Version = desc.ApiVersion.ToString()
            };

            if (desc.IsDeprecated)
            {
                info.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";
            }

            return info;
        }

        private void AddApiAuthentication(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("define url..."),
                        TokenUrl = new Uri("define url..."),
                        // Add scopes here
                        Scopes = new Dictionary<string, string>
                        {
                            { "scope.name.write", "Write Scope Display Name" },
                            { "scope.name.read", "Read Scope Display Name" }
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "oauth2",
                        In = ParameterLocation.Header
                    },
                    // Add scopes here
                    new List<string> { "scope.name.write", "scope.name.read" }
                }
            });
        }
    }
}
