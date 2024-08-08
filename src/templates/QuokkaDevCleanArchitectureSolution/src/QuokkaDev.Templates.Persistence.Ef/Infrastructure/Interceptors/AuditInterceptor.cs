using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using QuokkaDev.Templates.Application.Infrastructure.Interfaces;
using QuokkaDev.Templates.Domain.SeedWork;
using QuokkaDev.Templates.Persistence.Ef.Infrastructure.Implementations;
using QuokkaDev.Templates.Persistence.Ef.Infrastructure.Utils;

namespace QuokkaDev.Templates.Persistence.Ef.Infrastructure.Interceptors
{
    internal class AuditInterceptor : ISaveChangesInterceptor
    {
        private readonly ICurrentUserAccessor currentUserAccessor;

        public AuditInterceptor(ICurrentUserAccessor currentUserAccessor)
        {
            this.currentUserAccessor = currentUserAccessor;
        }

        public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            string userName = await currentUserAccessor.GetCurrentUserNameAsync();
            Audit(eventData, userName);
            return result;
        }

        public InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var task = SavingChangesAsync(eventData, result, default);
            task.GetAwaiter().GetResult();
            return result;
        }

        /// <summary>
        /// Create audit for tracked entities
        /// </summary>
        /// <returns></returns>
        private void Audit(DbContextEventData eventData, string username)
        {
            if (eventData.Context is ApplicationDbContext dbContext)
            {
                dbContext.ChangeTracker.DetectChanges();

                List<AuditInfo> auditEntries = new List<AuditInfo>();
                foreach (EntityEntry entry in dbContext.ChangeTracker.Entries())
                {
                    if (entry.Entity is IAuditable)
                    {
                        if (entry.Entity is AuditEntry || entry.State == EntityState.Detached ||
                        entry.State == EntityState.Unchanged)
                        {
                            continue;
                        }
                        var auditEntry = new AuditInfo(entry, username);
                        auditEntries.Add(auditEntry);
                    }
                }

                if (auditEntries.Any())
                {
                    var logs = auditEntries.Select(x => x.ToAudit());
                    dbContext.AuditEntries.AddRange(logs);
                }
            }
        }
    }
}
