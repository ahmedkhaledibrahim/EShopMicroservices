using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Common
{
    public class AuditLog : BaseEntity<Guid>
    {
        public string TableName { get; set; } = default!;

        public string Action { get; set; } = default!; 

        public string KeyValues { get; set; } = default!;

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public DateTime ChangedAt { get; set; }

        public string? ChangedBy { get; set; }
    }
}
