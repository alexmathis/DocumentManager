using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;
using MediatR;


namespace DocumentManager.Application.Documents.Commands.CreateDocument;

public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, int>
{
    private readonly IDocumentRepository _documentRepository;

    public CreateDocumentCommandHandler(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        // Use the factory method to create a new Document
        var document = Document.Create(
            name: request.Name,
            size: request.Size,
            storagePath: request.StoragePath,
            ownerId: request.UserId,
            organizationId: request.OrganizationId
        );

        // Insert the document into the repository
        _documentRepository.Insert(document);

        // Save changes to the database
        await _documentRepository.SaveChangesAsync();

        // Return the newly created document's ID
        return document.Id;
    }
}