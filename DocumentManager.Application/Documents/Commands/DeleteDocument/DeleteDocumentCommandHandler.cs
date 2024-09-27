using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Exceptions;
using DocumentManager.Domain.Interfaces;

namespace DocumentManager.Application.Documents.Commands.DeleteDocument;

public class DeleteDocumentCommandHandler : ICommandHandler<DeleteDocumentCommand, Unit>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDocumentCommandHandler(
        IDocumentRepository documentRepository,
        IUserRepository userRepository,
        IAuditLogRepository auditLogRepository,
        IUnitOfWork unitOfWork)
    {
        _documentRepository = documentRepository;
        _userRepository = userRepository;
        _auditLogRepository = auditLogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the document by its ID
        var document = await _documentRepository.GetByIdAsync(request.Id);
        if (document == null)
        {
            throw new DocumentNotFoundException(request.Id);
        }

        // Retrieve the user who is attempting to delete the document
        var user = await _userRepository.GetByIdAsync(request.DeletingUserId);
        if (user == null)
        {
            throw new UserNotFoundException(request.DeletingUserId);
        }

        if (document.OwnerId != request.DeletingUserId || document.OrganizationId != user.OrganizationId)
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this document.");
        }
        _documentRepository.Delete(document);


        var auditLog = AuditLog.Create(document.Id, request.DeletingUserId, "Deleted");
     
        _auditLogRepository.Insert(auditLog);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value; 
    }
}
