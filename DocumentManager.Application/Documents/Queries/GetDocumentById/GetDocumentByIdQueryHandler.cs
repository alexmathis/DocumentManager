using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;
using MediatR;


namespace DocumentManager.Application.Documents.Queries.GetDocumentById;


public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, Document?>
{
    private readonly IDocumentRepository _documentRepository;

    public GetDocumentByIdQueryHandler(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<Document?> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the document by Id
        return await _documentRepository.GetByIdAsync(request.Id);
    }
}