using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;

namespace DocumentManager.Infrastructure.Persistence.Repositories;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly ApplicationDbContext _dbContext;


    public AuditLogRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Insert(AuditLog AuditLog) => _dbContext.Set<AuditLog>().Add(AuditLog);

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}

