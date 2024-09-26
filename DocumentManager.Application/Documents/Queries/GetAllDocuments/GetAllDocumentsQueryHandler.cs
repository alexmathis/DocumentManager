using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;
using MediatR;


namespace DocumentManager.Application.Documents.Queries.GetAllDocuments;

public class GetAllDocumentsQueryHandler : IRequestHandler<GetAllDocumentsQuery, IEnumerable<Document>>
{
    private readonly IDocumentRepository _documentRepository;

    public GetAllDocumentsQueryHandler(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<IEnumerable<Document>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
    {
        // Retrieve all documents
        return await _documentRepository.GetAllAsync();
    }
}
