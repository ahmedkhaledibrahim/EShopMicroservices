using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Common;
using System.Text.Json;

namespace Ordering.Persistence.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var auditEntries = new List<AuditLog>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog)
                    continue;

                if (entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                auditEntries.Add(CreateAudit(entry));
            }

            context.Set<AuditLog>().AddRange(auditEntries);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        private AuditLog CreateAudit(EntityEntry entry)
        {
            return new AuditLog
            {
                ID = Guid.NewGuid(),
                TableName = entry.Metadata.GetTableName()!,
                Action = entry.State.ToString(),
                KeyValues = GetPrimaryKey(entry),
                OldValues = GetOldValues(entry),
                NewValues = GetNewValues(entry),
                ChangedAt = DateTime.UtcNow
            };
        }
        private string GetOldValues(EntityEntry entry)
        {
            var values = entry.Properties
                .ToDictionary(
                    p => p.Metadata.Name,
                    p => entry.State == EntityState.Added
                        ? null
                        : p.OriginalValue);

            return JsonSerializer.Serialize(values);
        }

        private string GetNewValues(EntityEntry entry)
        {
            var values = entry.Properties
                .ToDictionary(
                    p => p.Metadata.Name,
                    p => p.CurrentValue);

            return JsonSerializer.Serialize(values);
        }

        private string GetPrimaryKey(EntityEntry entry)
        {
            var key = entry.Properties
                .Where(p => p.Metadata.IsPrimaryKey())
                .ToDictionary(
                    p => p.Metadata.Name,
                    p => p.CurrentValue);

            return JsonSerializer.Serialize(key);
        }
    }
}
