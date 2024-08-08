using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace QuokkaDev.Templates.Persistence.Ef.Infrastructure.Utils
{
    internal class AuditInfo
    {
        public EntityEntry Entry { get; }
        public AuditType AuditType { get; set; }
        public string AuditUser { get; set; }
        public string EntityName { get; set; }
        public Dictionary<string, object>
               KeyValues
        { get; } = [];
        public Dictionary<string, object>
               OldValues
        { get; } = [];
        public Dictionary<string, object>
               NewValues
        { get; } = [];
        public List<string> ChangedColumns { get; } = [];

        public AuditInfo(EntityEntry entry, string auditUser)
        {
            Entry = entry;
            AuditUser = auditUser;
            EntityName = Entry.Entity.GetType().Name;
            SetChanges();
        }

        private void SetChanges()
        {
            foreach (PropertyEntry property in Entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                string dbColumnName = property.Metadata.GetColumnName();

                if (property.Metadata.IsPrimaryKey())
                {
                    KeyValues[propertyName] = property.CurrentValue!;
                    continue;
                }

                switch (Entry.State)
                {
                    case EntityState.Added:
                        NewValues[propertyName] = property.CurrentValue!;
                        AuditType = AuditType.Create;
                        break;

                    case EntityState.Deleted:
                        OldValues[propertyName] = property.OriginalValue!;
                        AuditType = AuditType.Delete;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            ChangedColumns.Add(dbColumnName);

                            OldValues[propertyName] = property.OriginalValue!;
                            NewValues[propertyName] = property.CurrentValue!;
                            AuditType = AuditType.Update;
                        }
                        break;
                }
            }
        }

        public AuditEntry ToAudit()
        {
            var audit = new AuditEntry()
            {
                Id = Guid.Empty,
                AuditDateTimeUtc = DateTimeOffset.UtcNow,
                AuditType = AuditType.ToString(),
                AuditUser = AuditUser,
                EntityName = EntityName,
                KeyValues = JsonSerializer.Serialize(KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
                ChangedColumns = ChangedColumns.Count == 0 ? null : JsonSerializer.Serialize(ChangedColumns)
            };

            return audit;
        }
    }

    internal enum AuditType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
}
