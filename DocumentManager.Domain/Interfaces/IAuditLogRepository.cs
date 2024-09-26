using DocumentManager.Domain.Entities;

namespace DocumentManager.Domain.Interfaces;


public interface IAuditLogRepository
{
    /// <summary>
    /// Inserts an audit log entry.
    /// </summary>
    /// <param name="auditLog">The audit log entry to be added.</param>
    void Insert(AuditLog auditLog);

    /// <summary>
    /// Saves changes to the database asynchronously.
    /// </summary>
    Task SaveChangesAsync();
}