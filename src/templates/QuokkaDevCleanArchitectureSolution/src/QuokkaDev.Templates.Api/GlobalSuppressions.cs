// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Roslynator", "RCS1102:Make class static.", Justification = "<Pending>", Scope = "type", Target = "~T:QuokkaDev.Templates.Api.Program")]
[assembly: SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "<Pending>", Scope = "type", Target = "~T:QuokkaDev.Templates.Api.Program")]
[assembly: SuppressMessage("Major Code Smell", "S3885:\"Assembly.Load\" should be used", Justification = "<Pending>", Scope = "member", Target = "~M:QuokkaDev.Templates.Api.Startup.ConfigureContainer(Autofac.ContainerBuilder)")]
[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "<Pending>", Scope = "member", Target = "~F:QuokkaDev.Templates.Api.Controllers.PingController.commandDispatcher")]
[assembly: SuppressMessage("Critical Code Smell", "S4487:Unread \"private\" fields should be removed", Justification = "<Pending>", Scope = "member", Target = "~F:QuokkaDev.Templates.Api.Controllers.PingController.commandDispatcher")]
