namespace QuokkaDev.Templates.Persistence.Ef.Infrastructure.Utils
{
    public class AuditEntry
    {
        public required Guid Id { get; set; }

        public required DateTimeOffset AuditDateTimeUtc { get; set; }
        public required string AuditType { get; set; }
        public required string AuditUser { get; set; }
        public required string EntityName { get; set; }
        public string? KeyValues { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? ChangedColumns { get; set; }

        public AuditEntry() { }
    }
}
