# QuokkaDev Template Collection.

QuokkaDev Template collection is a set of dotnet templates, C# project items and code snippet that can help a developer to write project and files following a shared set of best practices.

It is composed by two main artifacts.

The first artifact is a NuGet package containing two different project templates, one for scaffolding a generic NuGet package solution and one for scaffolding a web API solution based on Clean Architecture styles(see more about about Clean Architecture [here](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)).

The second artifact is a Visual Studio 2022 extension (VSIX) containing some item templates (they can be used in the web API solution in the NuGet Package) and some code snippet.

# NuGet Package installation

There are three way to install the NuGet package.

The first (recommended) is directly from [nuget.org](https://www.nuget.org/), using the command:

    dotnet new install QuokkaDev.Templates.Collection

The second is to download it from the [release section of this repository](https://github.com/quokka-dev/quokkadev-templates-collection/releases) and then install it manually using the command:

    dotnet new install path/to/my/local/package/QuokkaDev.Templates.Collection.x.x.x.nupkg

The last is to clone the repository, pack the template package and then install it manually using. For pack the NuGet package use the following command from the root of the repository:

    dotnet pack src/QuokkaDev.Templates.Collection.csproj -c Release /p:NoDefaultExcludes=true /p:Version=1.0.0

And then install it using the command:

    dotnet new install path/to/my/local/package/QuokkaDev.Templates.Collection.1.0.0.nupkg

# Update of the NuGet Package

If you install the package using the first way, you can easily check for updates using the command:

    dotnet new update --check-only

If a new version of the package is available you can intall it with:

    dotnet new install QuokkaDev.Templates.Collection.x.x.x

# Visual Studio 2022 Extension installation

You can install the Visual Studio 2022 extension in four way.

The first, recommended is directly from Visual Studio, from the `Extensions --> Manage Extensions` menu, searching 'QuokkaDev Templates' from Online section.

The second is from [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=QuokkaDev.quokkadevvsixtemplates). Download the extension and the install it with double click.

The third is to download it from the [release section of this repository](https://github.com/quokka-dev/quokkadev-templates-collection/releases) and then install it manually with double click.

The last way is to clone this repository, build the project QuokkaDevVSIXTemplates and then install the extension with adouble click from the bin folder

# Project Templates
The NuGet package contains two different templates

## Quokka Dev Clean Architecture Solution
Quokka Dev Clean Architecture Solution is a solution template for build a Asp.Net REST API project (currently in .NET 8.0) based on [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) principles. It furthermore uses [CQRS](https://martinfowler.com/bliki/CQRS.html) design pattern for having separate write/read models. In the domain layer some DDD ([Domain Driven Design](https://it.wikipedia.org/wiki/Domain-driven_design)) principles are implemented. The template come with this out-of-the-box features:

- .editorconfig file for C# style rules
- .gitignore file configured for .NET solution
- Some GitHub workflows examples for Pull Requests, Release, Publish and Nightly Build
- README file
- three different test projects, using xUnit, Moq and FluentAssertions
    - An example of Unit Tests
    - A complete suite of Architectural Tests
    - An example of Integration Tests
- Dependency Injection implemented with AutoFac
- Automatic loading of dependencies during application startup (currently infrastructure projects are directly referenced inside Api project because Visual Studio don't copy automatically non-referenced projects, but in future a script for this task will be realized)
- Two example of Data Access infrastructure projects
    - Using EntityFramework for write model (Commands in CQRS)
    - Using Dapper for read model (Queries in CQRS)
- Custom implementation of CQRS pattern based on [QuokkaDev.CQRS](https://www.nuget.org/packages/QuokkaDev.Cqrs/) and [QuokkaDev.CQRS.Decorators](https://www.nuget.org/packages/QuokkaDev.Cqrs.Decorators/) packages (both the packages are open and availables on GitHub)
- Validation based on FluentValidation
- Integration with AutoMapper
- API docs with Swagger
- Health Checks
- Centralized management of the exceptions
- API Versioning
- Log with Serilog

## Quokka Dev Package Solution
Quokka Dev Package Solution is a solution template for build a .Net NuGet package (currently in .NET 6.0). The template come with this out-of-the-box features:

- .editorconfig file for C# style rules
- .gitignore file configured for .NET solution
- base configuration of the .csproj file for NuGet Package generation
- Some GitHub workflows examples for Pull Requests, Release, Publish and Nightly Build
- README file
- Unit Test projects, using xUnit, Moq and FluentAssertions

# Item Templates
The Visual Studio 2022 extension come with some item templates out-of-the-box. The templates are written for using in the `Quokka Dev Clean Architecture Solution`. Following templates are availables:

### CQRS Command
Create a couple of classes `Command` and `CommandResult` to use with the CQRS pattern. In the item name don't use 'Command' suffix, it will be added automatically by the template
```csharp
namespace ConsoleApp1
{
    /// <summary>
    /// A command request
    /// </summary>
    public class MyCustomCommand
    {

    }

    /// <summary>
    /// A command result
    /// </summary>
    public class MyCustomCommandResult
    {

    }
}
```

### CQRS Command and Handler
Create a set of classes `Command`, `CommandResult` and `CommandHandler` to use with the CQRS pattern. In the item name don't use 'Command' suffix, it will be added automatically by the template
```csharp
using AutoMapper;
using Microsoft.Extensions.Logging;
using QuokkaDev.CQRS;

namespace ConsoleApp1
{
    /// <summary>
    /// A command request
    /// </summary>
    public class MyCustomCommand
    {

    }

    /// <summary>
    /// A command result
    /// </summary>
    public class MyCustomCommandResult
    {

    }

    /// <summary>
    ///  A command handler for MyCustomCommand
    /// </summary>
    public class MyCustomCommandHandler : ICommandHandler<MyCustomCommand, MyCustomCommandResult>
    {
        private readonly ILogger<MyCustomCommandHandler> logger;
        private readonly IMapper mapper;

        public MyCustomCommandHandler(ILogger<MyCustomCommandHandler> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<MyCustomCommandResult> Handle(MyCustomCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

```
### CQRS CommandHandler
Create a class `CommandHandler` to use with the CQRS pattern. In the item name don't use 'Command' suffix, it will be added automatically by the template
```csharp
using AutoMapper;
using Microsoft.Extensions.Logging;
using QuokkaDev.CQRS;

namespace ConsoleApp1
{
    /// <summary>
    ///  A command handler for MyCustomCommand
    /// </summary>
    public class MyCustomCommandHandler : ICommandHandler<MyCustomCommand, MyCustomCommandResult>
    {
        private readonly ILogger<MyCustomCommandHandler> logger;
        private readonly IMapper mapper;

        public MyCustomCommandHandler(ILogger<MyCustomCommandHandler> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<MyCustomCommandResult> Handle(MyCustomCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
```
### CQRS CommandValidator
Create a class `CommandValidator` to use with the CQRS pattern. In the item name don't use 'Command' suffix, it will be added automatically by the template
```csharp
using FluentValidation;

namespace ConsoleApp1
{
    /// <summary>
    /// A Fluent Validation Validator for command MyCustomCommand
    /// </summary>
    public class MyCustomCommandValidator : AbstractValidator<MyCustomCommand>
    {
        public MyCustomCommandValidator()
        {
            //RuleFor(x => x.MyProperty).NotEmpty();
        }
    }
}
```
### CQRS Query
Create a couple of classes `Query` and `QueryResult` to use with the CQRS pattern. In the item name don't use 'Query' suffix, it will be added automatically by the template
```csharp
namespace ConsoleApp1
{
    /// <summary>
    /// A query request
    /// </summary>
    public class MyCustomQuery
    {

    }

    /// <summary>
    /// A query result
    /// </summary>
    public class MyCustomQueryResult
    {

    }
}
```
### CQRS Query and Handler
Create a set of classes `Query`, `QueryResult` and `QueryHandler` to use with the CQRS pattern. In the item name don't use 'Query' suffix, it will be added automatically by the template
```csharp
using AutoMapper;
using Microsoft.Extensions.Logging;
using QuokkaDev.CQRS;

namespace ConsoleApp1
{
    /// <summary>
    /// A query request
    /// </summary>
    public class MyCustomQuery
    {

    }

    /// <summary>
    /// A query result
    /// </summary>
    public class MyCustomQueryResult
    {

    }

    /// <summary>
    ///  A query handler for MyCustomQuery
    /// </summary>
    public class MyCustomQueryHandler : IQueryHandler<MyCustomQuery, MyCustomQueryResult>
    {
        private readonly ILogger<MyCustomQueryHandler> logger;
        private readonly IMapper mapper;

        public MyCustomQueryHandler(ILogger<MyCustomQueryHandler> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<MyCustomQueryResult> Handle(MyCustomQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
```
### CQRS QueryHandler
Create a class `QueryHandler` to use with the CQRS pattern. In the item name don't use 'Query' suffix, it will be added automatically by the template
```csharp
using AutoMapper;
using Microsoft.Extensions.Logging;
using QuokkaDev.CQRS;

namespace ConsoleApp1
{
    /// <summary>
    ///  A query handler for MyCustomQuery
    /// </summary>
    public class MyCustomQueryHandler : IQueryHandler<MyCustomQuery, MyCustomQueryResult>
    {
        private readonly ILogger<MyCustomQueryHandler> logger;
        private readonly IMapper mapper;

        public MyCustomQueryHandler(ILogger<MyCustomQueryHandler> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<MyCustomQueryResult> Handle(MyCustomQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
```
### CQRS QueryValidator
Create a class `QueryValidator` to use with the CQRS pattern. In the item name don't use 'Query' suffix, it will be added automatically by the template
```csharp
using FluentValidation;

namespace ConsoleApp1
{
    /// <summary>
    /// A Fluent Validation Validator for query MyCustomQuery
    /// </summary>
    public class MyCustomQueryValidator : AbstractValidator<MyCustomQuery>
    {
        public MyCustomQueryValidator()
        {
            //RuleFor(x => x.MyProperty).NotEmpty();
        }
    }
}
```
### CQRS Use Case
Create the scaffolding for an entire Use Case implemented with CQRS. Creates a folder structure for Commands, Queries, Handlers and Validators with an example file for every folder.
### Query Service
Create a base `QueryService` with an example query in Dapper. In the item name don't use 'QueryService' suffix, it will be added automatically by the template.
```csharp
using Dapper;
using QuokkaDev.Templates.Application.Services;
using System.Data.SqlClient;

namespace QuokkaDev.Templates.DataAccess.Queries
{
    internal class MyCustomQueryService : IMyCustomQueryService
    {
        private readonly string connectionString = string.Empty;

        public MyCustomQueryService(DataAccessQuerySettings settings)
        {
            connectionString = !string.IsNullOrWhiteSpace(settings.ConnectionString) ? settings.ConnectionString : throw new ArgumentNullException(nameof(settings.ConnectionString));
        }

        /// <summary>
        /// Implement IMyCustomQueryService
        /// </summary>
        public async Task<dynamic> GetAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                   @"select * FROM MyTable t
                        WHERE t.Id=@id"
                        , new { id }
                    );

                return result;
            }
        }
    }
}

```
### Repository
Create a base `Repository` with an EntityFramework DbContext. In the item name don't use 'Repository' suffix, it will be added automatically by the template.
```csharp
namespace ConsoleApp1
{
    internal class MyCustomRepository : BaseRepository<MyCustom>, IMyCustomRepository
    {
        public MyCustomRepository(CommandsDbContext context) : base(context)
        {
        }
    }
}
```
### Action Filter
Create an action filter implementing `IAsyncActionFilter`.
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WebApiApp1
{
    /// <summary>
    /// MyCustomActionFilter action filter
    /// </summary>
    public class MyCustomActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<MyCustomActionFilter> logger;

        public MyCustomActionFilter(ILogger<MyCustomActionFilter> logger)
        {
            this.logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (true)
            {
                await next();
            }
            else
            {
                context.Result = new ObjectResult("");
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            }
        }
    }
}
```
### Exception Filter
Create an exception filter implementing `IAsyncExceptionFilter`.
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiApp1
{
    /// <summary>
    /// MyCustomExceptionFilter exception filter
    /// </summary>
    public class MyCustomExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<MyCustomExceptionFilter> logger;

        public MyCustomExceptionFilter(ILogger<MyCustomExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
```
### Authorization Filter
Create an authorization filter implementing `IAsyncAuthorizationFilter`.
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApiApp1
{
    public class MyCustomAuthorizationFilter : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly ILogger<MyCustomAuthorizationFilter> logger;

        public MyCustomAuthorizationFilter(ILogger<MyCustomAuthorizationFilter> logger)
        {
            this.logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (true) // check if authorized
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
```
### Result Filter
Create a result filter implementing `IAsyncResultFilter`.
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WebApiApp1
{
    /// <summary>
    /// MyCustomResultFilter result filter
    /// </summary>
    public class MyCustomResultFilter : IAsyncResultFilter
    {
        private readonly ILogger<MyCustomResultFilter> logger;

        public MyCustomResultFilter(ILogger<MyCustomResultFilter> logger)
        {
            this.logger = logger;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            await next();
        }
    }
}
```
### Middleware
Create a base class implementing an Asp.Net Middleware.
```csharp
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiApp1
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger<MyCustomMiddleware> logger;

        public MyCustomMiddleware(RequestDelegate next, ILogger<MyCustomMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await next(httpContext);
        }
    }
}
```
### Policy Requirement
Create a class implementing a Policy Requirement and his Handler.
```csharp
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiApp1
{
    /// <summary>
    /// MyCustomRequirement policy requirement
    /// </summary>
    public class MyCustomRequirement : IAuthorizationRequirement
    {
        public string MyValue { get; set; }
    }

    /// <summary>
    /// Validate the MyCustomRequirement policy requirement
    /// </summary>
    public class MyCustomRequirementHandler : AuthorizationHandler<MyCustomRequirement>
    {
        /// <summary>
        /// Validate the MyCustomRequirement policy requirement
        /// Remember to register this handler in startup, for example with:
        /// services.AddScoped<MyCustomRequirementHandler,MyCustomRequirementHandler>();
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MyCustomRequirement requirement)
        {
            Claim claim = context?.User?.FindFirst("http://schemas.microsoft.com/identity/claims/sub");
            if (claim == null || claim.Value !== requirement.MyValue)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
```
### Web API Controller
Create a base Controller.
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuokkaDev.Cqrs.Abstractions;
using QuokkaDev.Templates.Application.UseCases.Ping;

namespace WebApiApp1
{
    /// <summary>
    /// MyCustomWebApiController Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MyCustomWebApiController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ILogger<MyCustomWebApiController> logger;

        public MyCustomWebApiController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, ILogger<MyCustomWebApiController> logger)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
            this.logger = logger;
        }

        /// <summary>
        /// My Get method
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "MyPolicy")]
        public async Task<IActionResult> Get(string id)
        {
            // var result = await queryDispatcher.Dispatch(new Query());
            return Ok();
        }

        /// <summary>
        /// My Post method
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpPost()]
        [Authorize(Policy = "MyPolicy")]
        public async Task<IActionResult> Post([FromBody] object payload)
        {
            // var result = await commandDispatcher.Dispatch(new Command());
            return Ok();
        }
    }
}
```
### xUnit test
Create a base xUnit Test with Moq and FluentAssertions reference.
```csharp
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MyTestProject
{
    public class MyCustomUnitTest
    {
        public MyCustomUnitTest()
        {
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var mock = new Mock<object>();
            mock.Setup(m => m.Equals(It.IsAny<object>())).Returns(true);
            var obj = mock.Object;

            // Act
            await Task.CompletedTask;

            // Assert
            obj.Should().NotBeNull();
        }
    }
}
```
# Code Snippets
### Http Get Method
shortcut: `httpget`
```csharp
/// <summary>
/// Get
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
[HttpGet("{id}")]
[Authorize(Policy = "MyPolicy"]
public async Task<IActionResult> Get(string id)
{
    return Ok();
}
```
### Http Post Method
shortcut: `httppost`
```csharp
/// <summary>
/// Post
/// </summary>
/// <param name="payload"></param>
/// <returns></returns>
[HttpPost()]
[Authorize(Policy = "MyPolicy"]
public async Task<IActionResult> Post([FromBody] object payload)
{
    return Ok();
}
```
### Http Put Method
shortcut: `httpput`
```csharp
/// <summary>
/// Put
/// </summary>
/// <param name="payload"></param>
/// <returns></returns>
[HttpPut("{id}")]
[Authorize(Policy = "MyPolicy"]
public async Task<IActionResult> Put([FromRoute] string id, [FromBody] object payload)
{
    return Ok();
}
```
### Http Delete Method
shortcut: `httpdelete`
```csharp
/// <summary>
/// Delete
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
[HttpDelete("{id}")]
[Authorize(Policy = "MyPolicy"]
public async Task<IActionResult> Delete(string id)
{
    return Ok();
}
```
### Add authorization
shortcut: `addauth`
```csharp
//Add authorization Policies
services.AddAuthorization(options =>
{
    options.AddPolicy("MyPolicy",
        policy =>
        {
            policy.AuthenticationSchemes = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
            policy.RequireAuthenticatedUser();
            //policy.Requirements.Add(new MyCustomRequirement());
        });
});
```
### Add Authorization Policy
shortcut: `policy`
```csharp
options.AddPolicy("MyPolicy",
policy =>
{
    policy.AuthenticationSchemes = new List<string>() { JwtBearerDefaults.AuthenticationScheme };
    policy.RequireAuthenticatedUser();
    //policy.Requirements.Add(new MyCustomRequirement());
});
```
### Add CORS Policy
shortcut: `cors`
```csharp
//remember to call app.UseCors("DefaultPolicy"); in Configure()
services.AddCors(o => o.AddPolicy("DefaultPolicy", builder =>
{
    builder.WithOrigins(Config.GetValue<string>("AllowedHosts"))
        .AllowAnyMethod()
        .AllowAnyHeader();
}));
```
### Dapper Query
shortcut: `dapquery`
```csharp
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();

    var result = await connection.QueryAsync<dynamic>(
        @"select * FROM MyTable t
            WHERE t.Id=@id"
            , new { id }
        );
}
```
### Dapper Stored
shortcut: `dapstored`
```csharp
var p = new DynamicParameters();
p.Add("@myParams", "myParamsValue");

var result = await connection.Query([], p, commandType: CommandType.StoredProcedure);
```
### xUnit Test Method
shortcut: `xunit`
```csharp
[Fact(DisplayName="Test")]
public void Test()
{
    // Arrange

    // Act

    // Assert
    true.Should().BeTrue();
}
```
### xUnit async Test Method
shortcut: `xunitasync`
```csharp
[Fact(DisplayName="TestAsync")]
public async Task TestAsync()
{
    // Arrange

    // Act

    // Assert
    true.Should().BeTrue();
}
```
### Create Mock
shortcut: `mock`
```csharp
var mock = new Mock<object>();
mock.Setup(m => m.Equals(It.IsAny<object>())).Returns(true);
object obj = mock.Object;
```



