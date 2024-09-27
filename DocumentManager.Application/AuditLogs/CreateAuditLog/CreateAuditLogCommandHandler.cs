using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;


namespace DocumentManager.Application.Organizations.Commands.CreateAuditLog;
public class CreateAuditLogCommandHandler : ICommandHandler<CreateAuditLogCommand, int>
{
    private readonly IAuditLogRepository _auditLogRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateAuditLogCommandHandler(IAuditLogRepository auditLogRepository, IUnitOfWork unitOfWork)
    {
        _auditLogRepository = auditLogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateAuditLogCommand request, CancellationToken cancellationToken)
    {
        var auditLog = AuditLog.Create(request.DocumentId, request.UserId, request.Action);

        _auditLogRepository.Insert(auditLog);

        await _unitOfWork.SaveChangesAsync();

        return auditLog.Id;
    }
}

