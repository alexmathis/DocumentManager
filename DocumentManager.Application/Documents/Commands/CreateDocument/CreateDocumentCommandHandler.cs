using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Application.Documents.Commands.CreateDocument;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Exceptions;
using DocumentManager.Domain.Interfaces;

public class CreateDocumentCommandHandler : ICommandHandler<CreateDocumentCommand, int>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public CreateDocumentCommandHandler(
        IDocumentRepository documentRepository,
        IAuditLogRepository auditLogRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IFileStorageService fileStorageService)
    {
        _documentRepository = documentRepository;
        _auditLogRepository = auditLogRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        if (user.OrganizationId != request.OrganizationId)
        {
            throw new UnauthorizedAccessException("You do not have permission to create documents in this organization.");
        }

        var defaultDirectory = "uploads/documents";

        var storagePath = await _fileStorageService.SaveFileAsync(request.File, defaultDirectory);

        var document = Document.Create(
            name: request.Name,
            size: (int)request.File.Length,  
            storagePath: storagePath,        
            ownerId: request.UserId,
            organizationId: request.OrganizationId
        );

        var newDocumentId = await _documentRepository.Insert(document);

        var auditLog = AuditLog.Create(newDocumentId, request.UserId, "Created");
        _auditLogRepository.Insert(auditLog);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return document.Id;  
    }
}
