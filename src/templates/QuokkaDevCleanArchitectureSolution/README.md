# QuokkaDev CleanArchitecture Template
<!--#if (UseArc42Templates) -->
[Architectural documentation](docs/ARC42.md)
<!--#endif -->

## Migrations

### Lemonpie.CreditPortal.DataAccess.Commands.Migrations

Add-Migration MigrationName -Project QuokkaDev.Templates.Persistence.Ef -StartupProject QuokkaDev.Templates.Api -Context ApplicationDbContext

Update-Database MigrationName -Project QuokkaDev.Templates.Persistence.Ef -StartupProject QuokkaDev.Templates.Api -Context ApplicationDbContextDbContext

Remove-Migration -Project QuokkaDev.Templates.Persistence.Ef -StartupProject QuokkaDev.Templates.Api -Context ApplicationDbContextDbContext
