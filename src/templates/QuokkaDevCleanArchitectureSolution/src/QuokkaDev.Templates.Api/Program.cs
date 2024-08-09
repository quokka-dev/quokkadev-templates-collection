using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using QuokkaDev.SecurityHeaders;
using QuokkaDev.Templates.Api.Infrastructure;
using QuokkaDev.Templates.Api.Infrastructure.HostedServices;
using QuokkaDev.Templates.Api.Infrastructure.Middlewares;
using QuokkaDev.Templates.Application;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Persistence.Ef;
using QuokkaDev.Templates.Query.Dapper;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

string connectionString = configuration.GetConnectionString("Default") ?? throw new Exception("Connection string 'Default' is not defined.");

//Settings
builder.Configuration.SetBasePath(environment.ContentRootPath)
        .AddJsonFile($"appsettings.json", optional: false)
        .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddUserSecrets(assembly: Assembly.GetExecutingAssembly(), optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();

//Logging
builder.AddLogging(configuration);

builder.Services.AddApplicationHealthChecks(configuration, connectionString);

builder.Services.AddApiServices(configuration);
builder.Services.AddApiDocumentationAndVersioning();

builder.Services.AddSingleton<BatchHostedService>();
builder.Services.AddSingleton<IHostedService, BatchHostedService>(
                   serviceProvider => serviceProvider.GetRequiredService<BatchHostedService>());

builder.Services.AddDataAccess(connectionString)
    .AddSpannedTransactions();
builder.Services.AddQueriesDataAccess(connectionString);

builder.Services.AddApplicationServices(configuration)
    .AutoRegisterApplicationservicesServices()
    .AddCommandAndQueries()
    .AddAutoMapper()
    .AddDomainEventsDispatchment()
    .RegisterBatches();

// Configure the HTTP request pipeline.
var app = builder.Build();

app.UseResponseCompression();

app.UseMiddleware<CorrelationMiddleware>(new CorrelationOptions
{
    TryToUseRequestHeader = true,
    ValidRequestHeaders = new string[] { "X-Correlation-Id" },
    EnrichLog = true,
    LogPropertyName = "CorrelationId",
    WriteCorrelationIDToResponse = true,
    DefaultResponseHeaderName = "X-Correlation-Id"
});

app.UseCors("Default");
app.UseSecurityHeaders(configuration);

if (environment.IsDevelopment())
{
    app.UseSwagger();

    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwaggerUI(c =>
    {
        c.InjectJavascript("/ui.js", "text/javascript");
        c.InjectStylesheet("/style.css");
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }

        c.DefaultModelsExpandDepth(0);
        c.DocumentTitle = "QuokkaDev API";

        // Uncomment to configure Swagger UI for using oauth
        //c.OAuthClientId(configuration["AzureAdB2C:ClientId"]);
        //c.OAuthScopes("scope.name.write", "scope.name.read");
        //c.OAuthAppName("QuokkaDev API - Swagger");
        //c.EnablePersistAuthorization();
        //c.OAuthUsePkce();
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

var cacheMaxAgeOneWeek = (60 * 60 * 24 * 365).ToString();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
             "Cache-Control", $"public, max-age={cacheMaxAgeOneWeek}");
    }
});

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true
});

// Uncomment to enable database bootstrap
//if (!configuration.GetValue<bool>("CONTINOUS_INTEGRATION_ENV", false))
//{
//    using var scope = app.Services.CreateScope();
//    using var dbBootstrap = scope.ServiceProvider.GetRequiredService<IDataAccessBootstrapper>();
//    {
//        dbBootstrap.BootstrapAsync().Wait();
//    }    
//}

app.Run();

public partial class Program { }