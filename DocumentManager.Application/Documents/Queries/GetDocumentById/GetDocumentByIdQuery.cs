using DocumentManager.Application.Abstractions.Messaging;

namespace DocumentManager.Application.Documents.Queries.GetDocumentById;

public sealed record GetDocumentByIdQuery(int Id, int UserId) : IQuery<DocumentResponse>;
