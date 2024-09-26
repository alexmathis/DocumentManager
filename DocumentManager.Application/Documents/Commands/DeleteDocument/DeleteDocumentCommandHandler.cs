using DocumentManager.Domain.Interfaces;
using MediatR;

namespace DocumentManager.Application.Documents.Commands.DeleteDocument;

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, Unit>
{
    private readonly IDocumentRepository _documentRepository;

    public DeleteDocumentCommandHandler(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the document by Id
        var document = await _documentRepository.GetByIdAsync(request.Id);

        if (document == null)
        {
            throw new KeyNotFoundException($"Document with Id {request.Id} not found.");
        }

        // Delete the document
        _documentRepository.Delete(document);

        // Save changes to the database
        await _documentRepository.SaveChangesAsync();

        return Unit.Value; // Return Unit to indicate success
    }
}
