using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Exceptions;
using DocumentManager.Domain.Interfaces;
using DocumentManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManager.Application.Documents.Commands.UpdateDocument;

public class UpdateDocumentCommandHandler : ICommandHandler<UpdateDocumentCommand, Unit>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public UpdateDocumentCommandHandler(
        IDocumentRepository documentRepository,
        IUserRepository userRepository,
        IAuditLogRepository auditLogRepository,
        IUnitOfWork unitOfWork,
        IFileStorageService fileStorageService)
    {
        _documentRepository = documentRepository;
        _userRepository = userRepository;
        _auditLogRepository = auditLogRepository;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<Unit> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
       
        var document = await _documentRepository.GetByIdAsync(request.Id);
        if (document == null)
        {
            throw new DocumentNotFoundException(request.Id);
        }


        var user = await _userRepository.GetByIdAsync(request.EditingUserId);
        if (user == null)
        {
            throw new UserNotFoundException(request.EditingUserId);
        }

        if (document.OwnerId != request.EditingUserId || document.OrganizationId != user.OrganizationId)
        {
            throw new UnauthorizedAccessException("You do not have permission to update this document.");
        }


        string storagePath = document.StoragePath;
        int newSize = document.Size;

        if (request.File != null)
        {
   
            await _fileStorageService.DeleteFileAsync(document.StoragePath);

            storagePath = await _fileStorageService.SaveFileAsync(request.File, "uploads/documents");
            newSize = (int)request.File.Length;
        }

        document.UpdateDocument(
            newName: request.Name,
            newSize: newSize,
            newStoragePath: storagePath
        );

        _documentRepository.Update(document);

        var auditLog = AuditLog.Create(request.Id, request.EditingUserId, "Updated");
        _auditLogRepository.Insert(auditLog);


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
