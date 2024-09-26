using DocumentManager.Domain.Interfaces;
using MediatR;


namespace DocumentManager.Application.Documents.Commands.UpdateDocument;
    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, Unit>
    {
        private readonly IDocumentRepository _documentRepository;

        public UpdateDocumentCommandHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<Unit> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the document by Id
            var document = await _documentRepository.GetByIdAsync(request.Id);

            if (document == null)
            {
                throw new KeyNotFoundException($"Document with Id {request.Id} not found.");
            }

            // Use the UpdateDocument method to update the document's properties
            document.UpdateDocument(
                newName: request.Name,
                newSize: request.Size,
                newStoragePath: request.StoragePath
            );

            // Update the document in the repository
            _documentRepository.Update(document);

            // Save changes to the database
            await _documentRepository.SaveChangesAsync();

            return Unit.Value; // Return Unit to indicate success
        }
    }

